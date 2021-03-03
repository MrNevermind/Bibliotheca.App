using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Bibliotheca.App.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public delegate void CloseWindow();
        public CloseWindow closeWindow { get; set; }

        public Window GetWindow(int height = 600, int width = 400)
        {
            Window Window = new Window()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Width = width,
                Height = height,
            };
            Window.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/Resources/ResourcesMerge.xaml", UriKind.Relative)
            });

            closeWindow = new CloseWindow(Window.Close);

            return Window;
        }
        public object Tag { get; set; }
    }
}
