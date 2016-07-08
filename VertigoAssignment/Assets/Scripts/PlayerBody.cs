using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerBody : MonoBehaviour {
  
	// Use this for initialization
	void Start ()
  {
    // start by finding all body parts
    Hand [] hands = gameObject.GetComponentsInChildren<Hand>();
    foreach (Hand hand in hands )
    {
      if (hand.type == Hand.HandType.LEFT)
      {
        leftHand = hand;
        Debug.Log("left hand found, playerhand.cs");
      }
      else if (hand.type == Hand.HandType.RIGHT)
      {
        rightHand = hand;
        Debug.Log("right hand found, playerhand.cs");
      }
    }

    head = gameObject.GetComponentInChildren<Head>();
    head.playerBody = this;

    // find the text for UI
    m_text = GameObject.FindGameObjectWithTag("ItemText").GetComponent<Text>();

    // set other hand in both hands
    leftHand.otherHand = rightHand;
    rightHand.otherHand = leftHand;

    // set owning body
    leftHand.playerBody = this;
    rightHand.playerBody = this;
	}
	
	// Update is called once per frame
	void Update ()
  {
    // start by doing a raycast to check if we're looking at anything
    FindLookatItem();

    // if we left click, use our left hand
    if (Input.GetMouseButtonDown(0))
      UseHand(leftHand);
    if (Input.GetMouseButton(0))
      UseHandContinious(leftHand);
    if (Input.GetMouseButtonUp(0))
      StopUseHand(leftHand);
    
    // if righ click, use our right hand
    if (Input.GetMouseButtonDown(1))
      UseHand(rightHand);
    if (Input.GetMouseButton(1))
      UseHandContinious(rightHand);
    if (Input.GetMouseButtonUp(1))
      StopUseHand(rightHand);
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

  // called when holding down the mouse button
  void UseHandContinious(Hand a_hand)
  {
    a_hand.UseEquippedContinious();
  }

  // called when releasing the mouse button
  void StopUseHand(Hand a_hand)
  {
    a_hand.StopUseEquipped();
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
        // set the text in our screen, notifing what were looking at
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

  // public variables
  public Hand leftHand;
  public Hand rightHand;
  public Head head;

  // member variables
  GameObject m_lookatItem;
  Text m_text;
}
