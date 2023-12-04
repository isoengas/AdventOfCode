namespace Day7
{
    public class Shell
    {
        private readonly Directory _root;
        public Shell()
        {
            _root = new Directory("/", null);
            CurrentDirectory = _root;
        }

        public Directory CurrentDirectory { get; private set; }
        
        public void GotoRoot()
        {
            CurrentDirectory = _root;
        }

        public void AddDirectory(string name)
        {
            CurrentDirectory.AddSubDirectory(name);
        }

        public void AddFile(string name, int size)
        {
            CurrentDirectory.AddFile(name, size);
        }

        public void Cd(string directoryName)
        {
            CurrentDirectory = CurrentDirectory.Get(directoryName);
        }

        public void CdParent()
        {
            CurrentDirectory = CurrentDirectory.Parent ?? _root;
        }

        internal void SendCommand(string command)
        {
            if (command == "cd /")
            {
                GotoRoot();
            }
            else if (command == "cd ..")
            {
                CdParent();
            }
            else if (command.StartsWith("cd"))
            {
                var target = command.Split(' ')[1];
                Cd(target);
            }
        }
    }
}
