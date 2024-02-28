using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NecklaceSO : ItemSO , IApplicableStat
{
    

    public int critDmg;

    public int manaRegen;

    public int mana;

    public int magicDefense;
    
    public string[] stats = {  "critDmg" , "manaRegen" , "mana", "magicDefense" };
    
    public override void ApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.critDmg += critDmg;
        playerStats.magicDefense += magicDefense;
        playerStats.manaRegen += manaRegen;
        playerStats.mana += mana;

    }
    
    public override void UnApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.critDmg -= critDmg;
        playerStats.manaRegen -= manaRegen;
        playerStats.mana -= mana;
        playerStats.magicDefense -= magicDefense;
        
    }
    
    // recuerda que puedes usar un constructor 

    public NecklaceSO()
    {
        Debug.Log("constructor");

    }
}
