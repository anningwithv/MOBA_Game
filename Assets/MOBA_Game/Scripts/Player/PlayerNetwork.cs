using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : PunBehaviour
{
    private Rigidbody m_rgb;
    private PlayerController m_playerController = null;

    private Vector3 m_correctPlayerPos;
    private Quaternion m_correctPlayerRot;
    private Vector3 m_currentVelocity;
    private float m_updateTime = 0;

    void Awake ()
    {
        m_rgb = GetComponent<Rigidbody>();
        m_playerController = GetComponent<PlayerController>();
    }

    /// <summary>
    /// If it is a remote player, interpolates position and rotation
    /// received from network. 
    /// </summary>
    public void FixedUpdate()
    {
        if (!photonView.isMine)
        {
            Vector3 projectedPosition = this.m_correctPlayerPos + m_currentVelocity * (Time.time - m_updateTime);
            transform.position = Vector3.Lerp(transform.position, projectedPosition, Time.deltaTime * 4);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.m_correctPlayerRot, Time.deltaTime * 4);
        }
    }

    /// <summary>
    /// At each synchronization frame, sends/receives player input, position
    /// and rotation data to/from peers/owner.
    /// </summary>
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //We own this car: send the others our input and transform data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(m_rgb.velocity);
            stream.SendNext(m_playerController.CurState);
        }
        else
        {
            //Remote player, receive data
            m_correctPlayerPos = (Vector3)stream.ReceiveNext();
            m_correctPlayerRot = (Quaternion)stream.ReceiveNext();
            m_currentVelocity = (Vector3)stream.ReceiveNext();
            m_playerController.CurState = (PlayerController.State)stream.ReceiveNext();
            m_updateTime = Time.time;
        }
    }
}
