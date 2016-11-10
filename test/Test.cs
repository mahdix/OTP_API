using System;
using System.Threading;
using System.IO;
using NUnit.Framework;
using OTP_API;
using OTP_API.Common;

namespace OTP_API.Test
{
	[TestFixture]
    public class OTPTest
    {
        [SetUp] 
        public void Init()
        {
            //prevent data conflict between two separate test cases
            Storage.Clear();
        }

        [TestCase("mahdi")]
        [TestCase("daniel")]
        [TestCase("john")]
        public void TestProcess(string user)
        {
            string password = Registration.Process(user);
            Assert.AreEqual("OK", Login.Process(user, password));
        }

        public void TestExceptions()
        {
            //test edge cases
            Assert.IsNull(Registration.Process(null));
            Assert.IsNull(Registration.Process(""));

            Assert.AreEqual("FAIL", Login.Process(null, "pass"));
            Assert.AreEqual("FAIL", Login.Process("", "pass"));
            Assert.AreEqual("FAIL", Login.Process("user", null));
            Assert.AreEqual("FAIL", Login.Process("user", ""));
        }

        [TestCase]
        public void TestStorage()
        {
            Storage.Set("key1", "value1");
            Assert.AreEqual("value1", Storage.Get("key1"));

            Storage.Set("key2", "20918732190738921739821897321897398");
            Assert.AreEqual("20918732190738921739821897321897398", Storage.Get("key2"));
        }

        [TestCase("mahdi", "123")]
        [TestCase("john", "MDSADKASDSA213312")]
        public void TestLogin(string user, string toBePass)
        {
            //make sure login is failed with empty or not yet created password
            Assert.AreEqual("FAIL", Login.Process(user, ""));
            Assert.AreEqual("FAIL", Login.Process(user, toBePass));
            Storage.Set(user, toBePass);

            //Make sure login is ok when we set the password manually
            Assert.AreEqual("OK", Login.Process(user, toBePass));
        }

        [TestCase("mahdi")]
        [TestCase("michael")]
        [TestCase("12345")]
        public void TestRegister(string userName)
        {
            //Make sure passwords are time-bound
            string password = Registration.Process(userName);
            Thread.Sleep(1000);
            string password2 = Registration.Process(userName);

            Assert.AreNotEqual(password, password2);

            //make sure password's basic properties are met
            Assert.AreEqual(password.Length, 64);
            Assert.AreEqual(password, password.ToUpper());
        }
    }
}


