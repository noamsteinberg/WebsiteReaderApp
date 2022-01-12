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
    public class GoogleWebsiteReader : WebsiteReader
    {
        public override string Url { get; set; }
        public override string Topic { get; set; }
        public override string ImgIndicator { get; set; }

        public GoogleWebsiteReader(string topic)
        {
            Topic = topic;
            Url = "https://www.google.com/search?q=" + Topic + "&tbm=isch";
            ImgIndicator = "class=\"GpQGbf\"";
        }
    }
}
