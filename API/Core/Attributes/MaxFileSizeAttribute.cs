using Core.Settings;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;
        public MaxFileSizeAttribute(int _maxFileSize)
        {
            maxFileSize = _maxFileSize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
                if (file.Length > maxFileSize)
                {
                    return new ValidationResult($"Maximum allowed size is {maxFileSize} bytes!");
                }
            }
            return ValidationResult.Success;
        }

    }
}
