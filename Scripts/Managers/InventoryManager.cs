using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using TMPro;
using System.Reflection;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update

    public ItemSlot[] itemSlot;

    public EquipmentSlot[] equipmentSlot;

    public int itemsHoleded;

    public ItemHovered itemHovered;

    [SerializeField] private PlayerStatsSO playerStatsSO;

    public TMP_Text infoDisplayerName, infoDisplayerDescription, infoDisplayerStats;
    
    
    
    
    
    private void Awake()
    {
        //Codigo que me permite añadir mas itemSlots sin tener que hacerlo desde el inspector, la funcion GetComponentsInChild(); no es lo que realmente parece y hace cosas raras
        // la solución ha sido usar GetChild para contar y luego con un for ir añadiendo cada componente. creo que se podria unar las dos ultimas linas
        // vale si se puede 
        
        itemHovered = GameObject.Find("HoveredItem").GetComponent<ItemHovered>();

        playerStatsSO.ResetStats();
        
        GameObject inventorySlots = GameObject.Find("Inventory_Slots");

        Transform[] slots = new Transform[inventorySlots.transform.childCount];
        
        itemSlot = new ItemSlot[slots.Length];
        
        Debug.Log(slots.Length);

        for (int i = 0; i < slots.Length; i++)
        {
            itemSlot[i] = (slots[i] = inventorySlots.transform.GetChild(i)).GetComponent<ItemSlot>();
        }
        
    }
    public void AddItem(ItemSO itemSO)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if(!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(itemSO);
                ItemsHoleded();
                return;
                
            }
            
        }

        throw new System.Exception("No hay espacio disponible para agregar el ítem.");

    }

    #region Explicación AddItemOnSlot

   /*
   Este bloque de código gestiona todas las operaciones cuando un objeto interactúa con el inventario. En primer lugar, se verifica si el slot en el que intentamos colocar el objeto es de tipo EquipmentSlot. Esto se debe a que EquipmentSlot es una clase heredada de ItemSlot con los mismos métodos de PointerEventData.

   Luego, se realiza una comprobación utilizando el método MatchEquipmentType, el cual devuelve true o false según el tipo de equipment que el EquipmentSlot acepta. Si esta condición se cumple, se trabaja con un objeto de tipo equipment y un EquipmentSlot, y se entra en el método HandleEquipmentSlot, que gestiona tres situaciones diferentes:

   1. Si el slot con el que estamos interactuando es el mismo en el que se ha colocado el objeto, significa que el jugador quiere dejarlo en el mismo lugar donde lo recogió. En este caso, se devuelve el ItemSO con "return ItemSO".

   2. La segunda situación ocurre cuando el hueco está lleno. En este caso, aunque el objeto se pueda colocar, el hueco está ocupado. La solución es intercambiar los objetos: el objeto del hueco va al hueco del item recogido y viceversa.

   3. La tercera situación es cuando el hueco está vacío. En este caso, simplemente se introduce el item en el hueco vacío.

   Cuando el slot no es de tipo EquipmentSlot, se aplican las mismas tres situaciones, pero trabajando simplemente con huecos de ItemSlot, no de EquipmentSlot.

   Cabe destacar que en todas estas situaciones, se maneja siempre la asignación de null al HoveredItem. ItemHovered es mi clase que maneja el item que estamos sujetando. Puede que esta no sea la manera más convencional, pero ha sido mi enfoque para trabajar con un item que no se encuentra realmente en ningún lugar concreto.

   Aun queda por progamar nuevas situaciones como items que se pueden acumlar como el dinero o huecos para items consumibles.


   Todo este codigo esta creado de forma propia y personal, pero entiendo que muchas otras personas hayan usado este enfoque, igualmente sigo dudando de como trabajo con los ScriptableObjects y su asignación a Null;
   
   
   El 24 de enero de 2024, todas las operaciones previamente explicadas estaban funcionando de manera óptima. Sin embargo, me di cuenta de que faltaba comprobar una situación específica: al desplazar objetos ya equipados, surgían problemas debido a que anteriormente el código solo verificaba si el slot de destino era de tipo "EquipmentSlot".
   
   No obstante, existían otras situaciones que venían de un "EquipmentSlot". Para abordar este problema, introduje un nuevo parámetro llamado "sourceSlot". Este parámetro permite que el método identifique el tipo de slot con el que estamos trabajando. Ahora, cuando el código se ejecuta desde un "EquipmentSlot" o un "ItemSlot", se envía la información

   de esa instancia de clase con un "this". Es importante destacar que también he modificado los nombres de los parámetros a opciones más claras, como "targetSlot" y "newItem", ya que los anteriores podrían resultar confusos, especialmente con el nuevo parámetro (anteriormente eran "itemSlot" y "itemSO").
   */

     #endregion
     
     
     // el problema esta en que necesito comprobar que cuando vengo de un equipmentslot, solo verifica si donde lo estoy dejando es un Equip o itemSlot, creo que puedo añadir una nuevo parametro rollo hoveredSlot algo asi,
     // entonces si el hovered slot es equipslot y el itemSlot es 

    public ItemSO AddItemOnSlot(ItemSlot sourceSlot ,ItemSlot targetSlot, ItemSO newItem)
    {
        if (sourceSlot is EquipmentSlot)
        {
            if (targetSlot.ItemSO?.GetType() == newItem.GetType())
            {
                ItemSO oldItem = targetSlot.ItemSO;
                
                ApplyPlayerStats(targetSlot.ItemSO, true);
                
                targetSlot.ItemSO = newItem;
                
                ApplyPlayerStats(newItem, false);
                
                itemHovered.HoveredItem = null;
                
                return oldItem;
                
            }else if (!targetSlot.isFull && !(targetSlot is EquipmentSlot))
            {
                targetSlot.ItemSO = newItem;
                
                ApplyPlayerStats(newItem, false);

                itemHovered.HoveredItem = null;
                
                Debug.Log("UnEquipment");

                return null;

            }
            else
            {
                itemHovered.HoveredItem = null;
                
                return newItem;
            }
        }
        
        else if(targetSlot is EquipmentSlot)
        {
            if(MatchEquipmentType(targetSlot, newItem))
            {
                return HandleEquipmentSlot(targetSlot, newItem);
                
            }
            
            else
            {
                itemHovered.HoveredItem = null;

                return newItem;
            }

        }
        
         return HandleRegularSlot(targetSlot , newItem);

    }

    
    public ItemSO HandleEquipmentSlot(ItemSlot targetSlot, ItemSO newItem)
    {
        if(itemHovered.hoveredItemSlot == itemHovered.lastSlotHovered)
        {
            itemHovered.HoveredItem = null;

            return newItem;

        }else if(targetSlot.isFull)
        {
            ItemSO oldItem = targetSlot.ItemSO;
            
            ApplyPlayerStats(targetSlot.ItemSO, false);
            
            targetSlot.ItemSO = newItem;
            
            ApplyPlayerStats(newItem, true);
            
            itemHovered.HoveredItem = null;
            
            Debug.Log("switching Equipment");
            
            return oldItem;

        }else
        {
            
            targetSlot.ItemSO = newItem;
            
            itemHovered.HoveredItem = null;
            
            Debug.Log("Equiping Equipment");

            ApplyPlayerStats(newItem, true);

            return null;

        }
        

    }

    public ItemSO HandleRegularSlot(ItemSlot targetSlot, ItemSO newItem)
    {
        if(targetSlot.isFull)
        {
            ItemSO oldItem = targetSlot.ItemSO;
            targetSlot.ItemSO = newItem;
            itemHovered.HoveredItem = null;
            
            print("swaping items");
            
            return oldItem; 

        }else if(itemHovered.hoveredItemSlot == itemHovered.lastSlotHovered)
        {
            itemHovered.HoveredItem = null;

            return newItem;

        }else
        {
            targetSlot.ItemSO = newItem;
            itemHovered.HoveredItem = null;

            return null;
        }

    }

    public ItemSO HandleSourceSlot(ItemSlot targetSlot, ItemSO newItem)
    {
        return null;
    }

    
    public bool MatchEquipmentType(ItemSlot itemSlot, ItemSO itemSO)
    {
        EquipmentSlot equipmentSlot = itemSlot as EquipmentSlot;

        return ItemDataStructure.equipmentTypeMapping.TryGetValue(equipmentSlot.ExpectedEquipmentType, out Type expectedItemType) &&
               expectedItemType.IsAssignableFrom(itemSO.GetType());

    }
    
    // Hay que cambiarle el nombre a este metodo es 0 claro 
    public ItemSO VerifyAddEquipment(ItemSlot itemSlot, ItemSO itemSO)
    {
        if(itemSlot is EquipmentSlot)
        {
            AddItem(itemSO);
            
            ApplyPlayerStats(itemSO, false);

            return null;

        }
        else
        {
            for(int i = 0; i < equipmentSlot.Length; i++)
            {
                if(MatchEquipmentType(equipmentSlot[i], itemSO))
                {
                    if(equipmentSlot[i].isFull)
                    {
                        ItemSO oldItem = equipmentSlot[i].ItemSO;
                        
                        ApplyPlayerStats(oldItem, false);
                        
                        equipmentSlot[i].ItemSO = itemSO;
                        
                        ApplyPlayerStats(itemSO, true);

                        return oldItem;
                    }
                    else
                    {
                        equipmentSlot[i].ItemSO = itemSO;
                        
                        ApplyPlayerStats(itemSO, true);

                        return null;

                    }
                    
                }
                
            }
            

        }

        return itemSO;
        
        
    }
    

    public void DisplayItemInformation(ItemSO itemToDisplay)
    {
        //infoDisplayer.text = infoDisplayer.text.Replace("\r", "");

        if (itemToDisplay == null)
        {
            infoDisplayerName.text =  "";
            infoDisplayerDescription.text = "";
            infoDisplayerStats.text = "";
        }
        else
        {
            string name = itemToDisplay.Name + " (" +  itemToDisplay.rarity + ")" + " LVL" + itemToDisplay.level ;
            infoDisplayerName.text =  name;
            infoDisplayerName.text = infoDisplayerName.text.Replace("\r", "");
            infoDisplayerDescription.text = itemToDisplay.description;

            infoDisplayerStats.text = "";
        
            Type itemType = itemToDisplay.GetType();
        
            FieldInfo[] fields = itemType.GetFields();

            string[] desiredFields = ItemDataStructure.GetProperites(itemType, itemToDisplay);

            foreach (FieldInfo field in fields)
            {
                // Campo individual de tipo objeto (diego esto no era un array), busco uno que este dentro de playerStats y tenga el mismo nombre que un campo del propio item
                // para usarlo mas tarde en el .text, es decir Campo campo = campo en estadisticas con el mismo nombre que el campo de mi item
                
                FieldInfo playerStatsField = playerStatsSO.GetType().GetField(field.Name);
                
                //si el campo es un int y su nombre esta dentro de ni string[] de nombre correctos significa que he encontrado el object stat concreto 
                // y ahora simplemente lo muestro por el displayerStats con ciertos colores cuando hagan las compraciones 
            
                if (field.FieldType == typeof(int) && desiredFields.Contains(field.Name))
                {
                    if ((int)playerStatsField.GetValue(playerStatsSO) > (int)field.GetValue(itemToDisplay))
                    {
                        infoDisplayerStats.text += field.Name + ": " + playerStatsField.GetValue(playerStatsSO) + "<color=red>->" + field.GetValue(itemToDisplay).ToString() + "</color>\n"  ; 
                    
                    }else if ((int)playerStatsField.GetValue(playerStatsSO) < (int)field.GetValue(itemToDisplay))
                    {
                        infoDisplayerStats.text += field.Name + ": " + playerStatsField.GetValue(playerStatsSO) + "<color=green>->" + field.GetValue(itemToDisplay).ToString() + " </color>\n"  ; 
                    }
                    else
                    {
                        infoDisplayerStats.text += "<color=yellow>" + field.Name + ": " + playerStatsField.GetValue(playerStatsSO) + "->" + field.GetValue(itemToDisplay).ToString() + " </color>\n" ; 
                    
                    }
               
                
                }
            
            }
            
        }
        
        
        
    }
    
    public void ApplyPlayerStats(ItemSO newItem, bool applyStats)
    {
        FieldInfo[] infos = newItem.GetType().GetFields();
        
        if (newItem is not IStatisticsManager) return;
        
        IStatisticsManager statAplayer = (IStatisticsManager)newItem;

        if (applyStats)
        {
            statAplayer.ApplyStatsAPlayer(playerStatsSO);
            EventManager.OnStatChanged(newItem, infos , true);
        }
        else
        {
            statAplayer.UnApplyStatsAPlayer(playerStatsSO);
            EventManager.OnStatChanged(newItem, infos , false);
        }
        
        
        
    }

    // Esto es horroroso la verdad 
    public void ItemsHoleded()
    {
        int a = 0;

         for (int i = 0; i < itemSlot.Length; i++)
        {
            if(itemSlot[i].isFull)
            {
                a++;
            }
            
        }

        itemsHoleded = a;

    }
     
    // Esto
    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].SelectedObject.SetActive(false);
            itemSlot[i].ThisItemSelected = false;
            
        }
    }

   
}
