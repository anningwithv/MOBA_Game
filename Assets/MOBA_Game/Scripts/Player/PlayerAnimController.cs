using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private PhotonView m_photonView = null;
    private Animator m_anim = null;

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_photonView = GetComponent<PhotonView>();
    }

    public void DoMove()
    {
        PlayMoveAnim();

        m_photonView.RPC("PlayMoveAnim", PhotonTargets.Others);
    }

    public void DoIdle()
    {
        PlayIdleAnim();

        m_photonView.RPC("PlayIdleAnim", PhotonTargets.Others);
    }

    [PunRPC]
    private void PlayMoveAnim()
    {
        Debug.Log("Play Move Anim");
        m_anim.SetBool("IsMove", true);
        m_anim.SetBool("IsIdle", false);
    }

    [PunRPC]
    private void PlayIdleAnim()
    {
        Debug.Log("Play Idle Anim");
        m_anim.SetBool("IsIdle", true);
        m_anim.SetBool("IsMove", false);
    }
}
