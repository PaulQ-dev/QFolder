using QFolder;
namespace Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to QFolders ver.1");
            bool run = true;
            while (run)
            {
                Console.Write("Operation (Pack, List, Unpack, Exit)?\n>");
                switch (Console.ReadLine()!.ToLower())
                {
                    case "pack":
                        //while (true)
                        {
                            Console.Write("Folder name?\n>");
                            string name = Console.ReadLine()!;
                            if (File.Exists(name+".qpack")) {
                                QFolderFileEncoder.Pack(name, name + ".qpack");
                                Console.WriteLine($"Done! Save as {name}.qpack");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Folder doesn't exists");
                            }
                        }
                        break;
                    case "tree":
                        Console.WriteLine("N/I");
                        break;
                    case "list":
                        //while (true)
                        {
                            Console.Write("Folders or Qpacks(F/Q)\n>");
                            string type = Console.ReadLine()!;
                            if(type == "F")
                            {
                                foreach(string dir in Directory.GetDirectories(Directory.GetCurrentDirectory()))
                                {
                                    Console.WriteLine(dir);
                                }
                                break;
                            }
                            if(type == "Q")
                            {
                                foreach(string pack in Directory.GetFiles(Directory.GetCurrentDirectory()))
                                {
                                    if(pack.Contains('.') && (pack[(pack.Length-6)..]) == ".qpack")
                                    Console.Write( pack + '\n');
                                }
                                break;
                            }
                        }
                        break;
                    case "unpack":
                        //while (true)
                        {
                            Console.Write("File name (without extension)?\n>");
                            string name = Console.ReadLine()!;
                            if (File.Exists(name + ".qpack"))
                            {
                                QFolderFileDecoder.Unpack(name+".qpack");
                                Console.WriteLine($"Done! Save as {name}");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("File doesn't exists");
                            }
                        }
                        break;
                    case "exit":
                        run = false;
                        break;
                }
            }
        }
    }
}