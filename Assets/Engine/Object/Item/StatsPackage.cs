using UnityEngine;
using System.Collections;

public class StatsPackage
{
    #region ItemStats
    public float DAMAGE;
    public float SPEED;
    public float AMMO;
    public float RANGE;
    public float HEAL;
    public bool CONSUMABLE;
    #endregion
    
    #region WearableStats
    public float ARMOR;
    #endregion
    
    #region AllStats
    public string ENCHANT;
    public string NAME;
    public string LEVEL;
    public string TYPE;
    public Sprite SPRITE;
    #endregion

    public StatsPackage()
    {

    }

    public void AutocompleteStats(StatsPackage package)
    {
        this.DAMAGE = package.DAMAGE;
        this.SPEED = package.SPEED;
        this.AMMO = package.AMMO;
        this.RANGE = package.RANGE;
        this.HEAL = package.HEAL;
        this.CONSUMABLE = package.CONSUMABLE;
        this.ARMOR = package.ARMOR;
        this.ENCHANT = package.ENCHANT;
        this.NAME = package.NAME;
        this.LEVEL = package.LEVEL;
        this.TYPE = package.TYPE;
        this.SPRITE = package.SPRITE;
    }
}