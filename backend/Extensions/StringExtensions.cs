namespace backend.Extensions
{
    public static class StringExtensions
    {
        public static int CountOccurrences(string text, string pattern)
        {
            int count = 0;
            int i = 0;

            while ((i = text.IndexOf(pattern, i, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                count++;
                i += pattern.Length; // increment i with length of the pattern
            }
            Console.WriteLine(count);
            return count;
        }

    }
}

