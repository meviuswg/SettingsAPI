using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SettingsAPIClient.Util
{
    internal class SerializationHelper
    {
        public static string ToBase64String(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static byte[] FromBase64String(string data)
        {
            return Convert.FromBase64String(data);
        }

        public static string HtmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        public static Image ToImage(string str)
        {
            byte[] byteArr = FromBase64String(str);

            using (var stream = new MemoryStream(byteArr))
            {
                return Image.FromStream(stream);
            }
        }

        public static string ImageToString(Image image)
        {
            ImageConverter converter = new ImageConverter();
            return ToBase64String((byte[])converter.ConvertTo(image, typeof(byte[])));
        } 

    }
}
