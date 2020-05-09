using System;
using System.Diagnostics;
using BankingBusiness;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankingBuisiness.TestingOOP
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Demo1()
        {
            Account a = new Account(123456, "scott thornton", 0.55M);

            Debug.WriteLine(a.Balance);
        }
    }
}
