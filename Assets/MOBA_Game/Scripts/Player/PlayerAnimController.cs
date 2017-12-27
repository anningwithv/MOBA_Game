using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private PhotonView m_photonView = null;
    private Animator m_anim = null;
    private PlayerController m_playerController = null;

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_photonView = GetComponent<PhotonView>();
        m_playerController = GetComponent<PlayerController>();
    }

    //public void PlayAnimation()
    //{
    //    SetAnimParamByState();

    //    m_photonView.RPC("SetAnimParamByState", PhotonTargets.Others);
    //}

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

    public void DoAttack()
    {
        PlayShootAnim();

        m_photonView.RPC("PlayShootAnim", PhotonTargets.Others);
    }

    public void DoDead()
    {
        PlayDeadAnim();

        m_photonView.RPC("PlayDeadAnim", PhotonTargets.Others);
    }

    [PunRPC]
    private void PlayMoveAnim()
    {
        Debug.Log("Play Move Anim");
        //SetAnimParamByState();
        m_anim.SetBool("IsMove", true);
        m_anim.SetBool("IsIdle", false);
        m_anim.SetBool("IsShoot", false);
    }

    [PunRPC]
    private void PlayIdleAnim()
    {
        Debug.Log("Play Idle Anim");
        //SetAnimParamByState();
        m_anim.SetBool("IsMove", false);
        m_anim.SetBool("IsIdle", true);
        m_anim.SetBool("IsShoot", false);
    }

    [PunRPC]
    private void PlayShootAnim()
    {
        Debug.Log("Play Shoot Anim");
        //SetAnimParamByState();
        m_anim.SetBool("IsMove", false);
        m_anim.SetBool("IsIdle", false);
        m_anim.SetBool("IsShoot", true);
    }

    [PunRPC]
    private void PlayDeadAnim()
    {
        Debug.Log("Play Dead Anim");

        m_anim.SetBool("IsDead", true);
    }

    [PunRPC]
    private void SetAnimParamByState()
    {
        switch (m_playerController.CurState)
        {
            case PlayerController.State.Idle:
                m_anim.SetBool("IsMove", false);
                m_anim.SetBool("IsIdle", true);
                m_anim.SetBool("IsShoot", false);
                break;
            case PlayerController.State.Move:
                m_anim.SetBool("IsMove", true);
                m_anim.SetBool("IsIdle", false);
                m_anim.SetBool("IsShoot", false);
                break;
            case PlayerController.State.Attack:
                m_anim.SetBool("IsMove", false);
                m_anim.SetBool("IsIdle", false);
                m_anim.SetBool("IsShoot", true);
                break;
        }
    }
}
