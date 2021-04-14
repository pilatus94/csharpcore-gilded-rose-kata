using csharpcore.Items;
using csharpcore.Strategies;
using FluentAssertions;
using Xunit;

namespace csharpcore.Tests
{
    public class StrategyTest
    {
        [Theory]
        [InlineData(10, 0, 1)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 0, 2)]
        [InlineData(-1, 0, 2)]
        [InlineData(-2, 50, 50)]
        public void AgedItemStrategy_UpdatesCorrectly(int initialSellIn, int initialQuality, int expectedQuality)
        {
            StoreItem item = new (ItemCategory.Aged, "Aged", initialSellIn, initialQuality);
            var agedItemStrategy = new AgedItemStrategy();

            agedItemStrategy.Update(item);

            item.SellIn.Should().Be(--initialSellIn);
            item.Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData(1, 0, 0)]
        [InlineData(10, 2, 0)]
        [InlineData(-1, 10, 8)]
        [InlineData(-2, 0, 0)]
        [InlineData(10, 50, 50)]
        public void ConjuredItemStrategy_UpdatesCorrectly(int initialSellIn, int initialQuality, int expectedQuality)
        {
            StoreItem item = new(ItemCategory.Conjured, "Conjured", initialSellIn, initialQuality);
            var conjuredItemStrategy = new ConjuredItemStrategy();

            conjuredItemStrategy.Update(item);

            item.SellIn.Should().Be(--initialSellIn);
            item.Quality.Should().Be(expectedQuality);
        }
    }
}
