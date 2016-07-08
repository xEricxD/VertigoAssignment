using UnityEngine;
using System.Collections;

public class Hat : Equipable
{
	// Use this for initialization
	void Start ()
  {
	  
	}
	
	// Update is called once per frame
	void Update ()
  {
	
	}

  public override void ActivateItem()
  {
    base.ActivateItem();
    // equip the hat on our head
    holdingHand.playerBody.head.EquipItem(this, holdingHand);
  }
}
