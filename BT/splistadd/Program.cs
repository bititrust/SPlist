using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using System.Security;
using System.Net;

namespace splistadd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter username: ");
            string login = Console.ReadLine(); 

            Console.WriteLine();
            Console.Write("Enter password: ");

            string password = "";

            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

                        
            var securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }

            string siteUrl = "https://ekontor/sites/it/itintsreg";
            ClientContext clientContext = new ClientContext(siteUrl);

            Microsoft.SharePoint.Client.List myList = clientContext.Web.Lists.GetByTitle("Autolist");

            ListItemCreationInformation itemInfo = new ListItemCreationInformation();
            ListItem myItem = myList.AddItem(itemInfo);
            myItem["Title"] = "WMI is down";
            myItem["Seade"] = "Peaserver";
            myItem["Kirjeldus"] = "Peaserver WNI is down for 5 min";
            try
            {
                myItem.Update();
                //var onlineCredentials = new SharePointOnlineCredentials(login, securePassword);
                NetworkCredential _myCredentials = new NetworkCredential(login, securePassword,"ESS");
                clientContext.Credentials = _myCredentials;
                clientContext.ExecuteQuery();
                Console.WriteLine("New Item inserted Succsessfully!");
                Console.WriteLine();

            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
            
        }
    }
}
