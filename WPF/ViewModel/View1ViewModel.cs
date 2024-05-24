using OneNoteVanilla5.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OneNoteVanilla5.WPF.ViewModel
{
    public class View1ViewModel: INotifyPropertyChanged
    {
        private string _currentNoteCount = "1000";

        private ObservableCollection<Note> _items = new ObservableCollection<Note>();

        public ObservableCollection<Note> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }


        public string CurrentNoteCount
        {
            get { return _currentNoteCount; }
            set
            {
                _currentNoteCount = value;
                OnPropertyChanged("CurrentNoteCount");
            }
        }

        public View1ViewModel()
        {
            Items.Add(new Note
            {
                Id = 1,
                Title = "Title",
                Description = "This is a test note",
                Content = "This is a test content"
            });
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
