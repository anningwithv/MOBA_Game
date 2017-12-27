using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Shoot,
        Dead
    }

    internal PlayerAnimController m_animController = null;
	internal PlayerInputController m_inputController = null;

	private PhotonView m_photonView = null;
    private Rigidbody m_rgd = null;
    private Weapon m_weapon = null;

    private float m_moveForce = 100f;
    private NavMeshAgent m_agent;

    private State m_curState = State.Idle;
    public State CurState
    {
        get
        {
            return m_curState;
        }
        set
        {
            m_curState = value;
        }
    }

    private void Awake ()
    {
        m_photonView = GetComponent<PhotonView>();
		m_inputController = GetComponent<PlayerInputController>();
        m_animController = GetComponent<PlayerAnimController>();

        m_rgd = GetComponent<Rigidbody>();
        m_agent = GetComponent<NavMeshAgent>();
        m_weapon = GetComponent<Weapon>();
    }

    private void Update ()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (m_photonView.isMine == false)
            return;

        if (m_curState == State.Move)
        {
            if (/*m_agent.pathStatus == NavMeshPathStatus.PathComplete &&*/ 
                m_agent.remainingDistance <= 0.05f)
            {
                SetState(State.Idle);
            }
        }
    }

    public void SetState(State state)
    {
        m_curState = state;

        if (m_curState == State.Idle)
        {
            Debug.Log("Set state idle");
            m_animController.DoIdle();
        }
        else if (m_curState == State.Move)
        {
            Debug.Log("Set state move");
            m_animController.DoMove();
        }
        else if (m_curState == State.Shoot)
        {
            Debug.Log("Set state shoot");
            m_animController.DoShoot();
            StartCoroutine(Fire());
        }
        else if (m_curState == State.Dead)
        {
            Debug.Log("Set state dead");
            m_animController.DoDead();
        }
        //m_animController.PlayAnimation();
    }

    public void Move(Vector3 pos)
    {
        SetState(State.Move);

        transform.LookAt(new Vector3(pos.x, transform.position.y, pos.z));

		m_agent.isStopped = false;
        m_agent.SetDestination(pos);
    }

    public void UnderAttack(int damage)
    {
        Debug.Log("Player under attack");
        SetState(State.Dead);
        //OnUnderAttack();

        //m_photonView.RPC("OnUnderAttack", PhotonTargets.Others);
    }

	public void FireToTargetPos(Vector3 pos)
	{
		m_agent.isStopped = true;

		transform.LookAt(new Vector3(pos.x, transform.position.y, pos.z));

		SetState (State.Shoot);
	}


    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(1.5f);
        m_weapon.Fire();

        yield return new WaitForSeconds(0.5f);
        SetState(State.Idle);
    }
}
