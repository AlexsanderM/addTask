using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace addTask
{
    class Program
    {
        static void Main(string[] args)
        {
            int update_id = 0;
            string message_id = "";
            string id_user = "";
            string id_chat= "";

            string text_user = "";


            string urlBot = "https://api.telegram.org/bot";
            string token = "487607339:AAHr8V9HG_SznrwE_3eRbGnkpKSfVCNKj1s";

            WebClient webClient = new WebClient();

            String urlGetMe = $"{urlBot}{token}/getMe";


            while (true)
            {
                String url = $"{urlBot}{token}/getUpdates?offset={update_id + 1}";
                String req = webClient.DownloadString(url);

                var array = Newtonsoft.Json.Linq.JObject.Parse(req)["result"].ToArray();

                for (int i = 0; i < array.Length; i++)
                {
                    String messegeSendText = "www";

                    update_id = Convert.ToInt32(array[i]["update_id"]);
                    message_id = Convert.ToString(array[i]["message"]["message_id"]);
                    id_user = Convert.ToString(array[i]["message"]["from"]["id"]);
                    id_chat = Convert.ToString(array[i]["message"]["chat"]["id"]);
                    text_user = Convert.ToString(array[i]["message"]["text"]);

                    try
                    {
                        Console.WriteLine(message_id);
                        Console.WriteLine(update_id);
                        Console.WriteLine(id_user);
                        Console.WriteLine(id_chat);
                        Console.WriteLine(text_user);

                        url = $"{urlBot}{token}/sendMessage?chat_id={id_chat}&text={messegeSendText}";
                        webClient.DownloadString(url);
                    }
                    catch 
                    {
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }
}
