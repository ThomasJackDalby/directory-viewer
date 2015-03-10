using Directory_Viewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Directory_Viewer.VM
{
    public class DVFileVM : DVIOItemVM
    {
        #region Constructors
        public DVFileVM(DVFile model)
            : base(model)
        { }
        #endregion

        #region Methods
        public override void SetShape(double top, double left, double xScale, double yScale)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Height = Model.Height * xScale;
            rectangle.Width = Model.Width * yScale;

            rectangle.SetValue(Canvas.TopProperty, top);
            rectangle.SetValue(Canvas.LeftProperty, left);
            Shape = rectangle;

            if (Model.Path.Contains(".jpg"))
            {
                rectangle.Stroke = Brushes.Green;
                rectangle.Fill = Brushes.LightGreen;
            }
            else if (Model.Path.Contains(".cs"))
            {
                rectangle.Stroke = Brushes.Red;
                rectangle.Fill = Brushes.LightPink;
            }
            else
            {
                rectangle.Stroke = Brushes.Blue;
                rectangle.Fill = Brushes.LightBlue;
            }
        }



        #endregion
    }
}
