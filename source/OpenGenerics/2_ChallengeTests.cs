using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace MasterSecrets.OpenGenerics
{
    [TestFixture]
    public class ChallengeTests
    {
        [Test]
        public void GetTypeTest()
        {
            Type answer = Challenge.GetType<int>();

            Assert.True(answer == typeof (int));
        }

        [Test]
        public void GetListTest()
        {
            IList answer = Challenge.GetList(typeof (DateTime));

            Assert.True(answer is IList<DateTime>);
        }

        [Test]
        public void GetFiveDefaultsTest()
        {
            IList answer = Challenge.GetFiveDefaults(typeof (string));

            Assert.True(answer.Count == 5);

            foreach (object obj in answer)
            {
                Assert.AreEqual(default(string), obj);
            }
        }
    }
}