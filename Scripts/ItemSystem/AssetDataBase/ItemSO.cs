using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;


public class ItemSO : ScriptableObject  , IStatisticsManager
{
    // Start is called before the first frame update
    public Rarity rarity;

    [SerializeField] public Sprite sprite;

    [SerializeField] public string description;

    [SerializeField] public string Name;

    public int level;
    
    public void OnEnable()
    {
        FieldInfo[] infos = this.GetType().GetFields();

        foreach (FieldInfo field in infos)
        {
            if (field.FieldType == typeof(int))
            {
                field.SetValue(this , Random.Range(1, 100));
            }
        }
       
    }

    public virtual void ApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        throw new System.NotImplementedException();
    }

    public virtual void UnApplyStatsAPlayer(PlayerStatsSO playerStats)
    {
        throw new System.NotImplementedException();
    }
}

public interface IStatisticsManager
{
    void ApplyStatsAPlayer(PlayerStatsSO playerStats);
    
    void UnApplyStatsAPlayer(PlayerStatsSO playerStats);
    
}

