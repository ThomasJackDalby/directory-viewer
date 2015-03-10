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

        public double XScale { get; set; }
        public double YScale { get; set; }

        public DVDirectoryVM RootDirectory { get; set; }
        #endregion

        #region Constructors
        public MainWindowVM(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            RootDirectory = new DVDirectoryVM(new DVDirectory(@"D:\"));
            RootDirectory.Model.SetAbstract(MainWindow.Width / MainWindow.Height);
            MainWindow.SizeChanged += SetScales;
            SetScales(null, null);
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
            RootDirectory.Model.SetAbstract(MainWindow.Width / MainWindow.Height);
            XScale = MainWindow.ActualWidth / RootDirectory.Width;
            YScale = MainWindow.ActualHeight / RootDirectory.Height;
            RootDirectory.SetShape(0, 0, XScale, YScale);
            AddDirectoryToCanvas(RootDirectory);
            OnPropertyChanged("RootDirectory");
        }


        #endregion
    }
}
