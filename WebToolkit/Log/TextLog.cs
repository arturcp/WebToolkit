using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

namespace WebToolkit.Log
{
    public class TextLog
    {
        private static Hashtable AvailableLogs = new Hashtable();

        private TextLog()
        {

        }

        private TextLog(string uniqueIdentifier, string path)
        {
            Id = uniqueIdentifier;
            Path = string.Concat(path, "/", uniqueIdentifier, ".log");
            Reset();
        }


        private string Id { get; set; }
        private List<string> Logs { get; set; }
        private string Path { get; set; }

        public static TextLog Instance(string path, string uniqueIdentifier)
        {
            if (!AvailableLogs.ContainsKey(uniqueIdentifier))
            {
                TextLog log = new TextLog(path, uniqueIdentifier);
                AvailableLogs[uniqueIdentifier] = log;
            }

            return (TextLog)AvailableLogs[uniqueIdentifier];
        }

        public void Create()
        {
            FileStream fs = File.Create(Path);
            fs.Close();
        }

        public void CreateAndAdd(string log)
        {
            Create();
            Add(log);
        }

        public void Add(string log)
        {
            Logs.Add(log);
        }

        public void Reset()
        {
            Logs = new List<string>();
        }


        public void Commit()
        {
            using (StreamWriter writer = new StreamWriter(Path))
            {
                writer.Write(Content());
                writer.Flush();
            }
        }

        public string Content()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string log in Logs)
            {
                sb.AppendLine(log);
            }

            return sb.ToString();
        }
    }
}
