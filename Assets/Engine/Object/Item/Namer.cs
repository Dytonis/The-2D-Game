using UnityEngine;
using System.Collections.Generic;

public class Namer
{
    private static List<List<string>> ItemNames = new List<List<string>>() 
    { 
        new List<string>() 
        { 
            "Terrible",
            "Horrifying",
            "Nasty",
            "Awful",
            "Trash",
            "Stupid"
        },
        new List<string>() 
        { 
            "OK",
            "Average",
            "Not-So Horrible",
            "Acceptable",
            "Not-Special",
            "\'Meh\'"
        },
        new List<string>() 
        { 
            "Fine",
            "Quality",
            "Good",
            "Nice",
            "Shiney",
            "Cool"
        },
        new List<string>() 
        { 
            "Great",
            "Well-Made",
            "Very Good",
            "Keen",
            "Sharp",
            "Impressive"
        },
        new List<string>() 
        { 
            "Perfect",
            "Amazing",
            "Unbreakable",
            "Fearful",
            "God-Like",
            "Flawless"
        }
    };

    private static List<Color> LevelColors = new List<Color>()
    {
        new Color(0.1f, 0.1f, 0.1f),
        new Color(1f, 1f, 1f),
        new Color(0f, 1f, 0f),
        new Color(0f, 0f, 1f),
        new Color(1f, 0f, 1f)
    };

    public static List<string> ItemTypes = new List<string>()
    {
        "Sword",
        "Dagger",
        "Bow",
        "Magic",
        "Chest",
        "Head"
    };

    /// <summary>
    /// Returns a random stat tree for a given level.
    /// </summary>
    /// <returns>The stats.</returns>
    /// <param name="level">Level. Will be modified +- 1 by random.</param>
    public static StatsPackage getStats(int level)
    {
        int newLevel = Random.Range((level == 1) ? 1 : level-1, (level == 5) ? 6 : level+2);

        StatsPackage package = new StatsPackage();

        package.TYPE = getType();          //name uses type, so we must run type first
        package.RARITYCOLOR = LevelColors[newLevel-1];
        package.NAME = getName(newLevel) + " " + package.TYPE.ToString();
        package.DAMAGE = getDamage(newLevel, package.TYPE.ToString());
        package.LEVEL = newLevel;

        return package;
    }

    private static string getType()
    {
        int pick = Random.Range(0, 6);

        return ItemTypes[pick];
    }

    private static string getName(int level)
    {
        int pick = Random.Range(0, 6);

        return ItemNames[level-1][pick];
    }

    private static float getDamage(int level, string type)
    {
        switch(type)
        {
            case "Sword":

                return 5f+(level * 4.5f)+Random.Range(-5f * level, 5f * level).Truncate(1);

            case "Dagger":

                return 2f+(level * 4.5f)+Random.Range(-5.1f * level, 5.9f * level).Truncate(1);

            case "Bow":

                return 40f+(level * 5f)+Random.Range(-5.1f * level, 5.9f * level).Truncate(1);

            case "Magic":

                return 25+(level * 7.5f)+Random.Range(-5.1f * level, 5.9f * level).Truncate(1);

            case "Chest":

                return 0f;

            case "Headpiece":

                return 0f;

            default:

                return 0f;
        }
    }
}

public static class NamerHelper
{
    public static float Truncate(this float value, int digits)
    {
        double mult = System.Math.Pow(10.0, digits);
        double result = System.Math.Truncate( mult * value ) / mult;
        return (float) result;
    }
}