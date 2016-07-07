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

  public void EquipItem(Equipable a_item)
  {
    m_equipedItem = a_item;
    m_equipedItem.transform.SetParent(transform);
    m_equipedItem.PickUpItem();
  }

  public void DropItem()
  {
    if (m_equipedItem)
    {
      m_equipedItem.DropItem();
      m_equipedItem = null;
    }
  }

  public bool HasItemEquipped()
  {
    return m_equipedItem != null;
  }

  //public variables
  public HandType type;

  //member variables
  Equipable m_equipedItem;
}
