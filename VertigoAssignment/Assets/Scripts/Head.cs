using UnityEngine;
using System.Collections;

public class Head : MonoBehaviour
{
	// Use this for initialization
	void Start ()
  {
	
	}
	
	// Update is called once per frame
	void Update ()
  {
	
	}

  public void EquipItem(Hat a_hat, Hand a_hand)
  {
    // start by releasing the item from our hand
    a_hand.DropItem();
    // if we already have a hat equipped,
    if (m_equippedHat)
    {
      // unequip the hat (dorp item enables the physics and resets variables)
      m_equippedHat.DropItem();
      m_equippedHat.transform.SetParent(null);
      // and place it in our hand instead of the new hat
      a_hand.EquipItem(m_equippedHat);
    }
    // equip the new hat
    a_hat.transform.SetParent(transform);
    m_equippedHat = a_hat;
    m_equippedHat.PickUpItem();
  }

  // public variables
  public PlayerBody playerBody;

  // member variables
  Hat m_equippedHat;
}
