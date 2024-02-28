    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler , IPointerDownHandler , IPointerUpHandler , IPointerClickHandler , IPointerExitHandler
{

    public bool isFull;

    [SerializeField] private GameObject selectedObject;

     public GameObject SelectedObject { get { return selectedObject; } set { selectedObject = value; }}

     [SerializeField] protected InventoryManager inventoryManager;

    private bool thisItemSelected = false;

    public bool ThisItemSelected  { get { return thisItemSelected; } set { thisItemSelected = value; }}

    [SerializeField] private TMP_Text quantityText;

    [SerializeField] protected Image itemImage;

    [SerializeField] protected Sprite emptyItemSprite;

    [SerializeField] protected ItemSO itemSO;


    public virtual ItemSO ItemSO 
    {
        get { return itemSO; }

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

                //print("cambio de value");
            }
        }
    }

    /// <summary>
    /// Item Hovered varaibles 
    /// </summary>
    /// 

    public ItemHovered itemHovered;

    protected ItemHovered ItemHovered { get { return itemHovered; } set { itemHovered = value; }}


    public virtual void SetNewValues()
    {
        itemImage.sprite = itemSO.sprite;

        isFull = true;
    }

    public virtual void SetBaseValues()
    {
        itemImage.sprite = emptyItemSprite;

        isFull = false;

    }

    void Awake()
    {
        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();

        itemHovered = GameObject.Find("HoveredItem").GetComponent<ItemHovered>();


    }

    public virtual void AddItem(ItemSO itemSO)
    {
        this.ItemSO = itemSO;

    }

     public void RemoveItem()
    {
        if(!itemHovered.itemHovering && isFull )
        {
            ItemSO = null;

        }
       

    }

    public virtual void OnPointerDown(PointerEventData evenData)
    {
         if(evenData.button == PointerEventData.InputButton.Left && isFull)
        {
            itemHovered.HoveringItem(itemSO, this);

            ItemSO = null;
        }
     

    }

    
    // Necesito comprobar si en este hueco es posible meter el objeto

     public virtual void OnPointerUp(PointerEventData evenData)
    {
         if(evenData.button == PointerEventData.InputButton.Left)
        {
            ItemSO = inventoryManager.AddItemOnSlot(this,itemHovered.lastSlotHovered , itemHovered.hoveredItem); 
            
        }
      

    }

    public void OnPointerEnter(PointerEventData evenData)
    {
         itemHovered.lastSlotHovered = this;
         
         if(itemSO != null) inventoryManager.DisplayItemInformation(itemSO);

    }
    
    public void OnPointerExit(PointerEventData evenData)
    {
        
        if(itemSO != null) inventoryManager.DisplayItemInformation(null);

    }


    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right && isFull)
        {
            ItemSO = inventoryManager.VerifyAddEquipment(this, ItemSO);

        }

    }

   
 
    public void OnRightClick()
    {
        if(isFull)
        {
             RemoveItem();

        }
        else if(!isFull)
        {
            //itemSO = itemHovered.ReturnHoldedItem();

            isFull = true;

            itemImage.sprite = itemSO.sprite;


        }
       
        
    }
}
