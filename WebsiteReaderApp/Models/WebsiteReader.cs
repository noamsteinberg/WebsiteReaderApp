using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebsiteReaderApp.Models
{
    public abstract class WebsiteReader
    {
        public abstract string Url { get; set; }
        public abstract string Topic { get; set; }
        public abstract string ImgIndicator { get; set; }


        //This function creates a Http reuest to get the html data of the
        //sent url. the function retrns the html data.
        public string GetHtmlCode()
        {
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(Url);
            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    throw new FileNotFoundException("Data sream is null");
                using (var sr = new StreamReader(dataStream))
                {
                    data = sr.ReadToEnd();
                }
            }

            return data;
        }

        //This function calls to the GetHtmlCode function and gets the Image urls out of the html by unique tag
        //symbolizes the begining of the images table. Then, the function identifies the url images by the img tag and the source 
        // url by the src= tag. Finally, the function returns a list containing 5 url images.
        public List<string> GetImgUrls()
        {
            string html = GetHtmlCode();
            var urls = new List<string>();
            int ndx = html.IndexOf(ImgIndicator, StringComparison.Ordinal);
            ndx = html.IndexOf("<img", ndx, StringComparison.Ordinal);

            while (ndx >= 0 && urls.Count < 5)
            {
                ndx = html.IndexOf("src=\"", ndx, StringComparison.Ordinal);
                ndx = ndx + 5;
                int ndx2 = html.IndexOf("\"", ndx, StringComparison.Ordinal);
                string url = html.Substring(ndx, ndx2 - ndx);
                urls.Add(url);
                ndx = html.IndexOf("<img", ndx, StringComparison.Ordinal);
            }

            return urls;
        }


        //This function gets the image Url, the wanted file name and the wanted format.
        //the function saves the image at the wanted location and in the wanted format.
        public void SaveImage(string imageUrl, string filename, ImageFormat format)
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(imageUrl);
            Bitmap bitmap; bitmap = new Bitmap(stream);

            if (bitmap != null)
            {
                bitmap.Save(filename, format);
            }

            stream.Flush();
            stream.Close();
            client.Dispose();
        }

        //This function calls the GetImgUrls function to get the image urls list and then saves them inn temp directory
        public bool Download5TopPhotos()
        {
            List<string> urls = GetImgUrls();
            for (int i = 0; i < urls.Count; i++)
            {
                SaveImage(urls[i], string.Format(@"c:\temp\{0}image{1}.Jpeg", Topic, i+1), ImageFormat.Jpeg);
            }
            return true;
        }

    }
}