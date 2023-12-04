namespace Day7
{
    public class Directory
    {
        private readonly List<Directory> _subdirectories = new List<Directory>();
        private readonly List<File> _files = new List<File>();
        public Directory(string name, Directory? parent)
        {
            Name = name;
            Parent = parent;
        }

        public int GetTotalSize()
        {
            return Walk().Sum(f => f.GetMySize());
        }

        public int GetMySize()
        {
            return _files.Sum(f => f.Size);
        }

        public IEnumerable<Directory> Walk()
        {
            yield return this;
            foreach (var subdir in _subdirectories.SelectMany(d => d.Walk()))
            {
                yield return subdir;
            }
        }

        public Directory? Parent { get; }

        public Directory Get(string name)
        {
            return _subdirectories.Single(d => d.Name == name);
        }

        public string Name { get; }

        public Directory AddSubDirectory(string name)
        {
            var subdirectory = new Directory(name, this);
            _subdirectories.Add(subdirectory);
            return subdirectory;
        }

        public File AddFile(string name, int size)
        {
            var file = new File(name, size);
            _files.Add(file);
            return file;
        }
    }
}
