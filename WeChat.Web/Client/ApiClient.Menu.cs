using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace WeChat.Api.Client
{
    // ApiClient.Menu.cs    
    public partial class ApiClient
    {
        // Custom-defined Menu

        // https://api.weixin.qq.com/cgi-bin/menu/create?access_token=ACCESS_TOKEN

        public string CreateMenu(string json)
        {
            string accessToken = GetAccessToken();

            string relativeUri = string.Format("/cgi-bin/menu/create?access_token={0}", accessToken);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            // {"errcode":0,"errmsg":"ok"}
            // {"errcode":40018,"errmsg":"invalid button name size"}
            return ApiPost(relativeUri, content);
        }

        public string QueryMenu()
        {
            string accessToken = GetAccessToken();

            string relativeUri = string.Format("/cgi-bin/menu/get?access_token={0}", accessToken);

            // {"menu":{"button":[{"type":"click","name":"Daily Song","key":"V1001_TODAY_MUSIC","sub_button":[]},{"type":"click","name":" Artist Profile ","key":"V1001_TODAY_SINGER","sub_button":[]},{"name":"Menu","sub_button":[{"type":"view","name":"Search","url":"http://www.soso.com/","sub_button":[]},{"type":"view","name":"Video","url":"http://v.qq.com/","sub_button":[]},{"type":"click","name":"Like us","key":"V1001_GOOD","sub_button":[]}]}]}}
            return ApiGet(relativeUri);
        }

        public string DeleteMenu()
        {
            string accessToken = GetAccessToken();

            string relativeUri = string.Format("/cgi-bin/menu/delete?access_token={0}", accessToken);

            // { "errcode":0,"errmsg":"ok"}
            return ApiGet(relativeUri);
        }


    }
}