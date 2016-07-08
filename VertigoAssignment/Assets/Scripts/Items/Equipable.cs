using UnityEngine;
using System.Collections;

public class Equipable : MonoBehaviour {

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update() {

  }

  public virtual void PickUpItem()
  {
    isPickedUp = true;
    // disable gravity
    GetComponentInChildren<Rigidbody>().isKinematic = true;
    GetComponentInChildren<Rigidbody>().useGravity = false;
    // set item to be child of hand, and reset local position
    transform.localPosition = Vector3.zero;
    transform.localRotation = Quaternion.Euler(0, 0, 0);

    // turn off all colliders
    Collider[] cols = GetComponents<Collider>();
    foreach (Collider col in cols)
      col.enabled = false;
  }

  public virtual void DropItem()
  {
    // reset all variables we changed when we picked up the item
    isPickedUp = false;
    transform.SetParent(null);
    
    GetComponentInChildren<Rigidbody>().isKinematic = false;
    GetComponentInChildren<Rigidbody>().useGravity = true;

    Collider[] cols = GetComponents<Collider>();
    foreach (Collider col in cols)
      col.enabled = true;
  }

  // called when clicking the mouse
  public virtual void ActivateItem()
  {

  }

  // called when holding the mouse down
  public virtual void ActivateItemContinious()
  {

  }

  // called when we release the mouse
  public virtual void StopActivateItem()
  {

  }

  // public variables
  public bool isPickedUp;
  public string itemName;
  public Hand holdingHand;
}
