using NUnit.Framework;
using Scorer;

namespace ScorerTests
{
    [TestFixture]
    public class ScorerTests
    {
        [Test]
        public void AddShouldReturnSumOfTwoIntegers()
        {
            var sut = new GameScorer();

            var result = sut.Add(2, 3);

            Assert.AreEqual(5, result);
        }
    }
}