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


