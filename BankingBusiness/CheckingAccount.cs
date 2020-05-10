using System;
using System.Collections.Generic;
using System.Text;

namespace BankingBusiness
{
    public class CheckingAccount : Account
    {
        bool isNegativeBalance = false;   //to keep most recent balance status after deposit or withdraw
        
        //Properties

        private decimal overdraftLimit;
        public decimal OverdraftLimit
        {
            get
            {
                return overdraftLimit;
            }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException("Non-Positive Overdraft Limit amount");
                }
                if(Balance < 0)
                {
                    isNegativeBalance = true;
                }
                else
                {
                    isNegativeBalance = false;
                }
                overdraftLimit = value;
            }
        }

        //Constructors

        public CheckingAccount(long acctNumber, string acctName, decimal rate) 
                : base(acctNumber, acctName, rate)
        {
            OverdraftLimit = 0;
        }

        public CheckingAccount(long acctNumber, string acctName, decimal rate, decimal overdraftLimit) 
                : base(acctNumber, acctName, rate)
        {
            OverdraftLimit = overdraftLimit;
        }

        public CheckingAccount(string[] values) : base(values)
        {
            if (values.Length < 5)
            {
                throw new IndexOutOfRangeException("Account Information Is Incomplete");
            }

            OverdraftLimit = decimal.Parse(values[4]);
        }

        //Methods

        public override void Deposit(decimal amount)
        {
            base.Deposit(amount);
        }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("Unacceptable Withdraw Amount");
            }

            if ((Balance - amount) < 0)
            {
                if((-1) * (Balance - amount) > OverdraftLimit)
                {
                    throw new ArgumentOutOfRangeException("Whithdrawal amount exceeds the Maximum Overdraft");
                }

                Balance -= amount;
                isNegativeBalance = true;
            }
            else
            {
                Balance -= amount;
            }
        }

        public override void AddMonthlyInterest()
        {
            if (!isNegativeBalance)
            {
                base.AddMonthlyInterest();
            }
            else if(Balance > 0)
            {
                isNegativeBalance = false;
            }
        }

        public override string ToString()
        {
            StringBuilder userDisplay = new StringBuilder();
            userDisplay.AppendLine($"Account Type: {"Checking",20}");
            userDisplay.AppendLine(base.ToString());
            userDisplay.AppendLine($"Overdraft Limit: {OverdraftLimit,20:C2}");
            return userDisplay.ToString();
        }

        public override string ToFile()
        {
            string condensed = base.ToFile() + $"|{OverdraftLimit:N2}";
            return condensed;
        }
    }
}
