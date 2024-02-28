using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Start is called before the first frame update
   private void OnTriggerEnter(Collider other)
   {
      var interactable = other.GetComponent<IInteractable>();
      if(interactable == null) return;
      interactable.Interact();

   }
}
