using System.Text;
using System.Text.Unicode;
using static System.Console;


namespace Lesson9_PracticeWork
{
    class Program
    {




static void Main(string[] args)
{
    WriteLine("--- Task 7: Checking Bracket Validity ---");
    
    string str1 = "{}[]";
    string str2 = "(())";
    string str3 = "[{}]";
    string str4 = "([)]";
    string str5 = "[[{}]";

    WriteLine($"String \"{str1}\" is valid: {str1.IsBracketBalanced()}");
    WriteLine($"String \"{str2}\" is valid: {str2.IsBracketBalanced()}");
    WriteLine($"String \"{str3}\" is valid: {str3.IsBracketBalanced()}");
    WriteLine($"String \"{str4}\" is valid: {str4.IsBracketBalanced()}");
    WriteLine($"String \"{str5}\" is valid: {str5.IsBracketBalanced()}");
}


    }
}


public static class Extensions
{
    public static bool IsBracketBalanced(this string str)
    {
        if (string.IsNullOrEmpty(str)) return true;

        Stack<char> stack = new Stack<char>();

        foreach (char ch in str)
        {
            if (ch == '(' || ch == '{' || ch == '[')
            {
                stack.Push(ch);
            }
            else if (ch == ')' || ch == '}' || ch == ']')
            {
                if (stack.Count == 0) return false;

                char lastOpened = stack.Pop();

                if (ch == ')' && lastOpened != '(') return false;
                if (ch == '}' && lastOpened != '{') return false;
                if (ch == ']' && lastOpened != '[') return false;
            }
        }

        return stack.Count == 0;
    }
}
