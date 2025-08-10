﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dto
{
    public class LogInResultDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public Dictionary<string, string>? Errors { get; set; }
    }
}
