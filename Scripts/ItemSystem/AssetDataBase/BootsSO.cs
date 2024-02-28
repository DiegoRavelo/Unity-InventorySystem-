using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsSO : ItemSO , IApplicableStat
{
    public int aggility;
    
    public int defense;
    
    public string[] stats = { "aggility" , "defense" };
    
    public override void ApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.aggility += aggility;
        playerStats.defense += defense;

    }
    
    public override void UnApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.aggility -= aggility;
        playerStats.defense -= defense;
        
    }
}
