using UnityEngine;
using System.Collections;

public class Gun : Equipable
{
  enum GunState : byte
  {
    READY_TO_FIRE = 0,
    FIRING,
    OUT_OF_AMMO,
    RELOADING
  }

  enum FireMode : byte
  {
    SEMI_AUTO = 0,
    FULL_AUTO
  }

	// Use this for initialization
	void Start ()
  {
    m_currentClipSize = m_totalClipSize;
    m_state = GunState.READY_TO_FIRE;
    m_currentReloadTime = m_currentShotTime = 0;

    // find our audio source
    m_gunAudio = GetComponent<AudioSource>();

    // Set particle systems
    fireParticle.loop = false;
    smokeParticle.loop = false;

    m_fireMode = FireMode.FULL_AUTO;
  }
	
	// Update is called once per frame
	void Update ()
  {
    switch (m_state)
    {
      case (GunState.RELOADING):
        // update reload time, and finish reloading when the the time passes the total reload time
        m_currentReloadTime += Time.deltaTime;
        if (m_currentReloadTime >= m_reloadTime)
        {
          m_state = GunState.READY_TO_FIRE;
          m_currentClipSize = m_totalClipSize;
          m_currentReloadTime = 0;
        }
        break;
        
      default:
        break;
    }

    // check if we want to switch fire mode
    if (isPickedUp)
    {
      if (Input.GetKeyDown(KeyCode.F))
      {
        m_fireMode = (m_fireMode == FireMode.FULL_AUTO) ? FireMode.SEMI_AUTO : FireMode.FULL_AUTO;
        m_gunAudio.PlayOneShot(switchModeSound);
      }
    }
	}

  // will be called when the item is used
  public override void ActivateItem()
  {
    base.ActivateItem();

    // test if we have any bullets left in our clip
    switch (m_state)
    {
      case GunState.READY_TO_FIRE:
        // make sure we are actually ready to fire
        if (m_currentClipSize > 0)
        {
          // fire a single shot on semi-auto
          if (m_fireMode == FireMode.SEMI_AUTO)
            Fire();
          // or set the state to firing for full-auto
          else
            m_state = GunState.FIRING;
        }
        else
          m_state = GunState.OUT_OF_AMMO;
        break;
      
      // if we run out of ammo on a semi-auto shot, make sure to check if we can reload
      case (GunState.OUT_OF_AMMO):
        if (holdingHand.otherHand.HasItemEquipped())
        {
          // check if we're holding ammo in our other hand, which we can use to reload our gun
          Ammo ammo = holdingHand.otherHand.GetEquippedItem() as Ammo;
          if (ammo)
          {
            // if so, reload the gun and consume the ammo
            m_gunAudio.PlayOneShot(reloadSound);
            ammo.Consume();
            m_state = GunState.RELOADING;
          }
        }
        // if we didnt switch states, it means we dont have ammo in hand, so play out of ammo sound
        if (m_state == GunState.OUT_OF_AMMO)
        {
          if (!m_gunAudio.isPlaying)
            m_gunAudio.PlayOneShot(emptySound);
        }
        break;
    }
  }

  public override void ActivateItemContinious()
  {
    base.ActivateItemContinious();

    // if we're not on full auto, holding down the mouse won't do anything, so return
    if (m_fireMode != FireMode.FULL_AUTO)
      return;

    switch (m_state)
    {
      case (GunState.FIRING):
        // as long as we have bullets in our clip, try to keep firing
        if (m_currentClipSize > 0)
        {
          if (m_currentShotTime <= 0)
          {
            // fire a shot and update the shot time
            Fire();
            m_currentShotTime = m_timePerShot;
          }
          else
          {
            // wait till we can fire our next shot
            m_currentShotTime -= Time.deltaTime;
          }
        }
        else
        {
          // if there's no bullets left, switch states
          m_state = GunState.OUT_OF_AMMO;
        }
        break;

      case (GunState.OUT_OF_AMMO):
        if (holdingHand.otherHand.HasItemEquipped())
        {
          // check if we're holding ammo in our other hand, which we can use to reload our gun
          Ammo ammo = holdingHand.otherHand.GetEquippedItem() as Ammo;
          if (ammo)
          {
            // if so, reload the gun and consume the ammo
            m_gunAudio.PlayOneShot(reloadSound);
            ammo.Consume();
            m_state = GunState.RELOADING;
          }
        }
        // if we didnt switch states, it means we dont have ammo in hand, so play out of ammo sound
        if (m_state == GunState.OUT_OF_AMMO)
        {
          if (!m_gunAudio.isPlaying)
            m_gunAudio.PlayOneShot(emptySound);
        }
        break;

      case GunState.READY_TO_FIRE:
        // make sure we are actually ready to fire
        if (m_currentClipSize > 0)
          m_state = GunState.FIRING;
        else
          m_state = GunState.OUT_OF_AMMO;
        break;
    }
  }

  public override void StopActivateItem()
  {
    base.StopActivateItem();

    // stop firing the gun
    if (m_state == GunState.FIRING)
    {
      m_state = GunState.READY_TO_FIRE;
    }
  }

  void Fire()
  {
    // play the sound and particle
    m_gunAudio.PlayOneShot(fireSound);
    fireParticle.Play();

    // fire a ray in front of us, and check if we hit anything. If so, play a small particle to show the impact point
    RaycastHit hitInfo;
    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 1000))
    {
      // show a smoke particle at our impact point
      smokeParticle.transform.position = hitInfo.point;
      smokeParticle.Play();
    }

    // reduce clipsize by 1
    m_currentClipSize -= 1;
  }

  // public variables
  public AudioClip fireSound;
  public AudioClip emptySound;
  public AudioClip reloadSound;
  public AudioClip switchModeSound;
  public ParticleSystem fireParticle;
  public ParticleSystem smokeParticle;

  // member variables
  // constants
  const int m_totalClipSize = 30;
  const float m_reloadTime = 2.4f;
  const float m_timePerShot = 0.1f;

  // other member vars
  int m_currentClipSize;
  float m_currentReloadTime;
  float m_currentShotTime;
  GunState m_state;
  FireMode m_fireMode;
  AudioSource m_gunAudio;
}
