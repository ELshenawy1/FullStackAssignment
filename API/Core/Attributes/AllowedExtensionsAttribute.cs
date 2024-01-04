using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string allowedExtensions;
        public AllowedExtensionsAttribute(string _allowedExtensions)
        {
            allowedExtensions = _allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null) 
            {
                var extension = Path.GetExtension(file.FileName);
                var isAllowed = allowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);
                if (!isAllowed)
                {
                    return new ValidationResult($"Only {allowedExtensions} are allowed!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
