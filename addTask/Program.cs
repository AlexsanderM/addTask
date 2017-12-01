using Microsoft.Office.Interop.Excel;
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
            int date = 0;
            string message_id = "";
            int id_user;
            string id_chat= "";
            string text_user = "";


            string urlBot = "https://api.telegram.org/bot";
            string token = "487607339:AAHr8V9HG_SznrwE_3eRbGnkpKSfVCNKj1s";

            WebClient webClient = new WebClient();
                        
            while (true)
            {
                String url = $"{urlBot}{token}/getUpdates?offset={update_id + 1}";
                String req = webClient.DownloadString(url);

                var array = Newtonsoft.Json.Linq.JObject.Parse(req)["result"].ToArray();

                foreach ( var info in array )
                {
                    String messegeSendText = "";

                    update_id = Convert.ToInt32(info["update_id"]);
                    message_id = Convert.ToString(info["message"]["message_id"]);
                    id_user = Convert.ToInt32(info["message"]["from"]["id"]);
                    id_chat = Convert.ToString(info["message"]["chat"]["id"]);
                    date = Convert.ToInt32(info["message"]["date"]);
                    text_user = Convert.ToString(info["message"]["text"]);

                    Console.WriteLine(message_id);
                    Console.WriteLine(update_id);
                    Console.WriteLine(id_user);
                    Console.WriteLine(id_chat);
                    Console.WriteLine(date);
                    Console.WriteLine(text_user);

                    DateTime pDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(date);
                    Console.WriteLine(pDate);

                    try
                    {
                        if ( id_user == 130116992) {
                            messegeSendText = Console.ReadLine();

                            String[] arrayWordTask = text_user.Split(' ');
                            Console.WriteLine(arrayWordTask[0] + arrayWordTask[1]);

                            Microsoft.Office.Interop.Excel.Application exApp = new Microsoft.Office.Interop.Excel.Application();
                            exApp.Visible = true;
                            exApp.Workbooks.Add();
                            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
                            workSheet.Cells[1, 1] = "ID";
                            workSheet.Cells[1, 2] = "Name";
                            workSheet.Cells[1, 3] = "Age";
                            int rowExcel = 2;
                            //for (int i = 0; i < dataGridView1.Rows.Count; i++)                            
                                workSheet.Cells[rowExcel, "A"] = pDate;
                           
                            workSheet.SaveAs("D:\\Sasha\\MyFile.xls");
                            exApp.Quit();


                            url = $"{urlBot}{token}/sendMessage?chat_id={id_chat}&text={messegeSendText}";
                            webClient.DownloadString(url);
                        }
                        else
                        {
                            messegeSendText = "Who are you?";

                            url = $"{urlBot}{token}/sendMessage?chat_id={id_chat}&text={messegeSendText}";
                            webClient.DownloadString(url);
                        }                        
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
