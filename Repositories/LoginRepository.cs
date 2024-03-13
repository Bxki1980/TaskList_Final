using System;
using System.Text.RegularExpressions;
using TaskList_Final_.Repositories;

public class LoginRepository : ILoginRepository
{
    // Regular expression for a strong password (at least 8 characters, at least one uppercase letter, one lowercase letter, and one digit)
    private const string StrongPasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";

    // Regular expression for a valid username (only letters, numbers, and underscores, and between 5 and 20 characters long)
    private const string ValidUsernamePattern = @"^[a-zA-Z0-9_]{5,20}$";

    public bool LoginValidation(string Username, string Password)
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
        {
            return false;
        }

        return Regex.IsMatch(Username, ValidUsernamePattern) && Regex.IsMatch(Password, StrongPasswordPattern);
    }
}
