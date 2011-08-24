using System;
using System.IO;
using FlagLib.IO;

namespace FlagLib.ConsoleTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string sourcePath = @"C:\Users\Dennis\Sync Test Source\Matrix.mkv";
            string targetPath = @"C:\Users\Dennis\Sync Test Target\Matrix.mkv";

            using (FileStream sourceStream = new FileInfo(sourcePath).OpenRead())
            {
                using (FileStream targetStream = File.Create(targetPath))
                {
                    StreamCopyOperation operation = new StreamCopyOperation(sourceStream, targetStream, 512 * 1024, true);

                    operation.CopyProgressChanged += (sender, e) =>
                        {
                            System.Console.WriteLine(DateTime.Now.ToLongTimeString() + " " + e.AverageSpeed / 1024 / 1024 + " MB/s " + e.TotalCopiedBytes / 1024 / 1024 + " MB");
                        };

                    operation.CopyStream();
                }
            }
        }
    }
}