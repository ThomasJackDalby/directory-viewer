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
        public DVFile CastModel 
        {
            get { return (DVFile)Model; }
            set { Model = value; }
        }

        public string Name
        {
            get { return CastModel.Name; }
            set { CastModel.Name = value; }
        }

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
            rectangle.ToolTip = new ToolTip() { Content = String.Format("{0}\n{1:n2}mb\n{2:n2}kb\n{3:n}b", CastModel.Name, Model.GetSizeInMegaBytes(), Model.GetSizeInKiloBytes(), Model.GetSizeInBytes()) };

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
