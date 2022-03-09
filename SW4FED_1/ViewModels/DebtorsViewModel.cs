using SW4FED_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using System.Windows.Input;
using System.Windows;


namespace SW4FED_1.ViewModels
{
    public class DebtorsViewModel : BindableBase
    {
        public DebtorsViewModel(string title, Debtors debtor)
        {
            Title = title;
            CurrentDebtor = debtor;


        }

        #region Properties
        string title;
        public string Title
        {
            get { return title; }
            set
            {
                SetProperty(ref title, value);
            }
        }

        Debtors currentDebtor;

        public Debtors CurrentDebtor
        {
            get { return currentDebtor; }
            set
            {
                SetProperty(ref currentDebtor, value);
            }
        }
        public bool IsValid
        {
            get
            {
                bool isValid = true;
                if (string.IsNullOrWhiteSpace(CurrentDebtor.Name))
                    isValid = false;
                return isValid;
            }
        }
        ICommand _okBtnCommand;
        public ICommand OkBtnCommand
        {
            get
            {
                return _okBtnCommand ?? (_okBtnCommand = new DelegateCommand(
                    OkBtnCommand_Execute, OkBtnCommand_CanExecute)
                  .ObservesProperty(() => CurrentDebtor.Name)
                  .ObservesProperty(() => CurrentDebtor.TotalDebt));
            }
        }

        private void OkBtnCommand_Execute()
        {
           
        }

        private bool OkBtnCommand_CanExecute()
        {
            return IsValid;
        }
        #endregion
    }
}
