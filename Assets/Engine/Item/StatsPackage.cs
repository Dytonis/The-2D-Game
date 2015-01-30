﻿using UnityEngine;
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
    #endregion

    public StatsPackage()
    {
        
    }
}