using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{

    /*  Note: INotifyPropertyChanged is an interface that we will use to update the changes to our View(MainWindow.xaml)
        When we change hitPoints, expPoints..etc.  The changes in are not reflected in our view.  This is wehre INotifyPropertyChanged comes in
        It notify the view that these values have changed.
    */
    public class Player : BaseNotificationClass
    {

        private String _name;
        private String _characterClass;
        private int _hitPoints;
        private int _expPoints;
        private int _level;
        private int _gold;



        public String Name
        {
            get { return _name; }
            set
            {
                // everytime  we set/update our value for name
                // we call OnPropertyChanged to notify anything using the property that it has been changed
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public String CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }
        public int HitPoints
        {
            get { return _hitPoints; }
            set
            {
                _hitPoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }
        }
        public int ExpPoints
        {
            get { return _expPoints; }
            set
            {
                _expPoints = value;
                OnPropertyChanged(nameof(ExpPoints));
            }
        }
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }
        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }

    }
}
