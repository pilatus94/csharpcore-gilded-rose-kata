namespace csharpcore.Items
{
    public class StoreItem : Item
    {
        public StoreItem(ItemCategory category, string name, int sellIn, int quality)
        {
            ItemCategory = category;
            Name = name;
            SellIn = sellIn;
            Quality = quality;
        }

        public ItemCategory ItemCategory { get; private set; }
    }
}