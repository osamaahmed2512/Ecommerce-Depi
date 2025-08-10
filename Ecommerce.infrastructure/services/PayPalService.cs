using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Ecommerce.infrastructure.services.PayPalSetting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
namespace Ecommerce.infrastructure.services
{
    public class PayPalService:IPaymentService
    {
        private readonly HttpClient _client;
        private readonly PayPalSettings _settings;
        private readonly IOrderService _orderService;  // Add this

        public PayPalService(
            HttpClient client,
            IOptions<PayPalSettings> settings,
            IOrderService orderService)  // Add this
        {
            _client = client;
            _settings = settings.Value;
            _orderService = orderService;  // Add this
        }

                private async Task<string> GetAccessTokenAsync()
        {
            var byteArray = Encoding.UTF8.GetBytes($"{_settings.ClientId}:{_settings.Secret}");
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var body = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await _client.PostAsync($"{_settings.BaseUrl}/v1/oauth2/token", body);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("access_token").GetString();
        }
        public async Task<string> CreatePayPalOrderAsync(decimal amount, string currency, string returnUrl, string cancelUrl)
        {
            var token = await GetAccessTokenAsync();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var payload = new
            {
                intent = "CAPTURE",
                purchase_units = new[]{
                    new {
                        amount = new {
                            currency_code = currency,
                            value = amount.ToString("F2")
                        }
                    }
                },
                application_context = new
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl,
                    brand_name = "Your Store",
                    user_action = "PAY_NOW"
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_settings.BaseUrl}/v2/checkout/orders", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            var approveLink = doc.RootElement.GetProperty("links")
                .EnumerateArray()
                .First(x => x.GetProperty("rel").GetString() == "approve")
                .GetProperty("href").GetString();
            return approveLink;
        }

        //public async Task<DefaultserviceResponse> CaptureAndUpdateOrderAsync(string paypalToken, int orderId)
        //{
        //    try
        //    {
        //        var token = await GetAccessTokenAsync();
        //        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //        var response = await _client.PostAsync(
        //            $"{_settings.BaseUrl}/v2/checkout/orders/{paypalToken}/capture",
        //            new StringContent("{}", Encoding.UTF8, "application/json")
        //        );


        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Update order status to paid
        //            var result = await _orderService.UpdateOrderStatusAsync(orderId, "paid");
        //            if (!result.Success)
        //            {
        //                return DefaultserviceResponse.Fail(
        //                    $"Payment captured but failed to update order: {result.Message}",
        //                    result.StatusCode
        //                );
        //            }
        //            return DefaultserviceResponse.Ok("Payment captured and order updated successfully");
        //        }
        //        else
        //        {
        //            // Payment failed, update order status
        //            await _orderService.UpdateOrderStatusAsync(orderId, "payment_failed");
        //            var errorContent = await response.Content.ReadAsStringAsync();
        //            return DefaultserviceResponse.Fail(
        //                $"Payment capture failed: {errorContent}",
        //                StatusCodes.Status400BadRequest
        //            );
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error
        //        await _orderService.UpdateOrderStatusAsync(orderId, "payment_failed");
        //        return DefaultserviceResponse.Fail(
        //            $"Payment processing error: {ex.Message}",
        //            StatusCodes.Status500InternalServerError
        //        );
        //    }
        //}

        public async Task<DefaultserviceResponse> CaptureAndUpdateOrderAsync(string paypalToken, int orderId)
        {
            try
            {
                var token = await GetAccessTokenAsync();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _client.PostAsync(
                    $"{_settings.BaseUrl}/v2/checkout/orders/{paypalToken}/capture",
                    new StringContent("{}", Encoding.UTF8, "application/json")
                );

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    // Log the full error response
                    Console.WriteLine($"PayPal Capture Error: {responseContent}");

                    await _orderService.UpdateOrderStatusAsync(orderId, "payment_failed");
                    return DefaultserviceResponse.Fail(
                        $"Payment capture failed: {responseContent}",
                        (int)response.StatusCode
                    );
                }

                // Update order status to paid
                var result = await _orderService.UpdateOrderStatusAsync(orderId, "paid");
                if (!result.Success)
                {
                    return DefaultserviceResponse.Fail(
                        $"Payment captured but failed to update order: {result.Message}",
                        result.StatusCode
                    );
                }

                return DefaultserviceResponse.Ok("Payment captured and order updated successfully");
            }
            catch (Exception ex)
            {
                await _orderService.UpdateOrderStatusAsync(orderId, "payment_failed");
                return DefaultserviceResponse.Fail(
                    $"Payment processing error: {ex.Message}",
                    StatusCodes.Status500InternalServerError
                );
            }
        }
        public async Task<DefaultserviceResponse> CancelOrderPaymentAsync(int orderId)
        {
            try
            {
                var result = await _orderService.UpdateOrderStatusAsync(orderId, "cancelled");
                if (!result.Success)
                {
                    return DefaultserviceResponse.Fail(
                        $"Failed to cancel order: {result.Message}",
                        result.StatusCode
                    );
                }
                return DefaultserviceResponse.Ok("Order cancelled successfully");
            }
            catch (Exception ex)
            {
                return DefaultserviceResponse.Fail(
                    $"Error cancelling order: {ex.Message}",
                    StatusCodes.Status500InternalServerError
                );
            }
        }
    }
}
