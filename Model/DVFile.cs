using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Viewer.Model
{
    public class DVFile : DVIOItem
    {
        #region Constructors
        public DVFile(string path)
            :base()
        {
            Path = path;
            FileInfo fileInfo = new FileInfo(Path);
            Size = fileInfo.Length;
        }
        public override void SetHeight(double value)
        {
            Height = value;
            Width = Size / Height;
        }

        public override void SetWidth(double value)
        {
            Width = value;
            Height = Size / Width;
        }
        #endregion
    }
}
