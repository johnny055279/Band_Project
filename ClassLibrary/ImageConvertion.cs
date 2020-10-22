using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;

namespace ClassLibrary
{
    public class ImageConvertion
    {
        public static string ByteToImage(byte[] ImageByte)
        {
            if (ImageByte == null)
            {
                return null;
            }
            else
            {
                try
                {
                    MemoryStream ms = new MemoryStream();
                    ms.Write(ImageByte, 0, ImageByte.Length);
                    string imageBase64 = Convert.ToBase64String(ms.ToArray());
                    string imageSrc = string.Format("data:image/gif;base64,{0}", imageBase64);
                    ms.Dispose();
                    return imageSrc;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}