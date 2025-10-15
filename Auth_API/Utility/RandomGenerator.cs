using System.Text;

namespace Auth_API.Utility
{
    public static class RandomGenerator
    {
        public static string GenerateRandomText(int textLength)
        {


            // creating a StringBuilder object()
            StringBuilder strBuilder = new StringBuilder();
            Random random = new Random();



            char letter;

            for (int i = 0; i < textLength; i++)
            {
                double flt = random.NextDouble();
                int num = Convert.ToInt32(Math.Floor(35 * flt));
                if (num < 26)
                    letter = Convert.ToChar(num + 65);
                else
                    letter = Convert.ToChar((num - 26) + 48);
                strBuilder.Append(letter);
            }
            return strBuilder.ToString();
        }
    }
}
