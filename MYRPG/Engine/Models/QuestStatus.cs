using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{   
    /// <summary>
    /// Class to manage the status  of quests in the game
    /// by default all quests are not completed, we set to true when a player completes it
    /// Player class will use this class to build a list of quests player has, and check on the status
    /// </summary>
    public class QuestStatus : BaseNotificationClass
    {
        private bool _isCompleted;
        public Quest PlayerQuest { get; } // remove setter, only constructor will be able to set it. more encapsulation
        public bool IsCompleted 
        {
            get { return _isCompleted; }
            set 
            {
                _isCompleted = value;
                OnPropertyChanged();
            }
        }
        public QuestStatus(Quest quest) 
        {
            PlayerQuest = quest;
            IsCompleted = false;
        }
    }
}
