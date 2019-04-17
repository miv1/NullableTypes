namespace UiTest.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class Helpers
    {
        public static string[] GetInput()
        {
            string line = Console.ReadLine();
            string[] split = line.Split(new char[] { ' ' }, StringSplitOptions.None);
            return split;
        }

        public static bool IsInteger(string str)
        {
            bool isInteger = true;

            try
            {
                int num = int.Parse(str);
            }
            catch (FormatException)
            {
                isInteger = false;
            }

            return isInteger;
        }
    }
}
