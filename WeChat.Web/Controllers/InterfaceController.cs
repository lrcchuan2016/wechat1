using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeChat.Http.Models;

namespace WeChat.Http.Controllers
{
    // lvchengnongke@163.com
    // lcnk20140121LCNK
    [RoutePrefix("")]
    public class InterfaceController : ApiController
    {
        [Route()]
        public HttpResponseMessage Get()
        {
            return new AccessModel().Access(this.Request);
        }

        [Route()]
        public HttpResponseMessage Post()
        {

            //IEnumerable<KeyValuePair<string, string>> queryStr = request.GetQueryNameValuePairs();
            //string signature = queryStr.GetValue("signature");
            //string timestamp = queryStr.GetValue("timestamp");
            //string nonce = queryStr.GetValue("nonce");
            //string encrypt_type = queryStr.GetValue("encrypt_type");
            //if (encrypt_type == "aes")
            //{

            //}
            //else
            //{

            //}
            return null;

        }
    }
}
