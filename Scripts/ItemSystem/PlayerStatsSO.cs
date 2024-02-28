using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;


[CreateAssetMenu(fileName = "PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    // Start is called before the first frame update

    public int attack;

    public int damage;
   
    public int critDmg;

    public int critPer;

    public int magicDefense;

    public int healthRegen;

    public int maxHealth;

    public int attackSpeed;

    public int aggility;

    public int defense;

    public int mana;

    public int manaRegen;


     public void ResetStats()
    {
        Type itemType = this.GetType();
        
        FieldInfo[] fields = itemType.GetFields();
        
        foreach (FieldInfo field in fields)
        { 
            field.SetValue(this, 0);
            
        }
    }
     
    // deberia usar un metodo para sumar y restar

   
    
}
