using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using XData.Diagnostics.Log;

namespace WeChat.Data.Components
{
    public class ReceivingHandler // Business Logic
    {
        public string Handle(string received)
        {
            return Handle(XElement.Parse(received)).ToString();
        }

        // for example
        private XElement Handle(XElement received)
        {
            Dictionary<string, object> dict = ReceivingMessage.ToDictionary(received);
            string msgType = dict["MsgType"].ToString();
            switch (msgType)
            {
                case "text":
                    return CallbackMessage.CreateTextMessage("Echo:" + dict["Content"].ToString(), dict);
                case "image":
                    return CallbackMessage.CreateImageMessage(dict["MediaId"].ToString(), dict);
                case "voice":
                    return CallbackMessage.CreateVoiceMessage(dict["MediaId"].ToString(), dict);
                case "video":
                case "shortvideo":
                    return CallbackMessage.CreateVideoMessage(dict["MediaId"].ToString(), "Video", "Echo", dict);
                case "location":
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Echo:");
                        sb.AppendLine(dict["Label"].ToString());
                        sb.AppendLine("Location:" + dict["Location_X"].ToString()+"," + dict["Location_Y"].ToString());
                        sb.AppendLine("Scale:" + dict["Scale"].ToString());
                        return CallbackMessage.CreateTextMessage(sb.ToString(), dict);
                    }
                case "link":
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Echo:");
                        sb.AppendLine("MsgType:link");
                        sb.AppendLine("Title:" + dict["Title"].ToString());
                        sb.AppendLine("Description:" + dict["Description"].ToString());
                        sb.AppendLine("Url:" + dict["Url"].ToString());
                        return CallbackMessage.CreateTextMessage(sb.ToString(), dict);
                    }
                case "event":
                    break;
            }

            Exception exception  = new NotSupportedException(msgType);
            Log4.Logger.Error("Handle receiving message", exception);
            throw exception;
        }


    }
}