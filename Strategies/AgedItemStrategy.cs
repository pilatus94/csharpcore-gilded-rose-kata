using csharpcore.Items;

namespace csharpcore.Strategies
{
    public class AgedItemStrategy : IUpdateStrategy
    {
        public void Update(Item item)
        {
            item.SellIn -= 1;
            item.Quality += item.SellIn < 0 ? 2 : 1;
            item.ValidateQuality();
        }
    }
}