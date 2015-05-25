using System.Collections.Generic;
using System.IO;

namespace CodeDiscoPlayer
{
    public class Config
    {
        public Config()
        {
            KeyMaps = new Dictionary<string, KeyMap>(32)
            {
                {"var",  new KeyMap{ StreamResourceName = "fkey"}},
                {"function",  new KeyMap{ StreamResourceName = "slash"}},
                 
                {".",  new KeyMap{ StreamResourceName = "wololo"}},
                {";",  new KeyMap{ StreamResourceName = "funky"}},
                {"[",  new KeyMap{ StreamResourceName = "squareBrackets", Loop = Loop.StartLoop}},
                {"]",  new KeyMap{ StreamResourceName = "squareBrackets", Loop = Loop.StopLoop}},
                {"{",  new KeyMap{ StreamResourceName = "CurleyBracketsLoop", Loop = Loop.StartLoop}},
                {"}",  new KeyMap{ StreamResourceName = "CurleyBracketsLoop", Loop = Loop.StopLoop}},
                {"(",  new KeyMap{ StreamResourceName = "roundbrackets", Loop = Loop.StartLoop}},
                {")",  new KeyMap{ StreamResourceName = "roundbrackets", Loop = Loop.StopLoop}},
                {"=",  new KeyMap{ StreamResourceName = "snare"}},
                {" ",  new KeyMap{ StreamResourceName = "highhat"}},
            };
        }

        public Dictionary<string, KeyMap> KeyMaps { get; set; }

        public static Config Load()
        {
            if (!File.Exists("config.json")) return new Config();

            var json = File.ReadAllText("config.json");

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            return serializer.Deserialize<Config>(json);
        }
    }

    public class KeyMap
    {
        public Loop Loop { get; set; }

        public string StreamResourceName { get; set; }
    }

    public enum Loop
    {
        None,
        StartLoop,
        StopLoop
    }
}
