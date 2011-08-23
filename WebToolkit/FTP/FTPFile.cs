
using System.Text.RegularExpressions;
namespace WebToolkit.FTP
{
    public class FTPFile
    {
        public string Permissions { get; set; }
        public string FileCode { get; set; } 
        public string Owner { get; set; }
        public string Group { get; set; }
        public string Size { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string Name { get; set; }

        public bool IsDirectory { get { return Permissions != null && Permissions.Length > 1 && Permissions[0].ToString().ToLower() == "d"; } }

        public FTPFile(string line)
        {
            line = Regex.Replace(line, @"\s+", " ");
            string[] parts = line.Split(' ');
            Permissions = parts[0];
            FileCode = parts[1];
            Owner = parts[2];
            Group = parts[3];
            Size = parts[4];
            Month = parts[5];
            Day = parts[6];
            Time = parts[7];

            Name = string.Empty;
            for (int i = 8; i < parts.Length; i++)
            {
                if (i == parts.Length - 1)
                    Name += parts[i];
                else
                    Name += parts[i] + " ";

            }
              
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}", Permissions, FileCode, Owner, Group, Size, Month, Day, Time, Name);
        }
    }
}
