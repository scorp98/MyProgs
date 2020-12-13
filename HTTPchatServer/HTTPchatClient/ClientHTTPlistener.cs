using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft;
namespace HTTPchatClient
{
    class ClientHTTPlistener
    {
        public static string port;
        public static void StartListener()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add($"http://localhost:{port}/connection/");
            listener.Start();

            while (listener.IsListening)
            {
                HandleRequests(listener);
            };

            listener.Stop();
        }
        private static void HandleRequests(HttpListener listener)
        {
            Task<HttpListenerContext> contextTask = listener.GetContextAsync();

            HttpListenerContext context = contextTask.Result;
            HttpListenerRequest request = context.Request;
            StreamReader reader = new StreamReader(request.InputStream);
            string s = reader.ReadToEnd();
            s = StringUtil.Decrypt(s);
            reader.Close();


            Message RecievedMessage = MsgOpsJson.DeSerialize(s);

            Console.WriteLine($"{RecievedMessage.Sender}: {RecievedMessage.Data}");

            HttpListenerResponse response = context.Response;
            Response authRes = new Response()
            {
                Status = true
            };
            string responseStr = JsonConvert.SerializeObject(authRes);
            responseStr = StringUtil.Crypt(responseStr);
            using (StreamWriter output = new StreamWriter(response.OutputStream))
            {
                output.Write(responseStr);
            }
        }
        //public static void SendResponce(HttpListenerContext context, bool OK)
        //{
        //    HttpListenerResponse response = context.Response;
        //    Response authRes = new Response()
        //    {
        //        Status = OK
        //    };
        //    string responseStr = JsonConvert.SerializeObject(authRes);
        //    responseStr = StringUtil.Crypt(responseStr);
        //    using (StreamWriter output = new StreamWriter(response.OutputStream))
        //    {
        //        output.Write(responseStr);
        //    }
        //}
    }
}
