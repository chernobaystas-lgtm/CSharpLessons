using System.Text.Unicode;
using Microsoft.VisualBasic;
using static System.Console;


namespace Lesson8_PracticeWork
{
    class Program
    {
static void Main(string[] args)
{
    WriteLine("--- Testing Television ---");
    IRemoteControl tv = new Television();
    tv.SetChannel(5); 
    tv.TurnOn();
    tv.SetChannel(7);
    tv.TurnOff();

    WriteLine("\n--- Testing Radio ---");
    IRemoteControl radio = new Radio();
    radio.TurnOn();
    radio.SetChannel(101);
    radio.TurnOff();
}

    }
}

public interface IRemoteControl
{
    void TurnOn();
    void TurnOff();
    void SetChannel(int channel);
}


class Television : IRemoteControl
{
    private bool isOn = false;
    private int currentChannel = 1;

    public void TurnOn()
    {
        isOn = true;
        WriteLine("Television is turned ON.");
    }

    public void TurnOff()
    {
        isOn = false;
        WriteLine("Television is turned OFF.");
    }

    public void SetChannel(int channel)
    {
        if (isOn)
        {
            currentChannel = channel;
            WriteLine($"Television: Switched to channel {currentChannel}");
        }
        else
        {
            WriteLine("Error: Turn on the television first.");
        }
    }
}

class Radio : IRemoteControl
{
    private bool isOn = false;
    private int currentStation = 100;

    public void TurnOn()
    {
        isOn = true;
        WriteLine("Radio is turned ON.");
    }

    public void TurnOff()
    {
        isOn = false;
        WriteLine("Radio is turned OFF.");
    }

    public void SetChannel(int channel)
    {
        if (isOn)
        {
            currentStation = channel;
            WriteLine($"Radio: Tuned to frequency {currentStation} FM");
        }
        else
        {
            WriteLine("Error: Turn on the radio first.");
        }
    }
}
