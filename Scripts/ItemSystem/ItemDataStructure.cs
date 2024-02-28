using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.PackageManager;
using System.Reflection;
using System.Linq;

public static class ItemDataStructure
{
    public static Dictionary<EquipmentType, Type> equipmentTypeMapping = new Dictionary<EquipmentType, Type>
    {
        { EquipmentType.Head, typeof(HeadSO) },
        { EquipmentType.Chest, typeof(ChestSO) },
        { EquipmentType.Greaves, typeof(PansSO) },
        { EquipmentType.Gloves, typeof(GlovesSO) },
        { EquipmentType.Weapon, typeof(WeaponSO) },
        { EquipmentType.Ring, typeof(RingSO) },
        { EquipmentType.Necklace, typeof(NecklaceSO) },
        { EquipmentType.Talisman, typeof(TalismanSO) },
        { EquipmentType.Boots , typeof(BootsSO)}
    
    };
    
    // hay que refactorizar esto 

    public static string[] GetProperites(Type type, ItemSO instance)
    {
        FieldInfo[] fields = type.GetFields();

        foreach (FieldInfo field in fields)
        {
            if (field.Name == "stats")
            {
                string[] stats = (string[])field.GetValue(instance);
                
                return stats;
            }
        }
        
        return null;
    }
    
    public static ScriptableObject CreateInstance(EquipmentType equipmentType)
    {
        Type type = equipmentTypeMapping[equipmentType];
        
        ScriptableObject instance = ScriptableObject.CreateInstance(type) as ItemSO;
        
        FieldInfo[] fields = type.GetFields();

        return instance;


    }

    
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic
    //Legendary
}

public enum EquipmentType
{
    Head,
    Chest,
    Gloves,
    Greaves,
    Boots,
    Weapon,
    Talisman,
    Necklace,
    Ring,
    Consumables
    
}

