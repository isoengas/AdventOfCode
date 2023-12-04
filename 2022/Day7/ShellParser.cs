namespace Day7
{
    public class ShellParser
    {
        private readonly Shell _shell;
        public ShellParser(Shell shell)
        {
            _shell = shell;
        }
        public void ParseLine(string line)
        {
            if (line.StartsWith('$'))
            {
                _shell.SendCommand(line.Substring(1).Trim());
            }
            else if (line.StartsWith("dir"))
            {
                var parts = line.Split(' ');
                _shell.AddDirectory(parts[1]);
            }
            else
            {
                var parts = line.Split(' ');
                _shell.AddFile(parts[1], int.Parse(parts[0]));
            }
        }
    }
}
