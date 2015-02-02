using UnityEngine;
using System.Collections;

public class InventoryItem : ScriptableObject
{
    public StatsPackage stats;

    public InventoryItem() { }	

    #region serialize
    public float DAMAGE;
    public float SPEED;
    public float AMMO;
    public float RANGE;
    public float HEAL;
    public bool CONSUMABLE;
    public string ENCHANT;
    public string NAME;
    public string LEVEL;
    public string TYPE;
    public Sprite SPRITE;
    #endregion

    public void init(StatsPackage package)
    {
        stats = new StatsPackage();
        stats.AutocompleteStats(package);

        DAMAGE = stats.DAMAGE;
        SPEED = stats.SPEED;
        AMMO = stats.AMMO;
        RANGE = stats.RANGE;
        HEAL = stats.HEAL;
        CONSUMABLE = stats.CONSUMABLE;
        ENCHANT = stats.ENCHANT;
        NAME = stats.NAME;
        LEVEL = stats.LEVEL;
        TYPE = stats.TYPE;
        SPRITE = stats.SPRITE;
    }

    public InventoryItem Clone()
    {
        InventoryItem clone = ScriptableObject.CreateInstance<InventoryItem>();
        clone.init(stats);
        return clone;
    }
}
