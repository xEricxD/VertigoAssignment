using UnityEngine;
using System.Collections;

public class FlashLight : Equipable
{
	// Use this for initialization
	void Start ()
  {
    m_isActive = false;
    m_light = gameObject.GetComponentInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update ()
  {
	
	}

  // will be called when the item is used
  public override void ActivateItem()
  {
    base.ActivateItem();

    //switch the light on / off
    if (m_light)
      m_light.enabled = !m_light.enabled;
  }

  public override void PickUpItem()
  {
    base.PickUpItem();

  }

  public override void DropItem()
  {
    base.DropItem();

  }

  // member variables
  bool m_isActive;
  Light m_light;
}
