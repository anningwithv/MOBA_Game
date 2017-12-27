using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public Bullet m_bulletPrefab = null;
    //public GameObject m_bulletSpawnPos = null;

	protected PhotonView m_photonView = null;
	protected PlayerController m_player = null;

	protected void Start()
    {
        m_photonView = GetComponent<PhotonView>();
        m_player = GetComponent<PlayerController>();
    }

    public virtual void Attack()
    {
        if (m_photonView.isMine == false)
        {
            return;
        }
    }
}
