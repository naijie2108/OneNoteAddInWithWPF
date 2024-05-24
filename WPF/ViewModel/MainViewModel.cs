using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace OneNoteVanilla5.WPF.ViewModel
{
    /** 
     * This View Model acts as a "router". It is binded to MainWindow, 
     * and controls the rendered view through the CurentViewModel property 
     * **/
    public class MainViewModel: INotifyPropertyChanged
    {
        private object _currentViewModel = new View1ViewModel();

        public object CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }

        public ICommand ShowView1Command { get; }
        public ICommand ShowView2Command { get; }

        public MainViewModel()
        {
            ShowView1Command = new RelayCommand(_ => ShowView(new View1ViewModel()));
            ShowView2Command = new RelayCommand(_ => ShowView(new View2ViewModel()));
        }

        private void ShowView(object ViewModel)
        {
            CurrentViewModel = ViewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
