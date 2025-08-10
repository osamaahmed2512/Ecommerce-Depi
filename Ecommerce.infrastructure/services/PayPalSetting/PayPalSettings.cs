using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.infrastructure.services.PayPalSetting
{
    public class PayPalSettings
    {
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public string BaseUrl { get; set; }
    }
}
