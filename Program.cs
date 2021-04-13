using System;
using System.Collections.Generic;
using csharpcore.Items;

namespace csharpcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("OMGHAI!");

            IList<StoreItem> items = new List<StoreItem>
            {
                new(ItemCategory.General, "+5 Dexterity Vest", 10, 20),
                new(ItemCategory.Aged, "Aged Brie", 2, 0),
                new(ItemCategory.General, "Elixir of the Mongoose", 5, 7),
                new(ItemCategory.Legendary, "Sulfuras, Hand of Ragnaros", 0, 80),
                new(ItemCategory.Legendary, "Sulfuras, Hand of Ragnaros", -1, 80),
                new(ItemCategory.Pass, "Backstage passes to a TAFKAL80ETC concert", 15, 20),
                new(ItemCategory.Pass, "Backstage passes to a TAFKAL80ETC concert", 10, 49),
                new(ItemCategory.Pass, "Backstage passes to a TAFKAL80ETC concert", 5, 49),
                new(ItemCategory.Conjured, "Conjured Mana Cake", 3, 6),
            };

            var app = new GildedRose(items);

            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");

                foreach (var item in items)
                    Console.WriteLine(item.Name + ", " + item.SellIn + ", " + item.Quality);

                Console.WriteLine("");
                app.UpdateQuality();
            }
        }
    }
}