using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Head_Equipment")]
public class HeadSO : ItemSO 
{
    // Start is called before the first frame update
    public int defense;
    
    public int maxHealth;
    
    public int healthRegen;
    
    
    public string[] stats = { "defense" , "maxHealth" , "healthRegen" };

      public override void ApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.defense += defense;
        playerStats.maxHealth += maxHealth;
        playerStats.healthRegen += healthRegen;
    }

    
      public override void UnApplyStatsAPlayer(PlayerStatsSO playerStats)
      {
          playerStats.defense -= defense;
          playerStats.maxHealth -= maxHealth;
          playerStats.healthRegen -= healthRegen;

      }


}
