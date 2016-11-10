using System;
using System.IO;
using NUnit.Framework;
using OTP_API;
using OTP_API.Common;

namespace OTP_API.Test
{
	[TestFixture]
    public class OTPTest
    {
        [TestCase("mahdi")]
        public void TestProcess(string user)
        {
            Assert.AreEqual("mahdi", user);
        }

        [TestCase]
        public void TestStorage()
        {
            Storage.Set("key1", "value1");
            Assert.AreEqual("value1", Storage.Get("key1"));

            Storage.Set("key2", "20918732190738921739821897321897398");
            Assert.AreEqual("20918732190738921739821897321897398", Storage.Get("key2"));
        }

        [TestCase]
        public void TestLogin()
        {
        }

        [TestCase]
        public void TestRegister()
        {

        }

    }
}


