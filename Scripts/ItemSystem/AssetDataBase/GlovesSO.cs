using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gloves_Equipment")]
public class GlovesSO : ItemSO
{
    public int attackSpeed;

    public int critPer;

    public int critDmg;
    
    public string[] stats = { "attackSpeed", "critPer", "critDmg" };

       public override void ApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.attackSpeed += attackSpeed;
        playerStats.critPer += critPer;
        playerStats.critDmg += critDmg;
      
    }

    
      public override void UnApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.attackSpeed -= attackSpeed;
        playerStats.critPer -= critPer;
        playerStats.critDmg -= critDmg;
    
    }


 
}
