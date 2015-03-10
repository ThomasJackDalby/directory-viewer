using Directory_Viewer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Directory_Viewer.VM
{
    public class DVDirectoryVM : DVIOItemVM
    {
        public DVDirectory CastModel 
        {
            get { return (DVDirectory)Model; }
            set { Model = value; }
        }

        public string Name
        {
            get { return CastModel.Name; }
            set { CastModel.Name = value; }
        }

        #region Constructors
        public DVDirectoryVM(DVDirectory model)
            : base(model)
        { }
        #endregion

        #region Methods
        public override void SetShape(double top, double left, double xScale, double yScale)
        {
            double currentTop = top;
            double currentLeft = left;

            foreach (DVIOItemVM item in SubItems)
            {
                item.SetShape(currentTop, currentLeft, xScale, yScale);
                if (item.Height == Height) currentLeft += item.Width * xScale;
                else currentTop += item.Height * yScale;   
            }

            Rectangle rectangle = new Rectangle();
            rectangle.Width = Model.Width * xScale;
            rectangle.Height = Model.Height * yScale;
            rectangle.Stroke = Brushes.Black;
            rectangle.StrokeThickness = 2;
            rectangle.SetValue(Canvas.TopProperty, top);
            rectangle.SetValue(Canvas.LeftProperty, left);
            rectangle.MouseDown += Select;
            Shape = rectangle;
        }
        #endregion
    }
}
