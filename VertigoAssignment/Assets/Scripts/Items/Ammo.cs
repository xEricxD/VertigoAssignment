using UnityEngine;
using System.Collections;

// this equipable doesn't really need anything, it is only here to fill a gun if we have one
public class Ammo : Equipable
{
	// Use this for initialization
	void Start ()
  {
	
	}
	
	// Update is called once per frame
	void Update ()
  {

	}

  public void Consume()
  {
    // release the item from our hand (to make sure we can pickup a new item)
    holdingHand.DropItem();
    // destroy the ammo pack
    Destroy(gameObject);
  }
}
