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

        string[] cities = { "Kyiv", "Amsterdam", "London", "Madrid", "Anchorage", "Rome", "Athens", "New York", "Oslo", "Astana", "Norfolk", "Minsk", "Antwerp", "Newark", "Aberdeen", "Nairobi", "Norwich", "Novosibirsk", "Aachen", "Ankara" };


        foreach (var city in cities)
        {
          WriteLine(city);
        }


        int targetLength = 6;

        var filteredCities = from c in cities
                     where c.Length == targetLength
                     select c;

        WriteLine($"\nМіста з довжиною назви {targetLength}:");
        foreach (var city in filteredCities)
        {
          WriteLine(city);
        }

        foreach (var city in cities)
        {
            if (city.StartsWith("A"))
            {
                WriteLine(city);
            }
        }

        foreach (var city in cities)
        {
            if (city.EndsWith("M"))
            {
                WriteLine(city);
            }
        }

        foreach (var city in cities)
        {
            if (city.StartsWith("N") && city.EndsWith("K"))
            {
                WriteLine(city);
            }
        }



    }
}