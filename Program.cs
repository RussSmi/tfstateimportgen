using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace tfstateimportgen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: tfstateimportgen.exe <tfstate file> <output file");
            }
            else 
            {
                string fileName = args[0];
                string output = "";

                try
                {

                    string jsonString = File.ReadAllText(fileName);

                    var jsonData = JsonConvert.DeserializeObject<dynamic>(jsonString);

                    foreach (var item in jsonData.resources)
                    {
                        if (item.mode != "data")
                        {
                            // create import line
                            string importLine = BuildLine(item);
                            Console.WriteLine(importLine);
                            output += importLine;
                            output += "\n";
                        }
                    }
                    File.WriteAllText(args[1], output);
                    Console.WriteLine("Completed, push enter to close...");
                    Console.ReadLine();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught processing state file, push enter to close...");
                    Console.WriteLine(ex.ToString());
                    Console.ReadLine(); 
                }
            }

        }

        static string BuildLine(dynamic item)
        {
            string line = "";
            string name = "";
            string id = "";

            if(item.module != null)
            {
                name = $"{item.module}.";
            }

            id = item.instances[0].attributes.id;

            line = $"terraform import {name}{item.type}.{item.name} {id}";            

            return line;
        }
    }
}
