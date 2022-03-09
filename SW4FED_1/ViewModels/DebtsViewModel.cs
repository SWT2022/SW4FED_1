using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW4FED_1.Models;
using System.Collections.ObjectModel;
using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using Prism.Services.Dialogs;

namespace SW4FED_1.ViewModels
{
    internal class DebtsViewModel : BindableBase
    {
        public DebtsViewModel(string title, Debtors debtor, Debts debts)
        {

            Title = title;
            CurrentDebtor = debtor;
            NewDebt = debts;
           

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

        Debts newDebt;
        public Debts NewDebt
        {
            get { return newDebt; }
            set { SetProperty(ref newDebt, value); }
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
                //if (string.IsNullOrWhiteSpace(CurrentDebtor.Name))
                //    isValid = false;
                return isValid;
            }
        }

        private DelegateCommand addValueCommand;
        public DelegateCommand AddValueCommand =>
            addValueCommand ?? (addValueCommand = new
            DelegateCommand(ExecuteAddValueCommand, CanExecuteAddValueCommand)
            .ObservesProperty(() => NewDebt.Debit)
            );

        void ExecuteAddValueCommand()
        {
            
        }

        bool CanExecuteAddValueCommand()
        {
            return IsValid;
        }

        #endregion


    }
}

