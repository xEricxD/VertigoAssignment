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
    a_hand.DropItem();
    if (m_equippedHat)
    {
      m_equippedHat.DropItem();
      m_equippedHat.transform.SetParent(null);
      a_hand.EquipItem(m_equippedHat);
    }

    a_hat.transform.SetParent(transform);
    m_equippedHat = a_hat;
    m_equippedHat.PickUpItem();
  }

  public bool HasItemEquipped() { return m_equippedHat != null; }

  public Equipable GetEquippedItem() { return m_equippedHat; }

  // public variables
  public PlayerBody playerBody;

  // member variables
  Hat m_equippedHat;
}
