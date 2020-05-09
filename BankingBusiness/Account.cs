using System;
using System.Text;

namespace BankingBusiness
{
    public class Account
    {
        //Properties

        private readonly long accountNumber; //to be able to assign it on other classes string constructor I had no other option

        public long AccountNumber
        {
            get
            {
                return accountNumber;
            }
        }

        private string accountName;
        public string AccountName
        {
            get
            {
                return accountName;
            }
            set
            {
                if (value == null || (value.Trim().Length == 0))
                {
                    throw new ArgumentOutOfRangeException("Empty or blank name");
                }

                accountName = value.Trim();
            }
        }

        private decimal interestRate;
        public decimal InterestRate
        {
            get
            {
                return interestRate;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Non-Positive Interest Rate");
                }

                interestRate = value / 100;
            }
        }
        private decimal balance;
        public decimal Balance 
        { 
            get
            {
                return balance;
            }
            set
            {
                balance = value;
            }
        }
        
        //Constructors

        public Account(long acctNumber, string acctName, decimal rate)
        {
            if(acctNumber.ToString().Length < 5 || acctNumber.ToString().Length > 10 || acctNumber.ToString().StartsWith("0"))
            {
                throw new ArgumentOutOfRangeException("Unacceptable Account Number");
            }
            accountNumber = acctNumber;
            AccountName = acctName;
            InterestRate = rate;
            Balance = 0;
        }

        public Account(string[] values)
        {
            if(values.Length < 4)
            {
                throw new IndexOutOfRangeException("Account Information Is Incomplete");
            }
            if (values[0].Length < 5 || values[0].Length > 10 || values[0].StartsWith("0"))
            {
                throw new ArgumentOutOfRangeException("Unacceptable Account Number");
            }
            accountNumber = long.Parse(values[0]);
            AccountName = values[1];
            InterestRate = decimal.Parse(values[2]);
            Balance = decimal.Parse(values[3]);
        }

        //Methods

        virtual public void Deposit(decimal amount)
        {
            if(amount <= 0)
            {
                throw new ArgumentOutOfRangeException("Unacceptable Deposit Amount");
            }

            Balance += amount;
        }

        virtual public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("Unacceptable Withdraw Amount");
            }

            if((Balance - amount) < 0)
            {
                throw new ArgumentOutOfRangeException("Withdraw Amount Exceeded Current Balance");
            }

            Balance -= amount;
        }

        virtual public void AddMonthlyInterest()
        {
            Balance += (1 + InterestRate);
        }
        public override string ToString()
        {
            StringBuilder userDisplay = new StringBuilder();
            userDisplay.AppendLine($"Account Name: {AccountName,20}");
            userDisplay.AppendLine($"Account Number: {AccountNumber,20}");
            userDisplay.AppendLine($"Balance: {Balance, 20:C2}");
            userDisplay.AppendLine($"Interest Rate: {InterestRate,19:N2}%");
            return userDisplay.ToString();
        }

        virtual public string ToFile()
        {
            string condensed = $"{AccountNumber}|{AccountName}|{InterestRate:N2}|{Balance:N2}";
            
            return condensed;
        }
    }   
}
