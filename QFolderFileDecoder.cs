namespace QFolder
{
    internal class QFolderFileDecoder
    {
        private readonly List<QItemD> dirs;
        private readonly List<QItemF> files;
        private string source = "output.qpack";

        public void Open(string path)
        {
            if (Directory.Exists(path)) source = path;
        }

        public void Load()
        {
            using BinaryReader br = new(File.OpenRead(source));
            int dirsL = br.ReadInt32();
            for(int i = 0; i < dirsL; i++)
            {
                string name = "";
                int nameL = br.ReadInt32();
                for(int j = 0; j < nameL; j++)
                {
                    name += (char)br.ReadInt16();
                }
                dirs.Add(new(name, br.ReadInt32()));
            }
            try
            {
                while (true)
                {
                    string name = "";
                    int nameL = br.ReadInt32();
                    for (int i = 0; i < nameL; i++)
                    {
                        name += (char)br.ReadInt16();
                    }
                    byte[] data = br.ReadBytes(br.ReadInt32());
                    files.Add(new(name, br.ReadInt32(), data));
                }
            }
            catch (EndOfStreamException) { }
        }

        public void Make()
        {
            RecursiveMake(-1);
        }

        public static void Unpack(string source = "output.qpack")
        {
            QFolderFileDecoder file = new();
            file.Open(source);
            file.Load();
            file.Make();
        }

        private void RecursiveMake(int DID)
        {
            string path;
            if (DID == -1) path = source[..source.LastIndexOf(".qpack")];
            else
            {
                path = source[..source.LastIndexOf(".qpack")] + '/' + GetPath(DID);
            }
            Directory.CreateDirectory(path);
            for(int i = 0; i < dirs.Count; i++)
            {
                if (dirs[i].parentD == DID) RecursiveMake(i);
            }
            for (int i = 0; i < files.Count; i++)
            {
                if (files[i].parentD == DID)
                {
                    using BinaryWriter bw = new(File.Create(path + '/' + files[i].name));
                    bw.Write(files[i].data);
                }
            }
        }

        private string GetPath(int DID)
        {
            int PID = DID;
            string name = dirs[PID].name;
            while (true)
            {
                PID = dirs[PID].parentD;
                if (PID == -1) break;
                name = dirs[PID].name + '/' + name;
            }
            return name;
        }

        public QFolderFileDecoder()
        {
            dirs = new List<QItemD>();
            files = new List<QItemF>();
        }
    }
}
