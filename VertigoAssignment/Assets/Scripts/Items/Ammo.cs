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
    holdingHand.DropItem();
    Destroy(gameObject);
  }
}
