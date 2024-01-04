using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Settings
{
    public static class FileSettings
    {
        public const string ImagesPath = "/assets/images";
        public const string AllowedExtensions = ".jpg,.jpeg,.png";
        public const int MaxFileSizeInMB = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;

    }
}
