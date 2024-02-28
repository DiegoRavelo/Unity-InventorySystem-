using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using System.Reflection;
using System.Linq;

public class PlayerStatsDisplay : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private PlayerStatsSO playerStats;

    public TMP_Text attack;

    public TMP_Text damage;
    
    public TMP_Text critDmg;

    public TMP_Text critPer;
       
    public TMP_Text magicDefense;
    
    public TMP_Text healthRegen;
       
    public TMP_Text maxHealth;
        
    public TMP_Text attackSpeed;
      
    public TMP_Text aggility;
       
    public TMP_Text defense;
    
    public TMP_Text mana;

    public TMP_Text manaRegen;


    private FieldInfo[] fieldInfos;

    private FieldInfo[] statsInfos;

    [SerializeField] private List<string> fieldNames = new List<string>();


    public void Start()
    {
        fieldInfos= this.GetType().GetFields();

        statsInfos = playerStats.GetType().GetFields();

        foreach (FieldInfo field in fieldInfos)
        {
            if (field.FieldType == typeof(TMP_Text))
            {
                fieldNames.Add(field.Name);
            }

        }
    }


    public void OnEnable()
    {
        EventManager.StatChanged += EventManagerOnStatChanged;

    }

    public void OnDisable()
    {
        EventManager.StatChanged -= EventManagerOnStatChanged;
    }

    public void EventManagerOnStatChanged(ItemSO item, FieldInfo[] itemFields, bool applyStats)
    {
        if (applyStats)
        {
            foreach (FieldInfo field in itemFields)
            {
                if (fieldNames.Contains(field.Name))
                {
                    FieldInfo stat = playerStats.GetType().GetField(field.Name);

                    FieldInfo textComp = this.GetType().GetField(field.Name);

                    TMP_Text textComponent = (TMP_Text)textComp.GetValue(this);

                    textComponent.text = stat.GetValue(playerStats).ToString();

                }


            }
        }
        else
        {
            foreach (FieldInfo field in itemFields)
            {
                if (fieldNames.Contains(field.Name))
                {
                    FieldInfo stat = playerStats.GetType().GetField(field.Name);

                    FieldInfo textComp = this.GetType().GetField(field.Name);

                    TMP_Text textComponent = (TMP_Text)textComp.GetValue(this);

                    textComponent.text = stat.GetValue(playerStats).ToString();
                    
                }


            }
            
        }
        

    }
}
