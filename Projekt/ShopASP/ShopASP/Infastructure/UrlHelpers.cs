using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopASP.Infastructure
{
    public static class UrlHelpers
    {
        public static string ItemPhotoPath (this UrlHelper helper, string ImageName)
        {
            var ImageFolder = AppSettings.ItemPhotoPathtoFolder;
            var path = Path.Combine(ImageFolder, ImageName);
            var absolutePath = helper.Content(path);
            return absolutePath;

        }
    }
}