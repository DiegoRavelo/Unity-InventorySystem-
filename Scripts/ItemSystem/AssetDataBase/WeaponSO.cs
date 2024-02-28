using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

[CreateAssetMenu(fileName = "Weapon_Equipment")]
public class WeaponSO : ItemSO , IStatisticsManager
{
    public int attack;

    public int damage;

    public int critPer;

    public int critDmg;

    public string[] stats = { "damage", "critPer", "critDmg" };
    

       public override void ApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.damage += damage;
        playerStats.attack += attack;
        playerStats.critPer += critPer;
        playerStats.critDmg += critDmg;
   
    }

    
      public override void UnApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.damage -= damage;
        playerStats.attack -= attack;
        playerStats.critPer -= critPer;
        playerStats.critDmg -= critDmg;
        
    }
      
    // public void OnEnable()
    // {
    //     FieldInfo[] infos = this.GetType().GetFields();
    //
    //     foreach (FieldInfo field in infos)
    //     {
    //         if (field.FieldType == typeof(int))
    //         {
    //             field.SetValue(this , Random.Range(1, 100));
    //         }
    //     }
    //    
    // }

   
}
