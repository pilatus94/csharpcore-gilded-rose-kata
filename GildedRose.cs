using System.Collections.Generic;
using csharpcore.Items;
using csharpcore.Strategies;

namespace csharpcore
{
    public class GildedRose
    {
        private readonly IList<StoreItem> _items;
        private readonly UpdateStrategyContext _updateContext = new();

        public GildedRose(IList<StoreItem> items) => _items = items;

        public void UpdateQuality()
        {
            foreach (var item in _items)
            {
                _updateContext.SetUpdateStrategy(item.ItemCategory);
                _updateContext.Update(item);
            }
        }

    }
}