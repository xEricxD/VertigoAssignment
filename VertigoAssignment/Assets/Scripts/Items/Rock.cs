using UnityEngine;
using System.Collections;

public class Rock : Equipable
{
  // Use this for initialization
  void Start()
  {
    // find audio source
    m_audio = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  // will be called when the item is used
  public override void ActivateItem()
  {
    base.ActivateItem();

    // get forward
    Vector3 forward = holdingHand.playerBody.transform.forward;
    // release the rock from the hand
    holdingHand.DropItem();

    // add forward force to the rock
    GetComponent<Rigidbody>().AddForce(forward * 400);

    // play throwing sound
    m_audio.Play();
  }

  AudioSource m_audio;
}
