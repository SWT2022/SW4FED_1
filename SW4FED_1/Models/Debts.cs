using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace SW4FED_1.Models
{
    public class Debts : BindableBase
    {
        string _date;
        double _debit;

        public Debts(string date, double debit)
        {
            _date = date;
            _debit = debit;
        }
        public Debts()
        {
            
        }

        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                SetProperty(ref _date, value); ;
            }
        }

        public double Debit  
        {
            get
            {
                return _debit;
            }
            set
            {
                SetProperty(ref _debit, value); ;
            }
        }

    }



}
