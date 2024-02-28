using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalismanSO : ItemSO , IApplicableStat
{
    // Start is called before the first frame update
    public int aggility;

    public int critPer;
    
    public int mana;

    public int manaRegen;

    public int magicDefense;
    
    public string[] stats = { "critPer" , "manaRegen " , "aggility" , "critPer", "magicDefense" };
    
    public override void ApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.aggility += aggility;
        playerStats.critPer += critPer;
        playerStats.mana += mana;
        playerStats.manaRegen += manaRegen;
        playerStats.magicDefense += magicDefense;

    }
    
    public override void UnApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.critPer -= critPer;
        playerStats.aggility -= aggility;
        playerStats.mana -= mana;
        playerStats.manaRegen -= manaRegen;
        playerStats.magicDefense -= magicDefense;

    }

}
