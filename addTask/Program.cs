using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace addTask
{
    class Program
    {
        static void Main(string[] args)
        {
            int update_id = 0;

            string url = "https://api.telegram.org/bot";
            string token = "487607339:AAHr8V9HG_SznrwE_3eRbGnkpKSfVCNKj1s";

            WebClient webClient = new WebClient();

            String urlGetMe = $"{url}{token}/getMe";


            while (true)
            {
                String urlUpdate = $"{url}{token}/getUpdates?offset={update_id + 1}";
                String req = webClient.DownloadString(urlUpdate);

                var array = Newtonsoft.Json.Linq.JObject.Parse(req)["result"].ToArray();

                for (int i = 0; i < array.Length; i++)
                {
                    //Console.WriteLine($"{array[i]["message"]["from"].ToString()}");
                    Console.WriteLine($"{array[i].ToString()}");


                }
                Console.ReadLine();
            }

            

        }
    }
}
