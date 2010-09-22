using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for StringHelper
/// </summary>
public class StringHelper
{
    const int MinRequiredPasswordLength = 8;

    /// <summary>
    /// Generates the password.
    /// </summary>
    /// <returns>Password</returns>
    public static string GenerateRandomPassword()
    {
        var rnd = new Random();
        string[] s = {
                             "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", 
                             "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", 
                             "r", "s", "t", "u", "v", "w", "x", "y", "z",
                             "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", 
                             "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
                         };
        int i;
        var sb = new StringBuilder(MinRequiredPasswordLength);
        for (i = 0; i <= MinRequiredPasswordLength; i++)
        {
            sb.Append(s[rnd.Next(1, s.Length)]);
        }
        return sb.ToString();
    }
}