using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject UIpanel;

    private ItemHovered itemHovered;

    void Start()
    {
        itemHovered = GameObject.Find("HoveredItem").GetComponent<ItemHovered>();
    }

    // Update is called once per frame
   
    public void LookAt(InputAction.CallbackContext context)
    {
       if(context.started)
       {
        
          ChangeUIPanel();

       }


    }

    public void ChangeUIPanel()
    {
        if(UIpanel.activeSelf)
        {
            itemHovered.ReturnHoldedItem();
            UIpanel.SetActive(false);
        }
        else
        {

            UIpanel.SetActive(true);

        }
    }
}
