
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
            //Regex regex = new Regex(@"^(?<dir>[\-ld])(?<permission>([\-r][\-w][\-xs]){3})\s+(?<filecode>\d+)\s+(?<owner>\w+)\s+(?<group>\w+)\s+(?<size>\d+)\s+(?<timestamp>((?<month>\w{3})\s+(?<day>\d{2})\s+(?<hour>\d{1,2}):(?<minute>\d{2}))|((?<month>\w{3})\s+(?<day>\d{1,2})\s+(?<year>\d{4})))\s+(?<name>.+)$");
            Regex regex = new Regex(@"^(?<dir>[\-ld])(?<permission>([\-r][\-w][\-xs]){3})\s+(?<filecode>\d+)\s+(?<owner>\w+)\s+(?<group>\w+)\s+(?<size>\d+)\s+(?<timestamp>((?<month>\w{3})\s+(?<day>\d{2})\s+(?<hour>\d{1,2}):(?<minute>\d{2}))|((?<month>\w{3})\s+(?<day>\d{2})\s+(?<year>\d{4})))\s+(?<name>.+)$");
            //MatchCollection matches =  regex.Matches(line);
            var matches = regex.Match(line);

            foreach (var match in matches.Groups)
            {
                var x = match;
            }


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
            Name = parts[8];                 
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}", Permissions, FileCode, Owner, Group, Size, Month, Day, Time, Name);
        }
    }
}
