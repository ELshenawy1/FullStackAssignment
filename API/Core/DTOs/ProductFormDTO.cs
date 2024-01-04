using Core.Attributes;
using Core.Settings;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ProductFormDTO
    {
        public string Category { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int MinQuantity { get; set; }
        public int DiscountRate { get; set; }

    }
}
