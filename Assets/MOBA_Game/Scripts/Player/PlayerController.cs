using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_rgd = null;

    private float m_moveForce = 100f;
    private NavMeshAgent m_agent;

    private void Start ()
    {
        m_rgd = GetComponent<Rigidbody>();
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void Update ()
    {
		
	}

    public void Move(Vector3 pos)
    {
        //转向  
        transform.LookAt(new Vector3(pos.x, transform.position.y, pos.z));
        //设置寻路的目标点  
        m_agent.SetDestination(pos);
    }
}
