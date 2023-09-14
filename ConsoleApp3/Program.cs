using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int filesQuantity = 0;
            long folderSize = 0;
            const string address = "C:/Users/rupakal/Documents/SF_module_8";
            folderSize = GetFolderSize(address, ref filesQuantity);
            Console.WriteLine("Initial folder size is " + folderSize);
            CleanDirectory(address);
            Console.WriteLine("{0} files deleted", filesQuantity);
            Console.WriteLine("{0} bytes were freed", folderSize);
            folderSize = GetFolderSize(address, ref filesQuantity);
            Console.WriteLine("Current folder size is " + folderSize);
            Console.ReadKey();

        }
        static long GetFolderSize(string address, ref int filesQuantity)
        {
            long size = 0;
            try
            {
                var directory = new DirectoryInfo(address);
                FileInfo[] files = directory.GetFiles();

                string[] directories = Directory.GetDirectories(address);
                filesQuantity += files.Length;
                foreach (var file in files)
                {
                    size += file.Length;
                    //Console.WriteLine(file.Name + " " + file.Length);
                }
                if (directories.Length > 0)
                {
                    foreach (var dr in directories)
                    {
                        size += GetFolderSize(dr,ref filesQuantity);
                    }
                }
                return size;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;

        }
        static void CleanDirectory(string directory)
        {
            if (Directory.Exists(directory))
            {
                string[] files = Directory.GetFiles(directory);
                for (int i = 0; i < files.Length; i++)
                {
                    if ((DateTime.Now - File.GetLastAccessTime(files[i])) >= TimeSpan.FromMinutes(0))
                    {
                        try
                        {
                            File.Delete(files[i]);
                            //Console.WriteLine("File deleted");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                string[] directories = Directory.GetDirectories(directory);
                for (int i = 0; i < directories.Length; i++)
                {
                    if ((DateTime.Now - Directory.GetLastAccessTime(directories[i])) >= TimeSpan.FromMinutes(0))
                    {
                        try
                        {
                            Directory.Delete(directories[i], true);
                            //Console.WriteLine("Directory deleted");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No folder fouded with the specific name {0}", directory);
                return;
            }
        }


    }
}
