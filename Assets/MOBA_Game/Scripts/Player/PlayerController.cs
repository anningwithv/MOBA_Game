using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move
    }

    private PlayerAnimController m_animController = null;
    private Rigidbody m_rgd = null;

    private float m_moveForce = 100f;
    private NavMeshAgent m_agent;

    private State m_curState = State.Idle;

    private void Start ()
    {
        m_animController = GetComponent<PlayerAnimController>();

        m_rgd = GetComponent<Rigidbody>();
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void Update ()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (m_curState == State.Move)
        {
            if (/*m_agent.pathStatus == NavMeshPathStatus.PathComplete &&*/ 
                m_agent.remainingDistance <= 0.05f)
            {
                SetState(State.Idle);
            }
        }
    }

    private void SetState(State state)
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
    }

    public void Move(Vector3 pos)
    {
        SetState(State.Move);

        transform.LookAt(new Vector3(pos.x, transform.position.y, pos.z));

        m_agent.SetDestination(pos);
    }
}
