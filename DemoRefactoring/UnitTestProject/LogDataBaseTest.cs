using System;
using DemoRefactoring;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class LogDataBaseTest
    {
        private LogDatabase log;
        private Message message;

        [TestInitialize]
        public void TestInitialize()
        {
            log = new LogDatabase();
            message = new Message("Hello");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LogError_NullMessage_ThrowArgumentNullException()
        {
            log.LogError(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LogMessage_NullMessage_ThrowArgumentNullException()
        {
            log.LogMessage(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LogWarning_NullMessage_ThrowArgumentNullException()
        {
            log.LogWarning(null);
        }

        [TestMethod]
        public void LogWarning_MessageHello_RegisterOk()
        {
            log.LogWarning(message);

            Assert.AreEqual(MessageType.Warning, log.LastMessageType);
            Assert.AreEqual(message, log.LastMessage);
        }

        [TestMethod]
        public void LogMessage_MessageHello_RegisterOk()
        {
            log.LogMessage(message);

            Assert.AreEqual(MessageType.Info, log.LastMessageType);
            Assert.AreEqual(message, log.LastMessage);
        }


        [TestMethod]
        public void LogError_MessageHello_RegisterOk()
        {
            log.LogError(message);

            Assert.AreEqual(MessageType.Error, log.LastMessageType);
            Assert.AreEqual(message, log.LastMessage);
        }

    }
}
