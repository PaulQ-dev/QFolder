using System.IO;

namespace QFolder
{
    public class QFolderFileEncoder
    {
        private readonly List<QItemD> dirs;
        private readonly List<QItemF> files;
        private string source = "source";

        public void Open(string path)
        {
            if (Directory.Exists(path)) source = path;
        }

        public void Build()
        {
            RecursiveBuild(source, -1);
        }

        public void Store(string path)
        {
            using BinaryWriter bw = new(File.Create(path));
            bw.Write(dirs.Count);
            foreach (QItemD itemD in dirs)
            {
                bw.Write(itemD.name.Length);
                foreach (char @char in itemD.name)
                {
                    bw.Write((ushort)@char);
                }
                bw.Write(itemD.parentD);
            }
            foreach (QItemF itemF in files)
            {
                bw.Write(itemF.name.Length);
                foreach (char @char in itemF.name)
                {
                    bw.Write((ushort)@char);
                }
                bw.Write(itemF.data.Length);
                bw.Write(itemF.data);
                bw.Write(itemF.parentD);
            }
        }

        public static void Pack(string source = "source", string output = "output.qpack")
        {
            QFolderFileEncoder file = new();
            file.Open(source);
            file.Build();
            file.Store(output);
        }

        private void RecursiveBuild(string path, int DID)
        {
            string[] dirs = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            foreach (string dir in dirs)
            {
                this.dirs.Add(new(dir[(path.Length + 1)..],DID));
                RecursiveBuild(dir, this.dirs.Count - 1);
            }
            foreach (string file in files)
            {
                this.files.Add(new(file[(path.Length + 1)..], DID, GetData(file)));
            }
        }

        private static byte[] GetData(string path)
        {
            byte[] data;
            using (BinaryReader br = new(File.OpenRead(path)))
            {
                data = new byte[br.BaseStream.Length];
                for (int i = 0; i < br.BaseStream.Length; i++)
                {
                    data[i] = br.ReadByte();
                }
            }
            return data;
        }

        public QFolderFileEncoder()
        {
            dirs = new List<QItemD>();
            files = new List<QItemF>();
        }
    }
}
