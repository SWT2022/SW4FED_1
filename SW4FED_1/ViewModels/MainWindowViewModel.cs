using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using SW4FED_1.Models;
using SW4FED_1.Views;

namespace SW4FED_1.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        private readonly string AppTitle = "The dept book";
        private string filePath = "";

        public MainWindowViewModel()
        {
            
            Debtors = new ObservableCollection<Debtors>();
            Debtors.Add(new Debtors("Jan", 314.876));
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


        #endregion Properties



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
            var vm = new DebtorsViewModel("Add new Debtor", newDebtor);

            var dlg = new DebtorsView
            {
                DataContext = vm
            };
            if (dlg.ShowDialog() == true)
            {
                Debtors.Add(newDebtor);
                CurrentDebtor = newDebtor; // Or CurrentIndex = Agents.Count - 1;
                Dirty = true;
            }
        }

        private DelegateCommand deleteCommand;
        public DelegateCommand DeleteCommand =>
            deleteCommand ?? (deleteCommand = new DelegateCommand(ExecuteDeleteCommand, DeleteAgent_CanExecute)
                    .ObservesProperty(() => CurrentIndex));

        void ExecuteDeleteCommand()
        {
            MessageBoxResult res = MessageBox.Show("Are you sure you want to delete debtor " + CurrentDebtor.Name +
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

       
        private DelegateCommand _editDebitCommand;
        public DelegateCommand EditDebitCommand =>
            _editDebitCommand ?? (_editDebitCommand = new
            DelegateCommand(ExecuteEditDebitCommand, CanExecuteEditDebitCommand)
            .ObservesProperty(() => CurrentIndex));

        void ExecuteEditDebitCommand()
        {
            var newDebt = new Debts(DateTime.UtcNow.ToString("MM-dd-yyyy"), 0);
            var vm = new DebtsViewModel("Add new Debtor", CurrentDebtor, newDebt);

            var dlg = new DebtsView
            {
                DataContext = vm
            };
            if (dlg.ShowDialog() == true)
            {
                CurrentDebtor.TotalDebt += newDebt.Debit;
                CurrentDebtor.ListOfDebts.Add(newDebt);
                Dirty = true;
            }
        }

        bool CanExecuteEditDebitCommand()
        {
            return CurrentIndex >= 0;
        }

        DelegateCommand _NewFileCommand;
        public DelegateCommand NewFileCommand
        {
            get { return _NewFileCommand ?? (_NewFileCommand = new DelegateCommand(NewFileCommand_Execute)); }
        }

        private void NewFileCommand_Execute()
        {
            MessageBoxResult res = MessageBox.Show("Any unsaved data will be lost. Are you sure you want to initiate a new file?", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                Debtors.Clear();
                Filename = "";
                Dirty = false;
            }
        }

        DelegateCommand _OpenFileCommand;
        public DelegateCommand OpenFileCommand
        {
            get { return _OpenFileCommand ?? (_OpenFileCommand = new DelegateCommand(OpenFileCommand_Execute)); }
        }

        private void OpenFileCommand_Execute()
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = "dbt"
            };
            if (filePath == "")
                dialog.InitialDirectory =
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);
            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                Filename = Path.GetFileName(filePath);

                try
                {
                    Debtors = FileHandler.ReadFile(filePath);
                    Dirty = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        DelegateCommand _SaveAsCommand;
        public DelegateCommand SaveAsCommand
        {
            get { return _SaveAsCommand ?? (_SaveAsCommand = new DelegateCommand(SaveAsCommand_Execute)); }
        }

        private void SaveAsCommand_Execute()
        {
            var dialog = new SaveFileDialog
            {
                DefaultExt = "dbt"
            };
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                Filename = Path.GetFileName(filePath);
                SaveFile();
            }
        }

        DelegateCommand _SaveCommand;
        public DelegateCommand SaveCommand
        {
            get
            {
                return _SaveCommand ?? (_SaveCommand = new
                   DelegateCommand(SaveFileCommand_Execute, SaveFileCommand_CanExecute)
                  .ObservesProperty(() => Debtors.Count));
            }
        }

        private void SaveFileCommand_Execute()
        {
            SaveFile();
        }

        private bool SaveFileCommand_CanExecute()
        {
            return (filename != "") && (Debtors.Count > 0);
        }

        private void SaveFile()
        {
            try
            {
                FileHandler.SaveFile(filePath, Debtors);
                Dirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to save file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion



    }
}
