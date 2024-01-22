using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VimassUHFUploadVideo.Vpass.Object
{
    public class WebClientWithTimeout : WebClient
    {
        public int Timeout { get; set; }

        public WebClientWithTimeout()
        {
            this.Timeout = 15000; // Default timeout of 60 seconds
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request != null)
            {
                request.Timeout = this.Timeout;
            }
            return request;
        }
    }

}
