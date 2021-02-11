using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace PingBot
{
    public class Config
    {
        public string[] Prefix { get; set; } = { "sd!" };

        public string Token { get; set; } = "Discord_Token_Here";

        public static XmlDocument MakeDocumentWithComments(XmlDocument xmlDocument)
        {
            xmlDocument = Xmlutils.CommentBeforeObject(xmlDocument, "/Config/Prefix", "Array of prefixes the bot will respond to");
            xmlDocument = Xmlutils.CommentBeforeObject(xmlDocument, "/Config/Token", "The Discord token, can be got from https://discord.com/developers/");

            return xmlDocument;
        }

        public static System.Threading.Tasks.Task<Config> GetAsync()
        {
            Config new_config = new Config();
            XmlSerializer serializer = new XmlSerializer(new_config.GetType());

            if (!File.Exists("pingbot.xml"))
            {
                using (StreamWriter streamWriter = new StreamWriter("pingbot.xml"))
                {
                    MakeDocumentWithComments(Xmlutils.SerializeToXmlDocument(new_config)).Save(streamWriter);
                    streamWriter.Close();
                }
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    new Process
                    {
                        StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "\\pingbot.xml")
                        {
                            UseShellExecute = true
                        }
                    }.Start();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("pingbot.xml should have opened in the default app, edit it, save it and press enter");
                    Thread.Sleep(1000);
                    Console.WriteLine("Press any key WHEN READY to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("pingbot.xml should exist in the CWD, edit it, save it and restart silverbot");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Environment.Exit(420);
                }
            }

            using FileStream fs = File.Open("pingbot.xml", FileMode.Open);
            Config readconfig = serializer.Deserialize(fs) as Config;
            fs.Close();

            return System.Threading.Tasks.Task.FromResult(readconfig);
        }
    }
}