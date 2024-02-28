using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSO : ItemSO , IApplicableStat
{

    public int damage;

    public int mana;

    public int manaRegen;

    public int magicDefense;
    
    public string[] stats = { "damage" , "mana" , "manaRegen" };
    
    public override void ApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.damage += damage;
        playerStats.mana += mana;
        playerStats.manaRegen += manaRegen;
        playerStats.magicDefense += magicDefense;

    }
    
    public override void UnApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.damage -= damage;
        playerStats.mana -= mana;
        playerStats.manaRegen -= manaRegen;
        playerStats.magicDefense -= magicDefense;
        
    }
    
 
}
