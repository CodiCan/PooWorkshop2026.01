using Backend;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend;

public class Time
{
    // Fields
   
    private readonly int hours;
    private readonly int minutes;
    private readonly int seconds;
    private readonly int milliseconds;

    // Properties
    
    public int Hours => hours;
    public int Minutes => minutes;
    public int Seconds => seconds;
    public int Milliseconds => milliseconds;

    // Constructors
    
    public Time() : this(0, 0, 0, 0)
    {
    }

    public Time(int hours) : this(hours, 0, 0, 0)
    {
    }

    public Time(int hours, int minutes) : this(hours, minutes, 0, 0)
    {
    }

    public Time(int hours, int minutes, int seconds) : this(hours, minutes, seconds, 0)
    {
    }

    public Time(int hours, int minutes, int seconds, int milliseconds)
    {
        if (hours < 0 || hours > 23)
            throw new ArgumentException($"The hour: {hours}, is not valid.");

        if (minutes < 0 || minutes > 59)
            throw new ArgumentException($"The minute: {minutes}, is not valid.");

        if (seconds < 0 || seconds > 59)
            throw new ArgumentException($"The second: {seconds}, is not valid.");

        if (milliseconds < 0 || milliseconds > 999)
            throw new ArgumentException($"The millisecond: {milliseconds}, is not valid.");

        this.hours = hours;
        this.minutes = minutes;
        this.seconds = seconds;
        this.milliseconds = milliseconds;
    }

    // Methods
   

    public override string ToString()
    {
        int hourInTwelveHourFormat = hours % 12;
        string suffix = hours < 12 ? "AM" : "PM";

        return $"{hourInTwelveHourFormat:D2}:{minutes:D2}:{seconds:D2}.{milliseconds:D3} {suffix}";
    }

    public long ToMilliseconds()
    {
        return (((hours * 60L + minutes) * 60 + seconds) * 1000) + milliseconds;
    }

    public long ToSeconds()
    {
        return (hours * 60L + minutes) * 60 + seconds;
    }

    public long ToMinutes()
    {
        return hours * 60L + minutes;
    }

    public bool IsOtherDay(Time other)
    {
        return ToMilliseconds() + other.ToMilliseconds() >= 86_400_000;
    }

    public Time Add(Time other)
    {
        long sum = (ToMilliseconds() + other.ToMilliseconds()) % 86_400_000;

        int newHours = (int)(sum / 3_600_000);
        sum %= 3_600_000;

        int newMinutes = (int)(sum / 60_000);
        sum %= 60_000;

        int newSeconds = (int)(sum / 1_000);
        int newMilliseconds = (int)(sum % 1_000);

        return new Time(newHours, newMinutes, newSeconds, newMilliseconds);
    }
}