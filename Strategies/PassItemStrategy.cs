using System;
using csharpcore.Items;

namespace csharpcore.Strategies
{
    public class PassItemStrategy : IUpdateStrategy
    {
        public void Update(Item item)
        {
            item.SellIn -= 1;

            if (item.SellIn < 0)
            {
                item.Quality = 0;
                return;
            }

            item.Quality += item.SellIn switch
            {
                >=10 => 1,
                >=5 => 2,
                >=0 => 3,
                _ => throw new ArgumentOutOfRangeException()
            };

            item.ValidateQuality();
        }
    }
}