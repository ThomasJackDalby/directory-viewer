using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Viewer.Model
{
    public abstract class DVIOItem
    {
        #region Properties
        public string Path { get; set; }
        public double Size { get; set; }

        public double Width { get; set; }    
        public double Height { get; set; }
        public double AspectRatio { get { return Height / Width; } }

        public List<DVIOItem> SubItems { get; set; }
        #endregion

        #region Constructors
        public DVIOItem()
        {
            SubItems = new List<DVIOItem>();
        }
        #endregion

        public abstract void SetHeight(double value);

        public abstract void SetWidth(double value);

        public void SetAbstract(double ratio)
        {
            // size = width * height
            // aspect = height / width
            // width = size / height
            // width = size / (aspect * width)
            // width *width * aspect = size
            // width = (size/aspect)^0.5

            SetWidth(Math.Sqrt(Size * ratio));
        }
    }
}
