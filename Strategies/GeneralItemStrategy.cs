namespace csharpcore.Strategies
{
    public class GeneralItemStrategy : IUpdateStrategy
    {
        public void Update(Item item)
        {
            item.SellIn -= 1;
            item.Quality -= (item.SellIn < 0 ? 2 : 1);
            item.ValidateQuality();
        }
    }
}