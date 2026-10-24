using System.Text.Unicode;
using Microsoft.VisualBasic;
using static System.Console;


namespace Lesson8_PracticeWork
{
    class Program
    {
static void Main(string[] args)
{
    WriteLine("--- Testing Password Validator ---");
    IValidator passValidator = new PasswordValidator();
    
    Write("Enter password to test: ");
    string passwordInput = ReadLine() ?? "";
    passValidator.Validate(passwordInput);

    WriteLine("\n--- Testing Email Validator ---");
    IValidator emailValidator = new EmailValidator();
    
    Write("Enter email to test: ");
    string emailInput = ReadLine() ?? "";
    emailValidator.Validate(emailInput);
}

    }
}

public interface IValidator
{
    bool Validate(string data);
}

class PasswordValidator : IValidator
{
    public bool Validate(string data)
    {
        if (string.IsNullOrEmpty(data) || data.Length < 8)
        {
            WriteLine("Password validation failed: Must be at least 8 characters long.");
            return false;
        }
        WriteLine("Password validation successful.");
        return true;
    }
}

class EmailValidator : IValidator
{
    public bool Validate(string data)
    {
        if (string.IsNullOrEmpty(data) || !data.Contains("@") || !data.Contains("."))
        {
            WriteLine("Email validation failed: Invalid email format (missing '@' or '.').");
            return false;
        }
        WriteLine("Email validation successful.");
        return true;
    }
}
