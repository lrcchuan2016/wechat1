using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using WeChat.Data.Components;
using WeChat.Dev.Configuration;
using XData.Data.Extensions;
using XData.Diagnostics.Log;

namespace WeChat.Http.Models
{
    internal class ReceivingModel
    {
        private Tencent.WXBizMsgCrypt _bizMsgCrypt = null;
        private Tencent.WXBizMsgCrypt BizMsgCrypt
        {
            get
            {
                if (_bizMsgCrypt == null)
                {
                    _bizMsgCrypt = new Tencent.WXBizMsgCrypt(DevConfig.Token, DevConfig.EncodingAESKey, DevConfig.AppID);
                }
                return _bizMsgCrypt;
            }
        }

        public HttpResponseMessage Handle(HttpRequestMessage request, string receivingMessage)
        {
            IEnumerable<KeyValuePair<string, string>> queryStr = request.GetQueryNameValuePairs();
            string encrypt_type = queryStr.GetValue("encrypt_type");
            Log4.Logger.Debug("encrypt_type:" + encrypt_type);
            if (encrypt_type == "aes")
            {
                string msg_signature = queryStr.GetValue("msg_signature");
                string timestamp = queryStr.GetValue("timestamp");
                string nonce = queryStr.GetValue("nonce");

                Log4.Logger.Debug("msg_signature:" + msg_signature);
                Log4.Logger.Debug("timestamp:" + timestamp);
                Log4.Logger.Debug("nonce:" + nonce);

                string receivedXml = string.Empty;
                int error = BizMsgCrypt.DecryptMsg(msg_signature, timestamp, nonce, receivingMessage, ref receivedXml);
                if (error != 0)
                {
                    Log4.Logger.ErrorFormat("BizMsgCrypt.DecryptMsg:{0}, Error:{1}", receivingMessage, error);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                }

                string replyXml = new ReceivingHandler().Handle(receivedXml);

                string encryptedMessage = string.Empty;
                error = BizMsgCrypt.EncryptMsg(replyXml, timestamp, nonce, ref encryptedMessage);
                if (error != 0)
                {
                    Log4.Logger.ErrorFormat("BizMsgCrypt.EncryptMsg:{0}, Error:{1}", replyXml, error);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                }

                HttpResponseMessage response = new HttpResponseMessage();
                response.Content = new StringContent(encryptedMessage, Encoding.UTF8);
                return response;
            }
            else // encrypt_type == "raw"
            {
                string replyXml = new ReceivingHandler().Handle(receivingMessage);

                HttpResponseMessage response = new HttpResponseMessage();
                response.Content = new StringContent(replyXml, Encoding.UTF8);
                return response;
            }
        }


    }
}