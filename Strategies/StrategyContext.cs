using System;
using csharpcore.Items;

namespace csharpcore.Strategies
{
    public class UpdateStrategyContext
    {
        private IUpdateStrategy _strategy;

        public void SetUpdateStrategy(ItemCategory category)
        {
            _strategy = category switch
            {
                ItemCategory.General => new GeneralItemStrategy(),
                ItemCategory.Aged => new AgedItemStrategy(),
                ItemCategory.Conjured => new ConjuredItemStrategy(),
                ItemCategory.Pass => new PassItemStrategy(),
                ItemCategory.Legendary => new LegendaryItemStrategy(),
                _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
            };
        }

        public void Update(StoreItem item) => _strategy.Update(item);
    }
}