using UnityEngine;
using System.Collections;


public class Hand : MonoBehaviour {

  public enum HandType
  {
    LEFT = 0,
    RIGHT
  }

  // Use this for initialization
  void Start ()
  {

	}
	
	// Update is called once per frame
	void Update ()
  {

  }

  // use our equipped item, if any item is equipped
  public void UseEquipped()
  {
    if (m_equipedItem)
    {
      m_equipedItem.ActivateItem();
    }
  }

  // used for continious frame calls (e.g. auto fire on gun)
  public void UseEquippedContinious()
  {
    if (m_equipedItem)
    {
      m_equipedItem.ActivateItemContinious();
    }
  }
  // used to reset variables when we stop activating the item
  public void StopUseEquipped()
  {
    if (m_equipedItem)
    {
      m_equipedItem.StopActivateItem();
    }
  }
  // called when we pickup a new item
  public void EquipItem(Equipable a_item)
  {
    m_equipedItem = a_item;
    m_equipedItem.holdingHand = this;
    m_equipedItem.transform.SetParent(transform);
    m_equipedItem.PickUpItem();
  }
  // called when we drop an item ( picking up a new item will drop the current one)
  public void DropItem()
  {
    if (m_equipedItem)
    {
      m_equipedItem.DropItem();
      m_equipedItem.holdingHand = null;
      m_equipedItem = null;
    }
  }

  public bool HasItemEquipped() { return m_equipedItem != null; }
  public Equipable GetEquippedItem() { return m_equipedItem; }

  //public variables
  public HandType type;
  public Hand otherHand;
  public PlayerBody playerBody;

  //member variables
  Equipable m_equipedItem;
}
