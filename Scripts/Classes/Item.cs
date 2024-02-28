using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable 
{
    [SerializeField] private string itemName;

    [SerializeField] private int quantity;


     [SerializeField] private InventoryManager inventoryManager;

     [SerializeField] private bool stackable;

     public ItemSO itemSO;

     

     

     void Start()
     {
        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();

    
     }

     public void Interact()
     {
        inventoryManager.AddItem(itemSO);

        EventManager.OnExpCollected(quantity);

        Destroy(gameObject);

     }

    
}
