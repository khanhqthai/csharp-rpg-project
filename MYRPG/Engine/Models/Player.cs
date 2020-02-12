using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Engine.Models
{

    /*  Note: INotifyPropertyChanged is an interface that we will use to update the changes to our View(MainWindow.xaml)
        When we change hitPoints, expPoints..etc.  The changes in are not reflected in our view.  This is wehre INotifyPropertyChanged comes in
        It notify the view that these values have changed.
    */
    public class Player : INotifyPropertyChanged
    {

        private String _name;
        private String _characterClass;
        private int _hitPoints;
        private int _expPoints;
        private int _level;
        private int _gold;



        public String name
        {
            get { return _name; }
            set
            {
                // everytime  we set/update our value for name
                // we call OnPropertyChanged to notify anything using the property that it has been changed
                _name = value;
                OnPropertyChanged("name");
            }
        }

        public String characterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged("characterClass");
            }
        }
        public int hitPoints
        {
            get { return _hitPoints; }
            set
            {
                _hitPoints = value;
                OnPropertyChanged("hitPoints");
            }
        }
        public int expPoints
        {
            get { return _expPoints; }
            set
            {
                _expPoints = value;
                OnPropertyChanged("expPoints");
            }
        }
        public int level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged("level");
            }
        }
        public int gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged("gold");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            // if anybody is listening to PropertyChanged, notify them that the property has changed
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
