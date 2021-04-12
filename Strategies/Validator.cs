namespace csharpcore.Strategies
{
    public static class Validator
    {
        public static void ValidateQuality(this Item item)
        {
            item.Quality = item.Quality switch
            {
                <0 => 0,
                >50 => 50,
                _ => item.Quality
            };
        }
    }
}