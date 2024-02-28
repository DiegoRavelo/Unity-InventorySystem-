using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class ItemHovered : MonoBehaviour
{
   

    [SerializeField] private int quantity;

    [SerializeField] private Sprite sprite;



    [SerializeField] private GameObject hoveredImage;

     [SerializeField] Camera mainCamera;

     private Vector3 worldPosition;

    [SerializeField] private TMP_Text itemName;

    public ItemSO hoveredItem;

     public ItemSO HoveredItem 
    {
        get { return hoveredItem; }

        set 
        {
            if(value == null)
            {
                hoveredItem = null;

                SetBaseValues();

            }else if(hoveredItem != value)
            {
                hoveredItem = value;

                SetNewValues();     
            }
        }
    }

      public void SetNewValues()
    {
        itemImage.sprite = hoveredItem.sprite;

         hoveredImage.SetActive(true);

        itemHovering = true;
    }

     public void SetBaseValues()
    {
        hoveredImage.SetActive(false);

        hoveredItemSlot = null;

        //lastSlotHovered = null;

        itemHovering = false;

    }

    public bool itemHovering;

    public ItemSlot hoveredItemSlot;

    public ItemSlot lastSlotHovered;

    public InventoryManager inventoryManager;
    
    

    [SerializeField] private Image itemImage;



     void Start()
     {

        hoveredImage.SetActive(false);

        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();

        
     }

     public void HoveringItem(ItemSO hoveredItem, ItemSlot hoveredItemSlot)
     {
        this.HoveredItem = hoveredItem;

        this.hoveredItemSlot = hoveredItemSlot;

        
   
     }

    

    public void LookAt(InputAction.CallbackContext context)
    {
        
        Vector3 position = context.ReadValue<Vector2>();

        position.z = 12f;

        worldPosition = mainCamera.ScreenToWorldPoint(position);

        transform.position = worldPosition;

    }   

    public void ReturnHoldedItem()
    {
        hoveredItemSlot.ItemSO = HoveredItem;

        HoveredItem = null;
        
    }


 

    
}
