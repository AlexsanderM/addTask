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
        static String addTask(String[] array) {
            String task = "";

            for (int i = 1; i < array.Length; i++)
            {
                task = task + array[i] + " ";
            }

            return task;
        }

        static void Main(string[] args)
        {
            int update_id = 0;
            int date = 0;
            string message_id = "";
            int id_user;
            string id_chat= "";
            string text_user = "";
            string fileExel = "D:\\Sasha\\MyFile.xls";

            int day;
            int month;
            int house; 
            int minutes;

            string urlBot = "https://api.telegram.org/bot";
            string token = "487607339:AAHr8V9HG_SznrwE_3eRbGnkpKSfVCNKj1s";

            WebClient webClient = new WebClient();

            Application exApp = new Application();

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
                    
                    DateTime dateNow = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(date);
                    Console.WriteLine(dateNow);

                    day = dateNow.Day;
                    month = dateNow.Month;
                    house = dateNow.TimeOfDay.Hours;
                    minutes = dateNow.TimeOfDay.Minutes;

                    Console.WriteLine($"{day}.{month}");
                    Console.WriteLine($"{house}:{minutes}");

                    try
                    {
                        if ( id_user == 130116992) {
                            //messegeSendText = Console.ReadLine();
                            int rowsExel;

                            String[] arrayWordTask = text_user.Split(' ');                                                

                            Workbook workbook = exApp.Workbooks.Open(fileExel);
                            Worksheet workSheet = (Worksheet)workbook.ActiveSheet;

                            rowsExel = workSheet.UsedRange.Rows.Count;   // check rows in Exel

                            exApp.Visible = false;
                            
                            workSheet.Cells[1, 1] = "Дата";
                            workSheet.Cells[1, 2] = "Время";
                            workSheet.Cells[1, 3] = "Кабинет";
                            workSheet.Cells[1, 4] = "Задача";
                                                                             
                            workSheet.Cells[rowsExel + 1, "A"] = $"{day}.{month}";
                            workSheet.Cells[rowsExel + 1, "B"] = $"{house}:{minutes}";
                            workSheet.Cells[rowsExel + 1, "C"] = arrayWordTask[0];
                            workSheet.Cells[rowsExel + 1, "D"] = addTask(arrayWordTask);

                            exApp.DisplayAlerts = false;
                            workbook.SaveAs(fileExel);
                            exApp.Quit();                            
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
