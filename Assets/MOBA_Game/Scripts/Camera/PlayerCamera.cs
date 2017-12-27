using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour 
{
	private Transform m_target = null;
	private Vector3 m_toTargetDistance = new Vector3 (0, 15, -15);
	private float m_followSmooth = 5f;

	void Start () 
	{
		
	}

	void Update () 
	{
		if (m_target == null && PhotonGameManager.Instance.m_localPlayer != null) 
		{
			m_target = PhotonGameManager.Instance.m_localPlayer.transform;
		}

		if (m_target != null) 
		{
			transform.position = Vector3.Lerp (transform.position, m_target.position + m_toTargetDistance, m_followSmooth * Time.deltaTime);
		}
	}
}
