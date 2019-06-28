using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TypeSafeEnumRef.TypeSafeEnum.Flags.Tests
{
    [TestClass]
    public class TypeSafeEnumTest
    {
        [TestMethod]
        [DataRow(1, "Ordered")]
        [DataRow(2, "Reserved")]
        [DataRow(4, "Available")]
        [DataRow(8, "Rented")]
        [DataRow(16, "Lost")]
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

        [TestMethod]
        [DataRow(1, 2, 3)]
        [DataRow(1, 4, 5)]
        [DataRow(1, 8, 9)]
        [DataRow(1, 16, 17)]
        [DataRow(2, 4, 6)]
        [DataRow(2, 8, 10)]
        [DataRow(2, 16, 18)]
        [DataRow(4, 8, 12)]
        [DataRow(4, 16, 20)]
        [DataRow(8, 16, 24)]
        public void Or_ShouldReturnExpectedResult(int bookStatusId1, int bookStatusId2, int expectedBookStatusId) 
        {
            BookStatus bookStatus1 = bookStatusId1;
            BookStatus bookStatus2 = bookStatusId2;
            BookStatus expectedBookStatus = expectedBookStatusId;

            BookStatus testObject = bookStatus1 | bookStatus2;

            Assert.AreEqual(expectedBookStatus, testObject);
        }

        [TestMethod]
        [DataRow(1, 3, 1)]
        [DataRow(1, 2, null)]
        public void And_ShouldReturnExpectedValue(int bookStatusId1, int bookStatusId2, int? expectedBookStatusId)
        {
            BookStatus bookStatus1 = bookStatusId1;
            BookStatus bookStatus2 = bookStatusId2;
            BookStatus expectedBookStatus = expectedBookStatusId;

            BookStatus testObject = bookStatus1 & bookStatus2;

            Assert.AreEqual(expectedBookStatus, testObject);
        }
    }
}
