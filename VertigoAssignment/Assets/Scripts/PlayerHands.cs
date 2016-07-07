using UnityEngine;
using System.Collections;

public class PlayerHands : MonoBehaviour {
  
	// Use this for initialization
	void Start ()
  {
    // start by setting both hands
    Hand [] hands = GetComponentsInParent<Hand>();

    foreach (Hand hand in hands )
    {
      if (hand.type == Hand.HandType.LEFT)
        m_leftHand = hand;
      else if (hand.type == Hand.HandType.RIGHT)
        m_rightHand = hand;
    }
	}
	
	// Update is called once per frame
	void Update ()
  {
    // if we left click, use the item in our left hand
	  if (Input.GetMouseButtonDown(0))
    {
      if (!m_leftHand)
        PickupEquipable(m_leftHand);
      else
        UseEquipable(m_leftHand);
    }
    // if righ click, use the item in our right hand
    if (Input.GetMouseButtonDown(1))
    {
      if (!m_rightHand)
        PickupEquipable(m_rightHand);
      else
        UseEquipable(m_rightHand);
    }
    // if we already have an item equipped, we can replace it with an item on the floor by holding the respective mouse button
	}

  void PickupEquipable(Hand a_hand)
  {
    // first check if there is an item in front of us
    Vector3 forward = transform.forward;
    RaycastHit hitInfo;

    // do a raycast to check if we are looking at anything
    if (Physics.Raycast(transform.position, forward, out hitInfo, 5))
    {
      // if so, check if it is an item (using the tag)
      if (hitInfo.collider.gameObject.CompareTag("Item"))
      {
        Debug.Log("We hit an item, now pick it up!");
        // get the equipable component and equip the item into the correct hand
        a_hand.EquipItem(hitInfo.collider.gameObject.GetComponent<Equipable>());
      }
      else
        Debug.Log("We did not hit an item!");
    }
    else
      Debug.Log("We did not hit an item!");
  }

  void UseEquipable(Hand a_hand)
  {
    a_hand.UseEquipped();
  }
  
  // member variables
  Hand m_leftHand;
  Hand m_rightHand;
}
