using System;
using System.Collections.Generic;
using System.Text;

namespace BankingBusiness
{
    public class SavingsAccount : Account
    {
        bool isBelowMinimumBalance = false; //to keep most recent balance status since last Deposit or withdrawal
        
        //Properties
        
        private decimal minimumBalance;
        public decimal MinimumBalance
        {
            get
            {
                return minimumBalance;
            }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException("Non-Positive Minimum Balance amount");
                }
                if(Balance < value)
                {
                    isBelowMinimumBalance = true;
                }
                else
                {
                    isBelowMinimumBalance = false;
                }
                minimumBalance = value;
            }
        }

        //Constructors

        public SavingsAccount(long acctNumber, string acctName, decimal rate) : base(acctNumber, acctName, rate)
        {
            MinimumBalance = 0;
        }

        public SavingsAccount(long acctNumber, string acctName, decimal rate, decimal minimumBalance) 
            : base(acctNumber, acctName, rate)
        {
            MinimumBalance = minimumBalance;
        }

        public SavingsAccount(string[] values): base(values)
        {
            if (values.Length < 5)
            {
                throw new IndexOutOfRangeException("Account Information Is Incomplete");
            }

            MinimumBalance = decimal.Parse(values[4]);
        }

        //Methods

        public override void Deposit(decimal amount)
        {
            base.Deposit(amount);
            if (Balance < MinimumBalance)
            {
                isBelowMinimumBalance = true;
            }
            else
            {
                isBelowMinimumBalance = false;
            }
        }
        public override void Withdraw(decimal amount)
        {
            base.Withdraw(amount);
            if (Balance < MinimumBalance)
            {
                isBelowMinimumBalance = true;
            }
            else
            {
                isBelowMinimumBalance = false;
            }
        }
        public override void AddMonthlyInterest()
        {
            if(!isBelowMinimumBalance)
            {
                base.AddMonthlyInterest();
            }
        }

        public override string ToString()
        {
            StringBuilder userDisplay = new StringBuilder();
            userDisplay.AppendLine($"Account Type: {"Savings",20}");
            userDisplay.AppendLine(base.ToString());
            userDisplay.AppendLine($"Minimum Balance: {MinimumBalance,20:C2}");
            return userDisplay.ToString();
        }

        public override string ToFile()
        {
            string condensed = base.ToFile() + $"|{MinimumBalance:N2}";
            return condensed;
        }
    }
}
