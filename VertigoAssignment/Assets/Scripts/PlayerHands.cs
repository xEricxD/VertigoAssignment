using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHands : MonoBehaviour {
  
	// Use this for initialization
	void Start ()
  {
    // start by setting both hands
    Hand [] hands = gameObject.GetComponentsInChildren<Hand>();
    // and find the text for UI
    m_text = GameObject.FindGameObjectWithTag("ItemText").GetComponent<Text>();

    foreach (Hand hand in hands )
    {
      if (hand.type == Hand.HandType.LEFT)
      {
        m_leftHand = hand;
        Debug.Log("left hand found, playerhand.cs");
      }
      else if (hand.type == Hand.HandType.RIGHT)
      {
        m_rightHand = hand;
        Debug.Log("right hand found, playerhand.cs");
      }
    }
	}
	
	// Update is called once per frame
	void Update ()
  {
    // start by doing a raycast to check if we're looking at anything
    FindLookatItem();

    // if we left click, use our left hand
    if (Input.GetMouseButtonDown(0))
      UseHand(m_leftHand);
    
    // if righ click, use our right hand
    if (Input.GetMouseButtonDown(1))
      UseHand(m_rightHand);
	}

  void UseHand(Hand a_hand)
  {
    // if we aren't looking at anything, try to use the item we're holding (if any)
    if (!m_lookatItem)
      UseEquipable(a_hand);
    // if we are looking at something, try to pick it up
    else
      PickupEquipable(a_hand);
  }

  void FindLookatItem()
  {
    // reset the lookat item
    m_lookatItem = null;
    m_text.text = "";

    // check if there is an item in front of us
    RaycastHit hitInfo;
    // do a raycast to check if we are looking at anything
    if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 2))
    {
      // if so, check if it is an item (using the tag)
      if (hitInfo.collider.gameObject.CompareTag("Item"))
      {
        m_lookatItem = hitInfo.collider.gameObject;
        Equipable eq = m_lookatItem.GetComponent<Equipable>();
        m_text.text = "pick up " + eq.itemName;
      }
    }
  }

  void PickupEquipable(Hand a_hand)
  {
    // if we're currently looking at an item
    if (m_lookatItem)
    {
      // drop our current item (if any)
      if (a_hand.HasItemEquipped())
        a_hand.DropItem();
      // and pick up the new item
      a_hand.EquipItem(m_lookatItem.GetComponent<Equipable>());
    }
  }

  void UseEquipable(Hand a_hand)
  {
    a_hand.UseEquipped();
  }
  
  // member variables
  Hand m_leftHand;
  Hand m_rightHand;
  GameObject m_lookatItem;
  Text m_text;
}
