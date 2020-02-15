using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; // import so we can use  INotifyPropertyChanged interface
namespace Engine
{
    // When ever a function is used by multiple class.
    // it is a good idea to put it in a base class so everyone can use it.
    // we will do this to the INotifyPropertyChanged interface.
    // this function notifies when a property is changed/updated, we use it to tell the xaml a property has been changed, please update it up the view(xaml)
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
