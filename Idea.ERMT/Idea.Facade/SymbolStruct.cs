namespace Idea.Facade
{
    public struct SymbolStruct
    {
        private string directory;
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Type { get; set; }
        public string Directory
        {
            get
            {
                if (directory.Length == 0)
                    directory = Name.Replace(" ", "") + ".PNG";
                return directory;
            }
            set { directory = value; }
        }
    }
}
