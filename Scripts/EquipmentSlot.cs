using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : ItemSlot , IPointerClickHandler
{
    // Start is called before the first frame update

    [SerializeField] public PlayerStatsSO playerStatsSO;

    [SerializeField] private EquipmentType expectedEquipmentType;

    public EquipmentType ExpectedEquipmentType { get{ return expectedEquipmentType; } set { expectedEquipmentType = value; } }

    private bool statsApplayed;

    public override ItemSO ItemSO 
    {
        get
        { 
            //print("gettin equipslot");

            //SetBaseValues();

            return itemSO;
        }

        set 
        {
            if(value == null)
            {
                SetBaseValues();

                itemSO = null;


            }
            else if(itemSO != value)
            {
                itemSO = value;

                SetNewValues();     

            }
        }
    }

    
    public override void AddItem(ItemSO itemSO)
    {

        switch(expectedEquipmentType)
        {
            case EquipmentType.Head:

            HeadSO headItem = itemSO as HeadSO;
            
            this.ItemSO = headItem;

            headItem.ApplyStatsAPlayer(playerStatsSO);

            break;

            case EquipmentType.Chest:

            ChestSO chestItem = itemSO as ChestSO;
            
            this.ItemSO = chestItem;

            chestItem.ApplyStatsAPlayer(playerStatsSO);

            break;

            case EquipmentType.Greaves:

            PansSO pansItem = itemSO as PansSO;
            
            this.ItemSO = pansItem;

            pansItem.ApplyStatsAPlayer(playerStatsSO);

            break;

            default:

            itemHovered.hoveredItemSlot.ItemSO = itemSO;

            break;
        }   
 
    }


    public override void SetNewValues()
    {
        itemImage.sprite = itemSO.sprite;
        
        isFull = true;
    }

    public override void SetBaseValues()
    {
        itemImage.sprite = emptyItemSprite;
        
        isFull = false;
        
    }

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right && isFull)
        {
            ItemSO = inventoryManager.VerifyAddEquipment(this , ItemSO);
            
        }

    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            ItemSO = inventoryManager.AddItemOnSlot(this ,itemHovered.lastSlotHovered , itemHovered.hoveredItem); 

            
        }
    }



}

