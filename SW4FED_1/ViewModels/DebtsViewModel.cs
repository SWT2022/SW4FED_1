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
        public DebtsViewModel(string title, Debtors debtor)
        {
           debts = new ObservableCollection<Debts>();

            Title = title;
            CurrentDebt = debtor;

        }

        private ObservableCollection<Debts> debts;

        public ObservableCollection<Debts> Debts
        {
            get { return debts; }
            set { SetProperty(ref debts, value); }
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

        Debts currentDebt;

        public Debts CurrentDebt
        {
            get { return currentDebt; }
            set
            {
                SetProperty(ref currentDebt, value);
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
                //if (string.IsNullOrWhiteSpace(CurrentDebtor.Name))
                //    isValid = false;
                return isValid;
            }
        }

        private DelegateCommand addValueCommand;
        public DelegateCommand AddValueCommand =>
            addValueCommand ?? (addValueCommand = new
            DelegateCommand(ExecuteAddValueCommand, CanExecuteAddValueCommand)
            .ObservesProperty(() => CurrentDebt.Debit));

        void ExecuteAddValueCommand()
        {
            var newDebit = new Debts("342", 1234);
            debts.Add(newDebit);
        }

        bool CanExecuteAddValueCommand()
        {
            return IsValid;
        }

        #endregion


    }
}

