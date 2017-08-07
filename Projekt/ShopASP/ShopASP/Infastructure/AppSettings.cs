using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ShopASP.Infastructure
{
    public class AppSettings
    {

        private static string _itemPhotosPath = ConfigurationManager.AppSettings["ItemImagesFolder"];

        public static string ItemPhotoPathtoFolder
        {
            get
            {
                return _itemPhotosPath;
            }
        }
    }
}