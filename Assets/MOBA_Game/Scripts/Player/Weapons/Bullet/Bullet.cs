using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using Photon;

// represents a bullet, controls its movement and lifetime
public class Bullet : PunBehaviour
{

    // type of a bullet, ranged type is used for missiles and fixed is used for bullet put on the ground like a grenade
    public enum BulletType { Ranged, Fixed };

    public BulletType type;

    // speed of the projectile iff it is a ranged bullet
    public float m_projectileSpeed = 100;

    // how much time to be destroyed if it doesn't hit a car
    public float m_lifetime = 3;

    [HideInInspector]
    public bool m_isLocal;

    private bool m_wasHitCalled = false;
    internal PlayerController m_owner = null;
    private BulletEffect m_bulletEffect;

    public void Start()
    {
        this.m_bulletEffect = GetComponent<BulletEffect>();
    }

    void Update()
    {
        if (type == BulletType.Ranged)
        {
            // do this even for not local projectiles (dead simple dead reckoning)
            transform.Translate(Vector3.forward * m_projectileSpeed * Time.deltaTime, Space.Self);
        }

        // the rest is done only in the original copy of the weapon
        if (!m_isLocal)
            return;

        if (m_lifetime <= 0 && !m_wasHitCalled)
        {
            EventWasHit();
            m_wasHitCalled = true;
        }

        m_lifetime -= Time.deltaTime;
    }

    // calls the effect attached to this bullet
    private void EventHit(GameObject go)
    {
        m_bulletEffect.Hit(go);
    }

    // destroys the bullet and its copies over network
    private void EventWasHit()
    {
        m_bulletEffect.PreFinish();
    }

    // destroy this bullet and other copies over network in case of hit the track or other gameobjects with tag "World"
    public void OnTriggerEnter(Collider other)
    {
        if (!m_isLocal)
            return;

        if (other.tag == "World")
        {
            EventWasHit();
            return;
        }

        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && player != m_owner)
        {
            EventWasHit();

            player.UnderAttack(1);
            return;
        }
    }
}