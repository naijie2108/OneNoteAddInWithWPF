using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OneNoteVanilla5.WPF.ViewModel
{
    public class View2ViewModel : INotifyPropertyChanged
    {
        private string _currentNoteCount = "0";

        public string CurrentNoteCount
        {
            get { return _currentNoteCount; }
            set
            {
                _currentNoteCount = value;
                OnPropertyChanged("CurrentNoteCount");
            }
        }

        public void ChangeCount(string count)
        {
            CurrentNoteCount = count;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
