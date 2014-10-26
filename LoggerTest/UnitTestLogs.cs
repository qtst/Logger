﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger;
using System.IO;
using System.Collections.Generic;

namespace LoggerTest
{
    [TestClass]
    public class UnitTestLogs
    {
        /// <summary>
        /// 加密解密测试
        /// </summary>
        [TestMethod]
        public void TestEncrypt()
        {
            Logs log = new Logs(Path.Combine(Environment.CurrentDirectory, "Logs"));
            log.IsEncrypt = true;

            string testStr = "日志加密测试";

            string strOutput = log.WriteLine(testStr);
            string strDes = log.EncryptLine(strOutput);
            strDes = strDes.Substring(strDes.Length - testStr.Length, testStr.Length);

            Assert.AreEqual(testStr, strDes);
        }

        /// <summary>
        /// 写字节数组测试
        /// </summary>
        [TestMethod]
        public void TestWriteLine()
        {
            Logs log = new Logs(Path.Combine(Environment.CurrentDirectory, "Logs"));

            byte[] bs = new byte[] { 1, 2, 3, 4, 5, 6 };
            string str = log.WriteLine(bs, "字节数组测试");
        }

        [TestMethod]
        public void TestReadLine()
        {
            string log = "2014-10-25 08:34:50 886 Error:日志加密测试,异常测试";
            string logEncrypt = "Sci3lqTkws1mnaAgMGM3k67KFandKIrO+JVC9NWhBEMJ0hPnMOlstBNF1VosP+xFLvraiQXDHd0=";

            Logs logDal = new Logs("");
            Log l = logDal.ReadLine(log);
            Assert.IsTrue(log.Contains(l.Describe));

            Log l1 = logDal.ReadLine(logEncrypt);
            Assert.IsTrue(l1.Describe.Contains("日志加密测试"));
        }

        [TestMethod]
        public void TestRead()
        {
            Logs log = new Logs();
            log.IsEncrypt = true;
            for (int i = 0; i < 1000; i++)
                log.WriteLine("日志读取测试" + i);
            string path = log.LogFilePath;

            List<Log> logs = log.Read(path);
        }
    }
}
