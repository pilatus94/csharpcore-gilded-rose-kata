using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace csharpcore
{
    public class GildedRoseTest
    {
        [Theory]
        [InlineData("GeneralItem", 10, 20, 9, 19)]
        [InlineData("GeneralItem", 0, 0, -1, 0)]
        [InlineData("GeneralItem", 0, 20, -1, 18)]
        [InlineData("Sulfuras, Hand of Ragnaros", 0, 80, 0, 80)]
        [InlineData("Sulfuras, Hand of Ragnaros", -1, 80, -1, 80)]
        [InlineData("Aged Brie", 2, 0, 1, 1)]
        public void UpdateQuality_CorrectlyAdjustsPropertiesPerDay(string itemName, int initialSellIn, int initialQuality, int expectedSellIn, int expectedQuality)
        {
            var item = new Item { Name = itemName, SellIn = initialSellIn, Quality = initialQuality };
            IList<Item> items = new List<Item> { item };
            var app = new GildedRose(items);

            app.UpdateQuality();

            item.Quality.Should().Be(expectedQuality);
            item.SellIn.Should().Be(expectedSellIn);
        }

        [Theory]
        [InlineData(1, 0, 0)]
        [InlineData(0, 0, 0)]
        [InlineData(-1, 2, 0)]
        [InlineData(-2, 2, 0)]
        [InlineData(-3, 10, 8)]
        public void UpdateQuality_QualityDegradation(int initialSellIn, int initialQuality, int expectedQuality)
        {
            var item = new Item { Name = "General", SellIn = initialSellIn, Quality = initialQuality };
            IList<Item> items = new List<Item> { item };
            var app = new GildedRose(items);

            app.UpdateQuality();
            item.Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData(11, 10, 11)]
        [InlineData(10, 10, 12)]
        [InlineData(5, 10, 13)]
        [InlineData(4, 10, 13)]
        [InlineData(0, 10, 0)]
        public void UpdateQuality_BackstagePassQualityAdjustments(int initialSellIn, int initialQuality, int expectedQuality)
        {
            var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = initialSellIn, Quality = initialQuality };
            IList<Item> items = new List<Item> { item };
            var app = new GildedRose(items);

            app.UpdateQuality();
            item.Quality.Should().Be(expectedQuality);
        }

        [Fact]
        public void UpdateQuality_QualityIsNotNegative()
        {
            var item = new Item { Name = "General", SellIn = 10, Quality = 10 };
            IList<Item> items = new List<Item> { item };
            var app = new GildedRose(items);

            for (int i = 0; i < 30; i++)
            {
                app.UpdateQuality();

                item.Quality.Should().BeGreaterOrEqualTo(0);
            }
        }

        [Fact]
        public void UpdateQuality_QualityIsNotHigherThan50()
        {
            var item = new Item { Name = "Aged Brie", SellIn = 0, Quality = 49 };
            IList<Item> items = new List<Item> { item };
            var app = new GildedRose(items);

            for (int i = 0; i < 30; i++)
            {
                app.UpdateQuality();
                item.Quality.Should().BeLessOrEqualTo(50);
            }
        }
    }
}