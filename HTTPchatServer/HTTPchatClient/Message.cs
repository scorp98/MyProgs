using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft;

namespace HTTPchatClient
{
    [Serializable]
    public class Message
    {
        public string Sender;
        public string Receiver;
        public string Data;
        public string MessageType;
        public string ID;
    }
    [Serializable]
    public class Response
    {
        public bool Status;
    }
    public static class MsgOpsJson
    {
        public static string Serialize(Message msg)
        {
            string json = JsonConvert.SerializeObject(msg, Newtonsoft.Json.Formatting.Indented);
            return json;
        }
        public static Message DeSerialize(string json)
        {
            Message msg = JsonConvert.DeserializeObject<Message>(json);
            return msg;
        }
    }
}