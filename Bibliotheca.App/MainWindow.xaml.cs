using Bibliotheca.App.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bibliotheca.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            EnsureApplicationResources();
            InitializeComponent();
            this.DataContext = this;
            var mainViewModel = new MainViewModel();
            View = mainViewModel;
        }
        private object view;
        public object View
        {
            get
            {
                return view;
            }
            set
            {
                view = value;
                NotifyPropertyChanged();
            }
        }

        public void EnsureApplicationResources()
        {
            this.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/Resources/ResourcesMerge.xaml", UriKind.Relative)
            });
        }
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
