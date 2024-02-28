using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chest_Equipment")]
public class ChestSO : ItemSO 
{
    public int healthRegen;

    public int defense;

    public int maxHealth;
    
    public string[] stats = { "healthRegen", "defense", "maxHealth" };


      public override void ApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.healthRegen += healthRegen;
        playerStats.defense += defense;
        playerStats.maxHealth += maxHealth;
   
    }

    
      public override void UnApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.healthRegen -= healthRegen;
        playerStats.defense -= defense;
        playerStats.maxHealth -= maxHealth;
   
        
    }
}
