using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace SW4FED_1.Models
{
    public class Debtors : BindableBase
    {
        string _name;
        List<Debts> _debt;
        double _totalDebt;
        private string v;
        private Debts debts;

        public Debtors(string name)
        {
            _name = name;
   
        }

        public Debtors()
        {
           

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
                SetProperty(ref _name, value); ;
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
                SetProperty(ref _totalDebt, value); ;
            }
        }



    }

    



}
