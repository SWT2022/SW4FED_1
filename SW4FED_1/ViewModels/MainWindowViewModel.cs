using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using SW4FED_1.Models;

namespace SW4FED_1.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        private readonly string AppTitle = "The dept book";
        private string filePath = "";

        public MainWindowViewModel()
        {
            Debtors = new ObservableCollection<Debtors>();
            Debtors.Add(new Debtors("Jan"));
            CurrentDebtor = Debtors[0];

        }

        #region Properties

        private ObservableCollection<Debtors> debtors;
        public ObservableCollection<Debtors> Debtors
        {
            get { return debtors; }
            set { SetProperty(ref debtors, value); }
        }

        private Debtors currentDebtor;
        public Debtors CurrentDebtor
        {
            get { return currentDebtor; }
            set { SetProperty(ref currentDebtor, value); }
        }

        private int currentIndex;
        public int CurrentIndex
        {
            get { return currentIndex; }
            set { SetProperty(ref currentIndex, value); }
        }

        private string filename = "";
        public string Filename
        {
            get { return filename; }
            set
            {
                SetProperty(ref filename, value);
                RaisePropertyChanged("Title");
            }
        }

        public string Title
        {
            get
            {
                var s = "";
                if (Dirty)
                    s = "*";
                return Filename + s + " - " + AppTitle;
            }
        }

        private bool dirty = false;
        public bool Dirty
        {
            get { return dirty; }
            set
            {
                SetProperty(ref dirty, value);
                RaisePropertyChanged("Title");
            }
        }

        private Debts debt;

        public Debts Debt
        {
            get { return debt; }
        }

        #endregion Properties

        #region Methods

        #endregion

        #region Commands

        private DelegateCommand _previusCommand;
        public DelegateCommand PreviusCommand =>
            _previusCommand ?? (_previusCommand = new DelegateCommand(ExecutePreviusCommand, CanExecutePreviusCommand)
            .ObservesProperty(() => CurrentIndex));

        void ExecutePreviusCommand()
        {
            if (CurrentIndex > 0)
                --CurrentIndex;
        }

        bool CanExecutePreviusCommand()
        {
            if (CurrentIndex > 0)
                return true;
            else
                return false;
        }

        private DelegateCommand _nextCommand;
        public DelegateCommand NextCommand =>
            _nextCommand ?? (_nextCommand = new DelegateCommand(ExecuteNextCommand, CanExecuteNextCommand)
            .ObservesProperty(() => CurrentIndex));

        void ExecuteNextCommand()
        {
            if (CurrentIndex < (Debtors.Count - 1))
                CurrentIndex++;
        }

        bool CanExecuteNextCommand()
        {
            if (CurrentIndex < (Debtors.Count - 1))
                return true;
            else
                return false;
        }

        private DelegateCommand addCommand;
        public DelegateCommand AddCommand =>
            addCommand ?? (addCommand = new DelegateCommand(ExecuteAddCommand));

        void ExecuteAddCommand()
        {
            var newDebtor = new Debtors();
            //var vm = new AgentViewModel("Add new agent", newAgent);

            //var dlg = new AgentView
            //{
            //    DataContext = vm
            //};
            //if (dlg.ShowDialog() == true)
            //{
            //    Agents.Add(newAgent);
            //    CurrentAgent = newAgent; // Or CurrentIndex = Agents.Count - 1;
            //    Dirty = true;
            //}
        }

        private DelegateCommand deleteCommand;
        public DelegateCommand DeleteCommand =>
            deleteCommand ?? (deleteCommand = new DelegateCommand(ExecuteDeleteCommand, DeleteAgent_CanExecute)
                    .ObservesProperty(() => CurrentIndex));

        void ExecuteDeleteCommand()
        {
            MessageBoxResult res = MessageBox.Show("Are you sure you want to delete agent " + CurrentDebtor.Name +
                "?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                Debtors.Remove(CurrentDebtor);
                Dirty = true;
            }
        }

        private bool DeleteAgent_CanExecute()
        {
            if (Debtors.Count > 0 && CurrentIndex >= 0)
                return true;
            else
                return false;
        }

        private DelegateCommand closeAppCommand;
        public DelegateCommand CloseAppCommand =>
            closeAppCommand ?? (closeAppCommand = new DelegateCommand(ExecuteCloseAppCommand));

        void ExecuteCloseAppCommand()
        {
            Application.Current.MainWindow.Close();
        }

        #endregion

    }
}
