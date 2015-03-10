using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Viewer.Model
{
    public class DVDirectory : DVIOItem
    {
        #region Constructors
        public DVDirectory(string path)
            :base()
        {
            Path = path;           
            foreach (string file in Directory.EnumerateFiles(Path)) try { SubItems.Add(new DVFile(file)); } catch (Exception) { Console.WriteLine(String.Format("Unable to add {0}", file)); }
            foreach (string directory in Directory.EnumerateDirectories(Path)) try { SubItems.Add(new DVDirectory(directory)); }
                catch (Exception) { Console.WriteLine(String.Format("Unable to add {0}", directory)); }
            Size = SubItems.Sum(item => item.Size);
        }
        #endregion

        #region Methods  
        public static long GetDirectorySize(string currentDirectory, bool verbose)
        {
            string[] fileArray = Directory.GetFiles(currentDirectory, "*.*");
            string[] directoryArray = Directory.GetDirectories(currentDirectory);
            long totalSize = 0;

            foreach (string fileName in fileArray)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                totalSize += fileInfo.Length;
            }

            foreach (string directoryName in directoryArray)
            {
                totalSize += GetDirectorySize(directoryName, verbose);
            }
            if (verbose) Console.WriteLine(String.Format("{0} Size: {1}bytes", currentDirectory, totalSize));
            return totalSize;
        }

        public override void SetHeight(double value)
        {
            Height = value;
            Width = Size / Height;
            UpdateIOItems();
        }

        public override void SetWidth(double value)
        {
            Width = value;
            Height = Size / Width;
            UpdateIOItems();
        }

        public void UpdateIOItems()
        {
            foreach(DVIOItem item in SubItems)
            {
                if (AspectRatio < 1) item.SetHeight(Height);
                else item.SetWidth(Width);
            }
        }
        #endregion
    }
}
