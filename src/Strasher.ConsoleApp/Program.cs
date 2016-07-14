using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Strasher;
using System.Configuration;

namespace Strasher.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> files = new List<string>();

            if (args.Length == 0)
            {
                Console.WriteLine("Enter file or folder path.");
                string fileOrDirectory = Console.ReadLine();
                if (File.Exists(fileOrDirectory))
                    files.Add(fileOrDirectory);
                else if (Directory.Exists(fileOrDirectory))
                    files = Directory.GetFiles(fileOrDirectory, "*", SearchOption.TopDirectoryOnly).ToList();
                if (files.Count == 0)
                {
                    Console.WriteLine("Not a valid folder or file path.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

                Strasher strasher = new Strasher();
                Console.WriteLine();
                Console.WriteLine("To generate Strashes for the given files, press s.");
                Console.WriteLine("To verfiy the latest Strash for these files, press v.");
                Console.WriteLine("Enter any other key to view all existing Strashes.");
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                bool generateStrash = (consoleKeyInfo.Key == ConsoleKey.S);
                bool verifyStrash = (consoleKeyInfo.Key == ConsoleKey.V);
                Console.WriteLine(Environment.NewLine);
                if (generateStrash) Console.WriteLine("Files Strashed:");
                foreach (string file in files)
                {
                    if (generateStrash)
                    {
                        strasher.StrashFile(file, ConfigurationManager.AppSettings["FileHashesCommaDelimited"].Split(',').ToList());
                        Console.WriteLine(file);
                    }
                    else if (verifyStrash)
                    {
                        Console.WriteLine(file);
                        foreach (KeyValuePair<string, bool> verifiedItem in strasher.VerifyLastestStrash(file))
                        {
                            Console.WriteLine(string.Concat(verifiedItem.Key, " hash checked, and it ", verifiedItem.Value ? "matches." : "does not match."));
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine(file);
                        foreach (KeyValuePair<string, string> strash in strasher.ReadFileStrashes(file))
                        {
                            Console.WriteLine(strash.Key);
                            Console.WriteLine(strash.Value);
                        }
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to end the program.");
                Console.ReadKey();
            }
        }
    }
}
