namespace BDP.Application.Helpers
{
    public class Randomizer
    {
        public static long Numeric(int length)
        {
            var random = new Random();
            string result = string.Empty;
            for (int i = 0; i < length; i++)
                result = String.Concat(result, random.Next(10).ToString());
            return Convert.ToInt64(result);
        }
    }
}
