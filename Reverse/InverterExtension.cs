namespace Inverter
{
    internal static class InverterExtension
    {
        internal static string Inverter(this string text)
        {
            string reverse = string.Empty;

            for (int i = 1; i <= text.Length; i++)
            {
                reverse += text[text.Length - i];
            }

            return reverse;
        }
    }
}
