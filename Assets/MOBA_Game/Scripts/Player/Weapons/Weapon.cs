using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Bullet m_bulletPrefab = null;
    public GameObject m_bulletSpawnPos = null;

    private PhotonView m_photonView = null;
    private PlayerController m_player = null;

    private void Start()
    {
        m_photonView = GetComponent<PhotonView>();
        m_player = GetComponent<PlayerController>();
    }

    public void Fire()
    {
        if (m_photonView.isMine == false)
        {
            return;
        }

        GameObject bulletGO = null;

        if (m_bulletPrefab.type == Bullet.BulletType.Ranged)
        {
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            eulerAngles.x = 0;

            Quaternion bulletRotation = transform.rotation;
            bulletRotation.eulerAngles = eulerAngles;

            bulletGO = PhotonNetwork.Instantiate("Bullet/" + m_bulletPrefab.name, m_bulletSpawnPos.transform.position, bulletRotation, 0);
            bulletGO.GetComponent<Bullet>().m_isLocal = true;
            bulletGO.GetComponent<Bullet>().m_owner = m_player;
        }
    }
}
