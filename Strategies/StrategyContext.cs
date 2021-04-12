using System;
using csharpcore.Items;

namespace csharpcore.Strategies
{
    public class StrategyContext
    {
        private IUpdateStrategy _strategy;

        public void SetUpdateStrategy(ItemCategory category)
        {
            switch (category)
            {
                case ItemCategory.General:
                    break;
                case ItemCategory.Aged:
                    break;
                case ItemCategory.Conjured:
                    break;
                case ItemCategory.Pass:
                    break;
                case ItemCategory.Legendary:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category), category, null);
            }
        }
    }
}
