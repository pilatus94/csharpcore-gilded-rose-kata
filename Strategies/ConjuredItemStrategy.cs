using csharpcore.Items;

namespace csharpcore.Strategies
{
    public class ConjuredItemStrategy : IUpdateStrategy
    {
        public void Update(Item item)
        {
            item.SellIn -= 1;
            item.Quality -= 2;
            item.ValidateQuality();
        }
    }
}