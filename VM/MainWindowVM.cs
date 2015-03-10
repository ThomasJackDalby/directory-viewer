using Directory_Viewer.Model;
using MvvM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Directory_Viewer.VM
{
    public class MainWindowVM : ObservableObject
    {
        #region Properties
        public MainWindow MainWindow { get; set; }
        public static MainWindowVM Instance { get; set; }


        public double XScale { get; set; }
        public double YScale { get; set; }

        private DVIOItemVM selectedItem;
        public DVIOItemVM SelectedItem 
        {
            get { return selectedItem; }
            set
            {
                if (SelectedItem != null) SelectedItem.DeSelect();
                selectedItem = value;
                if (SelectedItem != null) SelectedItem.Select();
            }
        }

        public DVDirectoryVM RootDirectory { get; set; }
        #endregion

        #region Constructors
        public MainWindowVM(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            Instance = this;
            RootDirectory = new DVDirectoryVM(new DVDirectory(@"D:\Git"));
            MainWindow.SizeChanged += SetScales;
            SetScales(null, null);
            OnPropertyChanged("RootDirectory");
        }
        #endregion

        #region Methods
        public void AddDirectoryToCanvas(DVIOItemVM currentItem)
        {
            MainWindow.Diagram.Children.Add(currentItem.Shape);
            foreach (DVIOItemVM item in currentItem.SubItems) AddDirectoryToCanvas(item);
        }

        public void SetScales(object obj, SizeChangedEventArgs e)
        {
            MainWindow.Diagram.Children.Clear();

            MainWindow.Diagram.Width = MainWindow.Width - MainWindow.TreeCol.Width.Value;
            MainWindow.Diagram.Height = MainWindow.Height;

            RootDirectory.Model.SetAbstract(MainWindow.Diagram.Width / MainWindow.Diagram.Height);
            XScale = MainWindow.Diagram.Width / RootDirectory.Width;
            YScale = MainWindow.Diagram.Height / RootDirectory.Height;
            RootDirectory.SetShape(0, 0, XScale, YScale);
            AddDirectoryToCanvas(RootDirectory);
            OnPropertyChanged("RootDirectory");
        }


        #endregion
    }
}
