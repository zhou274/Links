namespace GameTemplate.Scripts
{
    public static class ExtendedString
    {
        public static string FormattedWith(this string str, params object[] args)
        {
            return string.Format(str, args);
        }
    }
}