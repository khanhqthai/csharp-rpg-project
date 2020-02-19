using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; // import so we can use  INotifyPropertyChanged interface
namespace Engine
{
    /// <summary>
    /// Update class property changes to the view(XAML)
    /// </summary>
    /// <remarks>
    /// Whenever a function is used by multiple classes.
    /// it is a good idea to put it in a base class so everyone can use it.
    /// we will do this to the OnPropertyChanged function.
    /// this function notifies when a property is changed/updated,
    /// we use it to notify the XAML a property has been changed
    /// Note: INotifyPropertyChanged is the interface that we will use to update the changes to our View(MainWindow.xaml)
    /// When we change hitPoints, expPoints..etc.  The changes in are not reflected in our view.  This is where INotifyPropertyChanged comes in
    /// It will notify the view that these values have changed.
    /// </remarks> 
    public class BaseNotificationClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // protected -  visible only inside this class and classes derived from it.
        // virtual -  it can be overriden in derived classes aka children classes 
        protected virtual void OnPropertyChanged(string propertyName)
        {
            // if anybody is listening to PropertyChanged, notify them that the property has changed
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
