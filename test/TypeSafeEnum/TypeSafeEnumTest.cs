using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TypeSafeEnumRef.TypeSafeEnum.Tests
{
    [TestClass]
    public class TypeSafeEnumTest
    {
        [TestMethod]
        [DataRow(1, "Ordered")]
        [DataRow(2, "Reserved")]
        [DataRow(3, "Available")]
        [DataRow(4, "Rented")]
        [DataRow(5, "Lost")]
        public void Switch_ShouldEnterExpectedCaseBranch(int bookStatusId, string expectedResult)
        {
            BookStatus testObject = bookStatusId;
            string bookStatusName = null;

            switch (testObject)
            {
                case var s when s == BookStatus.Ordered:
                    bookStatusName = "Ordered";
                    break;
                case var s1 when s1 == BookStatus.Reserved:
                    bookStatusName = "Reserved";
                    break;
                case var s2 when s2 == BookStatus.Available:
                    bookStatusName = "Available";
                    break;
                case var s3 when s3 == BookStatus.Rented:
                    bookStatusName = "Rented";
                    break;
                case var s4 when s4 == BookStatus.Lost:
                    bookStatusName = "Lost";
                    break;
            }

            Assert.AreEqual(expectedResult, bookStatusName);
        }
    }
}
