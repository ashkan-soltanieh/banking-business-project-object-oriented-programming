using System;
using System.Diagnostics;
using BankingBusiness;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankingBuisiness.TestingOOP
{
    [TestClass]
    public class TestDemo
    {
        [TestMethod]
        public void T02_001_Constructor1_ValidArguments()
        {
            CheckingAccount ca = new CheckingAccount(123456, "scott thornton", 0.55M);

        }

        [TestMethod]
        public void T02_021_Constructor3_InvalidRate1()
        {
            string[] args = { "123456", "scott thornton", "-0.55", "987.65", "675.78" }; ;
            CheckingAccount ca = new CheckingAccount(args);
        }
        [TestMethod]
        public void T02_027_Withdraw_InvalidArguments1()
        {
            string[] args = { "123456", "scott thornton", "0.55", "1000", "100.00" };
            CheckingAccount ca = new CheckingAccount(args);
            ca.Withdraw(-123.45M);
        }

        [TestMethod]
        public void T02_030_AddMonthlyInterest_WasNegative()
        {
            string[] args = { "123456", "scott thornton", "0.055", "1000", "100.00" };
            CheckingAccount ca = new CheckingAccount(args);
            ca.Withdraw(1050M);
            ca.AddMonthlyInterest();
            Debug.WriteLine(ca.Balance);
        }
    }
}
