using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace SW4FED_1.Models
{
    public class Debtors : BindableBase
    {
        string? _name;
        //List<Debts>? _debt;
        double _totalDebt;

        public Debtors(string name, double totalDebt)
        {
            _name = name;
            _totalDebt = totalDebt;
   
        }

        public Debtors()
        {
           

        }

        public Debtors? Clone()
        {
            return this.MemberwiseClone() as Debtors;
        }


        double calcTotalDebt(List<Debts> _debt)
        {
            double sum = 1;
            return sum;
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                SetProperty(ref _name, value);
            }
        }
        public double TotalDebt
        {
            get
            {
                return _totalDebt;
            }
            set
            {
                SetProperty(ref _totalDebt, value);
            }
        }

        //public List<Debts> Debt
        //{
        //    get
        //    {
        //        return _debt;
        //    }
        //    set
        //    {
        //        SetProperty(ref _debt, value);
        //    }
        //}



    }

    



}
