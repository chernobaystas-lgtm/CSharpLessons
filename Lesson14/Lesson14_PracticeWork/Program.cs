namespace LessonsMainCSharp;
using System;
using System.Collections;
using System.Text;
using static System.Console;
using System.Linq;


class Program
{
    static void Main(string[] args)
    {
       OutputEncoding = Encoding.UTF8;
       InputEncoding = Encoding.UTF8;


 string[] lastNames = { "Smith", "Williams", "Brown", "Orange", "Taylor", "Fox", "Kennedy" };

WriteLine(string.Join(", ", lastNames));


bool allMoreThan3 = lastNames.All(name => name.Length > 3);
WriteLine($"Чи всі прізвища мають довжину > 3 символів: {allMoreThan3}");

bool allBetween3And10 = lastNames.All(name => name.Length > 3 && name.Length < 10);
WriteLine($"Чи всі прізвища мають довжину від 4 до 9 символів: {allBetween3And10}");

bool anyStartsWithW = lastNames.Any(name => name.StartsWith("W", StringComparison.OrdinalIgnoreCase));
WriteLine($"Чи є хоча б одне прізвище на літеру 'W': {anyStartsWithW}");

bool anyEndsWithY = lastNames.Any(name => name.EndsWith("Y", StringComparison.OrdinalIgnoreCase));
WriteLine($"Чи є хоча б одне прізвище, що закінчується на 'Y': {anyEndsWithY}");

bool containsOrange = lastNames.Contains("Orange");
WriteLine($"Чи є у масиві прізвище 'Orange': {containsOrange}");

string? firstLength6 = lastNames.FirstOrDefault(name => name.Length == 6);
WriteLine($"Перше прізвище з довжиною 6: {firstLength6 ?? "Не знайдено"}");

string? lastLessThan15 = lastNames.LastOrDefault(name => name.Length < 15);
WriteLine($"Останнє прізвище з довжиною < 15: {lastLessThan15 ?? "Не знайдено"}");

    }
}