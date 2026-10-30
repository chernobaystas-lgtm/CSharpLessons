namespace LessonsMainCSharp;
using System.ComponentModel;
using static System.Console;
using System;
using System.Collections;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        SoccerPlayerTeam team = new SoccerPlayerTeam();

        Player p1 = new Player("Andriy", "Shevchenko", 183.5, 76.0, 45, Side.Middle, Position.Striker);
        Player p2 = new Player("Serhiy", "Rebrov", 173.0, 70.5, 47, Side.Right, Position.Striker);

        Player defaultPlayer = new Player();

        Player spare1 = new Player("Oleksandr", "Zinchenko", 175.0, 64.0, 27, Side.Left, Position.Defender);

        team.AddPlayerToTeam(p1);
        team.AddPlayerToTeam(p2);
        team.AddPlayerToTeam(defaultPlayer);

        team.AddSpareToTeam(spare1);

        WriteLine("--- Склад команди до змін (Поле + Лава) ---");
        foreach (Player player in team)
        {
            WriteLine(player);
        }

        WriteLine("\n--- Виконуємо заміну: Shevchenko <-> Zinchenko ---");
        bool isSubstituted = team.SubstitutePlayer("Shevchenko", "Zinchenko");
        WriteLine(isSubstituted ? "Заміна успішна!" : "Гравців не знайдено.");

        WriteLine("\n--- Видаляємо дефолтного гравця з команди ---");
        team.RemovePlayerToTeam("No last name");

        WriteLine("\n--- Фінальний склад команди ---");
        foreach (Player player in team)
        {
            WriteLine(player);
        }
    }
}




public enum Position
{
  Striker,
  Vinger,
  Mid_Defender,
  Defender,
  Goalkeeper
}

public enum Side
{
  Left,

  Middle,

  Right
}

public class Player : IEnumerable
{
  public string? Name{get; set;}
  public string? LastName{get; set;}
  public double? Height{get; set;}
  public double? Weight{get; set;}

  public int? Age{get;set;}

  public Side? MainSide{set; get;}

  public Position? MainPosition{get; set;}


  public Player()
  {
      Name = "No name";
      LastName = "No last name";
      Height = 0.0;
      Weight = 0.0;
      Age = 0;
      MainSide = Side.Middle;
      MainPosition = Position.Mid_Defender;

  }

  public Player(string? Name, string? LastName, double? Height, double? Weight, int? Age, Side? MainSide, Position? MainPosition)
  {
    this.Name = Name;
    this.LastName =LastName;
    this.Height = Height;
    this.Weight = Weight;
    this.Age = Age;
    this.MainSide = MainSide;
    this.MainPosition = MainPosition;
  }

  public override string ToString()
  {
    return $" Name: {Name}, LastName: {LastName}, Height: {Height}, Weight {Weight}, Age: {Age}, Side: {MainSide}, Position: {MainPosition}.";
  }

  public IEnumerator GetEnumerator()
  {
    throw new NotImplementedException();
  }
}

class SoccerPlayerTeam : IEnumerable<Player>
{
  private Player[] Players;
  private int Playerscount;

  private Player[] Spares;

  private int Sparescount;

  public SoccerPlayerTeam()
  {
        Players = new Player[11];
        Spares = new Player[12];

        Playerscount = 0;
        Sparescount = 0;
  }

  public void AddPlayerToTeam(Player player)
    {
        if (Players == null || this.Playerscount >= this.Players.Length) return;

        this.Players[this.Playerscount] = player;
        this.Playerscount++;
    }

    public bool RemovePlayerToTeam(string species)
    {
        int indexToRemove = -1;

        for (int i = 0; i < this.Playerscount; i++)
        {
            if (this.Players[i] != null)
            {
                indexToRemove = i;
                break;
            }
        }

        if (indexToRemove == -1) return false;

        for (int i = indexToRemove; i < this.Playerscount - 1; i++)
        {
            this.Players[i] = this.Players[i + 1];
        }

        this.Playerscount--;
        this.Players[this.Playerscount] = null;
        return true;
    }


public void AddSpareToTeam(Player player)
{
    if (this.Spares == null || this.Sparescount >= this.Spares.Length) return;

    this.Spares[this.Sparescount] = player;
    this.Sparescount++;
}

public bool RemoveSpare(string playerName)
{
    int indexToRemove = -1;

    for (int i = 0; i < this.Sparescount; i++)
    {
        if (this.Spares[i] != null && this.Spares[i].Name == playerName)
        {
            indexToRemove = i;
            break;
        }
    }

    if (indexToRemove == -1) return false;

    for (int i = indexToRemove; i < this.Sparescount - 1; i++)
    {
        this.Spares[i] = this.Spares[i + 1];
    }

    this.Sparescount--;
    this.Spares[this.Sparescount] = null;
    return true;
}

 public bool SubstitutePlayer(string activePlayerName, string sparePlayerName)
    {
        int activeIndex = -1;
        int spareIndex = -1;

        for (int i = 0; i < this.Playerscount; i++)
        {
            if (this.Players[i] != null && this.Players[i].Name == activePlayerName)
            {
                activeIndex = i;
                break;
            }
        }
        for (int i = 0; i < this.Sparescount; i++)
        {
            if (this.Spares[i] != null && this.Spares[i].Name == sparePlayerName)
            {
                spareIndex = i;
                break;
            }
        }

        if (activeIndex != -1 && spareIndex != -1)
        {
            Player temp = this.Players[activeIndex];
            this.Players[activeIndex] = this.Spares[spareIndex];
            this.Spares[spareIndex] = temp;
            return true;
        }

        return false;
    }

    public IEnumerator<Player> GetEnumerator()
    {
        for (int i = 0; i < this.Playerscount; i++)
        {
            if (this.Players[i] != null)
            {
                yield return this.Players[i];
            }
        }

        for (int i = 0; i < this.Sparescount; i++)
        {
            if (this.Spares[i] != null)
            {
                yield return this.Spares[i];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}