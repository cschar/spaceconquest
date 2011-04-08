using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace spaceconquest
{
    class ConfigParser
    {
        StreamReader sr;
        String ConfigName;
        Dictionary<String, List<String>> ConfigOptions;

        public ConfigParser(String target) {
            ConfigName = target;
        }

        public Dictionary<String, List<String>> ParseConfig() {
            sr = new StreamReader(ConfigName);
            ConfigOptions = new Dictionary<string,List<string>>();
            List<String> Holder = new List<string>();
            while (!sr.EndOfStream) {
                try
                {
                    string[] line = sr.ReadLine().Split(' ');
                    string key = line[0]; string val = line[1];
                    if(ConfigOptions.ContainsKey(key)) {
                        ConfigOptions[key].Add(val);
                    }
                    else {
                        List<String> vals = new List<String>();
                        vals.Add(val);
                        ConfigOptions.Add(key, vals); 
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            sr.Close();
            return ConfigOptions;
        }
    }
}
