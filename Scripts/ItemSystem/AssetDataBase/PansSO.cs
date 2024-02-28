using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PansSO")]
public class PansSO : ItemSO
{
    public int defense;

    public int aggility;
    
    public string[] stats = { "defense", "aggility" };

    public override void ApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.defense += defense;
        playerStats.aggility += aggility;
        
    }
    public  override void UnApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        playerStats.defense -= defense;
        playerStats.aggility -= aggility;
    
    }
    
    public static PansSO WeaponInstance()
    {
        PansSO newWeapon = CreateInstance<PansSO>();
        
        Debug.Log("Instancia creada");

        return newWeapon;

    }
    
    
}
