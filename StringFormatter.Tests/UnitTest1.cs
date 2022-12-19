using StringFormatter.Tests.Classes;

namespace StringFormatter.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private static FakeClass _fakeClass;

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            _fakeClass = new FakeClass
            {
                StringValue = "Some string value",
                IntValueField = 12,
                RecursivelyProperty = new FakeClass(),
                ArrayProperty = new[]
                {
                new ArrayElement { Data = 12 },
                new ArrayElement { Data = 14 }
            }
            };
        }

        [TestMethod]
        public void Format_UseFakeClass_ReturnsNormalData()
        {
            var s = Core.StringFormatter.Shared.Format("Static text is {StaticText}. {IntValueField} = 12.", _fakeClass);

            Assert.AreEqual($"Static text is {_fakeClass.StaticText}. {_fakeClass.IntValueField} = 12.", s);
        }

        [TestMethod]
        public void Format_UseShielding_StringDoesntChange()
        {
            var s = Core.StringFormatter.Shared.Format("{{StaticText}}{{}}", _fakeClass);

            Assert.AreEqual("{StaticText}{}", s);
        }

        [TestMethod]
        public void Format_TargetIsNull_Exception()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Core.StringFormatter.Shared.Format("{Property}", null));
            Assert.ThrowsException<ArgumentNullException>(() => Core.StringFormatter.Shared.Format(null, new object()));
        }

        [TestMethod]
        public void Format_AccessingPropertyRecursively_Works()
        {
            var s = Core.StringFormatter.Shared.Format("{RecursivelyProperty.StaticText}", _fakeClass);
            Assert.AreEqual(_fakeClass.RecursivelyProperty.StaticText, s);
        }

        [TestMethod]
        public void Format_ArrayElement_Works()
        {
            var s1 = Core.StringFormatter.Shared.Format("{ArrayProperty[0]}", _fakeClass);
            var s2 = Core.StringFormatter.Shared.Format("{ArrayProperty[1]}", _fakeClass);
            Assert.AreEqual(_fakeClass.ArrayProperty[0].ToString(), s1);
            Assert.AreEqual(_fakeClass.ArrayProperty[1].ToString(), s2);
        }

        [TestMethod]
        public void Format_ArrayElementProperty_Works()
        {
            var s1 = Core.StringFormatter.Shared.Format("{ArrayProperty[0].Data}", _fakeClass);
            var s2 = Core.StringFormatter.Shared.Format("{ArrayProperty[1].Data}", _fakeClass);
            Assert.AreEqual(_fakeClass.ArrayProperty[0].Data.ToString(), s1);
            Assert.AreEqual(_fakeClass.ArrayProperty[1].Data.ToString(), s2);
        }
    }
}