using System;
using System.Threading;
using System.IO;
using NUnit.Framework;
using OTP_API;
using OTP_API.Common;

namespace OTP_API.Test
{
    //Note that this code is testing logic behind the OTP API methods
    //Like registration, login and things like that.
    //There can be another test suite to test the whole package from outside
    //using the web interface (Sending http requests and checking the response)
	[TestFixture]
    public class Test
    {
        [SetUp] 
        public void Init()
        {
            //prevent data conflict between two separate test cases
            Storage.Clear();

            //restore TTL to the default value
            Data.TTL = 30;

            //we don't need logging during tests
            Data.LogFailedLogin = false;
            Data.LogSuccessfulLogin = false;
        }

        [TestCase("mahdi", "daniel")]
        public void TestProcess(string user1, string user2)
        {
            string password1 = Registration.Process(user1);
            Assert.AreEqual("OK", Login.Process(user1, password1));

            //Make sure password works only once
            Assert.AreEqual("FAIL", Login.Process(user1, password1));

            password1 = Registration.Process(user1);
            string password2 = Registration.Process(user2);
            Assert.AreEqual("OK", Login.Process(user2, password2));
            Assert.AreEqual("OK", Login.Process(user1, password1));

            //Make sure password works only once when we have two users
            Assert.AreEqual("FAIL", Login.Process(user2, password2));
            Assert.AreEqual("FAIL", Login.Process(user1, password1));

            //change TTL to 5 seconds for testing purposes because 30 seconds
            //is too high for test running
            Data.TTL = 5;
            password1 = Registration.Process(user1);
            Thread.Sleep(1000 * Data.TTL);
            Assert.AreEqual("FAIL", Login.Process(user1, password1));
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


