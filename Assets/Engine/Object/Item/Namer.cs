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
            "Trash" 
        },
        new List<string>() 
        { 
            "OK",
            "Average",
            "Not-So Horrible",
            "Acceptable",
            "Not-Special"
        },
        new List<string>() 
        { 
            "Fine",
            "Quality",
            "Good",
            "Nice",
            "Shiney"
        },
        new List<string>() 
        { 
            "Great",
            "Well-Made",
            "Very Good",
            "Keen",
            "Sharp" 
        },
        new List<string>() 
        { 
            "Perfect",
            "Amazing",
            "Unbreakable",
            "Fearful",
            "God-Like" 
        }
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

    public static StatsPackage getStats(int level)
    {
        StatsPackage package = new StatsPackage();

        package.TYPE = getType();          //name uses type, so we must run type first
        package.NAME = getName(level) + " " + package.TYPE.ToString();
        package.DAMAGE = getDamage(level, package.TYPE.ToString());

        return package;
    }

    private static string getType()
    {
        int pick = Random.Range(0, 6);

        return ItemTypes[pick];
    }

    private static string getName(int level)
    {
        int pick = Random.Range(0, 5);

        return ItemNames[level-1][pick];
    }

    private static float getDamage(int level, string type)
    {
        switch(type)
        {
            case "Sword":

                return 5f+(level * 4.5f)+Random.Range(-5*level, 5*level);

            case "Dagger":

                return 2f+(level * 4.5f)+Random.Range(-5*level, 5*level);

            case "Bow":

                return 40f+(level*5f)+Random.Range(-5*level, 5*level);

            case "Magic":

                return 25+(level*7.5f)+Random.Range(-5*level, 5*level);

            case "Chest":

                return 0f;

            case "Head":

                return 0f;

            default:

                return 0f;
        }
    }
}