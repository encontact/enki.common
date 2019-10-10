using System;
using System.Collections.Generic;
using Xunit;

namespace enki.common.core.test
{
    public enum SimpleEnum
    {
        Value1 = 0,
        Value2 = 1
    }

    public class FakeSubOjectStructure
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }

    public struct FakeStructure
    {
        public int Id { get; set; }
        public string Info { get; set; }
    }

    public class FakeObjectStructure
    {
        public int Id { get; set; }
        public long LongNumber { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public DateTime? NullableDate { get; set; }
        public SimpleEnum EnumerationData { get; set; }
        public FakeSubOjectStructure SubObject { get; set; }
        public FakeStructure structure { get; set; }
        public IEnumerable<string> SimpleEnumerableOfString { get; set; }
        public IEnumerable<FakeSubOjectStructure> SubObjectList { get; set; }
    }

    public class ObjectUtilsTests
    {
        [Theory]
        [MemberData(nameof(GetEqualObjects))]
        public void ValidCompareObjects(object o1, object o2)
        {
            Assert.True(ObjectUtils.GenericCompare(o1, o2, out var diffProp),
            $"Houveram propriedades consideradas diferentes: { string.Join(",", diffProp) }");
        }

        public static IEnumerable<object[]> GetEqualObjects()
        {
            var sameDate = DateTime.UtcNow;
            var allData = new List<object[]>
            {
                new object[] {
                    new FakeObjectStructure(),
                    new FakeObjectStructure(),
                },
                new object[] {
                    new FakeObjectStructure { Id = 1, LongNumber = 546576798798778, Text = "Text", Date = sameDate },
                    new FakeObjectStructure { Id = 1, LongNumber = 546576798798778, Text = "Text", Date = sameDate },
                },
                new object[] {
                    new FakeObjectStructure { Id = 1, LongNumber = 546576798798778, Text = "Text", Date = sameDate },
                    new FakeObjectStructure { Id = 1, LongNumber = 546576798798778, Text = "Text", Date = sameDate },
                },
                new object[] {
                    new FakeObjectStructure { EnumerationData = SimpleEnum.Value1 },
                    new FakeObjectStructure { EnumerationData = SimpleEnum.Value1 },
                },
                new object[] {
                    new FakeObjectStructure { structure = new FakeStructure { Id = 1, Info = "Test" } },
                    new FakeObjectStructure { structure = new FakeStructure { Id = 1, Info = "Test" } },
                },
                new object[] {
                    new FakeObjectStructure { SimpleEnumerableOfString = new List<string> { "Simple Text" } },
                    new FakeObjectStructure { SimpleEnumerableOfString = new List<string> { "Simple Text" } },
                },
                new object[] {
                    new FakeObjectStructure { SubObject = new FakeSubOjectStructure { Id = 2, Data = "{Bla}" } },
                    new FakeObjectStructure { SubObject = new FakeSubOjectStructure { Id = 2, Data = "{Bla}" } },
                },
            };

            return allData;
        }

        [Theory]
        [MemberData(nameof(GetDifferentObjects))]
        public void InvalidCompareObjects(object o1, object o2)
        {
            Assert.False(ObjectUtils.GenericCompare(o1, o2, out var x));
        }

        public static IEnumerable<object[]> GetDifferentObjects()
        {
            var sameDate = DateTime.UtcNow;
            var allData = new List<object[]>
            {
                new object[] {
                    new FakeObjectStructure { },
                    new FakeObjectStructure { Id = 1 },
                },
                new object[] {
                    new FakeObjectStructure { Id = 1, LongNumber = 546576798798778, Text = "Text", Date = sameDate, EnumerationData = SimpleEnum.Value1 },
                    new FakeObjectStructure { Id = 1, LongNumber = 54657679879877, Text = "Text", Date = sameDate, EnumerationData = SimpleEnum.Value1 },
                },
            };

            return allData;
        }

    }
}