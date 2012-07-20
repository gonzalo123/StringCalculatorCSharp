using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalculator;

namespace TestStringCalculator
{
    [TestClass]
    public class StringCaculatorTest
    {
        [TestMethod]
        public void TestSimpleNumbers()
        {
            var stringCalculator = new StringCalculator.StringCalculator();
            Assert.AreEqual(0, stringCalculator.Add(""));
            Assert.AreEqual(1, stringCalculator.Add("1"));
            Assert.AreEqual(2, stringCalculator.Add("2"));
            Assert.AreEqual(21, stringCalculator.Add("21"));
        }

        [TestMethod]
        public void TwoNumbers()
        {
            var stringCalculator = new StringCalculator.StringCalculator();
            Assert.AreEqual(2, stringCalculator.Add("1,1"));
            Assert.AreEqual(3, stringCalculator.Add("1,2"));
            Assert.AreEqual(33, stringCalculator.Add("11,22"));
        }

        [TestMethod]
        public void MoreThan2()
        {
            var stringCalculator = new StringCalculator.StringCalculator();
            Assert.AreEqual(3, stringCalculator.Add("1,1,1"));
            Assert.AreEqual(6, stringCalculator.Add("1,2,3"));
        }

        [TestMethod]
        public void MoreThan1Separator()
        {
            var stringCalculator = new StringCalculator.StringCalculator();
            Assert.AreEqual(3, stringCalculator.Add("1\n1,1"));
            Assert.AreEqual(6, stringCalculator.Add("1\n2,3"));
        }

        [TestMethod]
        public void CustomDemiters()
        {
            var stringCalculator = new StringCalculator.StringCalculator();
            Assert.AreEqual(3, stringCalculator.Add("//;\n1;2"));
            Assert.AreEqual(15, stringCalculator.Add("//;\n12;3"));
            Assert.AreEqual(16, stringCalculator.Add("//+\n12+3+1"));
        }

        [TestMethod]
        public void CustomDemitersVariousCharacters()
        {
            var stringCalculator = new StringCalculator.StringCalculator();
            Assert.AreEqual(3, stringCalculator.Add("//[**]\n1**2")); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] 
        public void TestNegativeNumbers()
        {
            var stringCalculator = new StringCalculator.StringCalculator();
            try
            {
                stringCalculator.Add("-1,2");
            }
            catch (ArgumentException exception)
            {
                Assert.AreEqual("negatives not allowed", exception.Message);
                throw;
            }
        }

        [TestMethod]
        public void IgnoredNumbersHigherThan1000()
        {
            var stringCalculator = new StringCalculator.StringCalculator();
            Assert.AreEqual(2, stringCalculator.Add("2,1000")); 
        }

        [TestMethod]
        public void MultipleDelimiters()
        {
            var stringCalculator = new StringCalculator.StringCalculator();
            Assert.AreEqual(10, stringCalculator.Add("//[**][$$]\n1**2$$3,4"));
        } 
    }
}
