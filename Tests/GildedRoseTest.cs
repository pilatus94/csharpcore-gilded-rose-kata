using System.Collections.Generic;
using csharpcore.Items;
using FluentAssertions;
using Xunit;

namespace csharpcore.Tests
{
    public class GildedRoseTest
    {
        [Theory]
        [InlineData(ItemCategory.General, "GeneralItem", 10, 20, 9, 19)]
        [InlineData(ItemCategory.General, "GeneralItem", 0, 0, -1, 0)]
        [InlineData(ItemCategory.General, "GeneralItem", 0, 20, -1, 18)]
        [InlineData(ItemCategory.Legendary, "Sulfuras, Hand of Ragnaros", 0, 80, 0, 80)]
        [InlineData(ItemCategory.Legendary, "Sulfuras, Hand of Ragnaros", -1, 80, -1, 80)]
        [InlineData(ItemCategory.Aged, "Aged Brie", 2, 0, 1, 1)]
        public void UpdateQuality_CorrectlyAdjustsPropertiesPerDay(ItemCategory category, string itemName, int initialSellIn, int initialQuality, int expectedSellIn, int expectedQuality)
        {
            var items = GetListWithSingleItem(category, itemName, initialSellIn, initialQuality);
            GildedRose app = new (items);

            app.UpdateQuality();

            items[0].Quality.Should().Be(expectedQuality);
            items[0].SellIn.Should().Be(expectedSellIn);
        }

        [Theory]
        [InlineData(ItemCategory.General, 1, 0, 0)]
        [InlineData(ItemCategory.General, 0, 0, 0)]
        [InlineData(ItemCategory.General, -1, 2, 0)]
        [InlineData(ItemCategory.General, -2, 2, 0)]
        [InlineData(ItemCategory.General, -3, 10, 8)]
        public void UpdateQuality_QualityDegradation(ItemCategory category, int initialSellIn, int initialQuality, int expectedQuality)
        {
            var items = GetListWithSingleItem(category, string.Empty, initialSellIn, initialQuality);
            GildedRose app = new(items);

            app.UpdateQuality();
            items[0].Quality.Should().Be(expectedQuality);
        }

        [Theory]
        [InlineData(11, 10, 11)]
        [InlineData(10, 10, 12)]
        [InlineData(5, 10, 13)]
        [InlineData(4, 10, 13)]
        [InlineData(0, 10, 0)]
        public void UpdateQuality_BackstagePassQualityAdjustments(int initialSellIn, int initialQuality, int expectedQuality)
        {
            var items = GetListWithSingleItem(ItemCategory.Pass, "Backstage passes to a TAFKAL80ETC concert", initialSellIn, initialQuality);
            GildedRose app = new(items);

            app.UpdateQuality();
            items[0].Quality.Should().Be(expectedQuality);
        }

        [Fact]
        public void UpdateQuality_QualityIsNotNegative()
        {
            var items = GetListWithSingleItem(ItemCategory.General, string.Empty, 10, 10);
            GildedRose app = new(items);

            for (var i = 0; i < 30; i++)
            {
                app.UpdateQuality();

                items[0].Quality.Should().BeGreaterOrEqualTo(0);
            }
        }

        [Fact]
        public void UpdateQuality_QualityIsNotHigherThan50()
        {
            var items = GetListWithSingleItem(ItemCategory.Aged, "Aged Brie", 0, 49);
            GildedRose app = new(items);

            for (var i = 0; i < 30; i++)
            {
                app.UpdateQuality();
                items[0].Quality.Should().BeLessOrEqualTo(50);
            }
        }

        [Fact]
        public void UpdateQuality_CorrectlyUpdatesQualityForConjuredItems()
        {
            var initialSellIn = 3;
            var initialQuality = 6;
            var items = GetListWithSingleItem(ItemCategory.Conjured, "Conjured Mana Cake", initialSellIn, initialQuality);
            GildedRose app = new(items);

            for (var i = 1; i <= 3; i++)
            {
                app.UpdateQuality();
                items[0].SellIn.Should().Be(initialSellIn - i);
                items[0].Quality.Should().Be(initialQuality - 2 * i);
            }
        }

        private static IList<StoreItem> GetListWithSingleItem(ItemCategory category, string name, int sellIn, int quality)
        {
            StoreItem item = new(category, name, sellIn, quality);
            return new List<StoreItem> { item };
        }
    }
}