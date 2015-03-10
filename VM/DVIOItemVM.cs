using Directory_Viewer.Model;
using MvvM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Directory_Viewer.VM
{
    public abstract class DVIOItemVM : ObservableObject
    {
        public DVIOItem Model { get; set; }

        public Rectangle Shape { get; set; }

        public ObservableCollection<DVIOItemVM> SubItems { get; set; }

        public double Size
        {
            get { return Model.Size; }
            set { Model.Size = value; }
        }

        public double Height
        {
            get { return Model.Height; }
            set { Model.Height = value; }
        }

        public double Width
        {
            get { return Model.Width; }
            set { Model.Width = value; }
        }

        public DVIOItemVM(DVIOItem model)
        {
            Model = model;

            SubItems = new ObservableCollection<DVIOItemVM>();

            foreach (DVIOItem item in Model.SubItems)
            {
                if (item.GetType() == typeof(DVFile)) SubItems.Add(new DVFileVM((DVFile)item));
                else if (item.GetType() == typeof(DVDirectory)) SubItems.Add(new DVDirectoryVM((DVDirectory)item));
                else throw new Exception();
            }

            SubItems = new ObservableCollection<DVIOItemVM>(SubItems.OrderByDescending((x => x.Size)).ToList());
            OnPropertyChanged("SubItems");
        }

        public abstract void SetShape(double top, double left, double xScale, double yScale);

        public void Select(object obj, MouseEventArgs e)
        {
            MainWindowVM.Instance.SelectedItem = this;
        }

        public void Select()
        {
            Shape.StrokeThickness = 10;
            Shape.SetValue(Canvas.ZIndexProperty, int.MaxValue);
        }

        public void DeSelect()
        {
            Shape.StrokeThickness = 2;
            Shape.SetValue(Canvas.ZIndexProperty, 0);
        }

    }
}
