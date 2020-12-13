using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace HTTPchatClient
{
    public class ClientHTTPclient
    {
        static HttpClient client = new HttpClient();
        static string uri = "http://localhost:8888/connection";
        public static Message msg = new Message();

        public static async Task<bool> GetIn(Message msg)
        {
            string json = MsgOpsJson.Serialize(msg);
            json = StringUtil.Crypt(json);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);

            string responseString = await response.Content.ReadAsStringAsync();
            responseString = StringUtil.Decrypt(responseString);
            Response _Response = JsonConvert.DeserializeObject<Response>(responseString);
            bool Success = _Response.Status;
            return Success;
        }
        public static void NonStopSend()
        {
            msg.ID = ClientHTTPlistener.port;
            msg.Sender = Program.msg.Sender;
            msg.MessageType = "0";
            string input;
            Console.WriteLine("Enter message: ");
            while (true)
            {
                msg.Receiver = null;
                input = Console.ReadLine();
                Regex regex = new Regex(@"^@([\w]+)");
                MatchCollection matches = regex.Matches(input);
                if (matches.Count == 1)
                {
                    msg.Receiver = matches[0].Value.Substring(1);
                    try
                    {
                        msg.Data = input.Substring(matches[0].Value.Length + 1);
                    }
                    catch
                    {
                        msg.Data = "";
                    }
                }
                else
                    msg.Data = input;
                SendMessage(msg);
            }
        }
        public static void SendMessage(Message msg)
        {
            string json = MsgOpsJson.Serialize(msg);
            json = StringUtil.Crypt(json);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            client.PostAsync(uri, content);
        }
    }
}
