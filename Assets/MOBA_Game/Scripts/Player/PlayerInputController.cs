using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInputController : MonoBehaviour
{
    internal bool m_isControlable = false;

    internal float SpeedX { get; set; }
    internal float SpeedY { get; set; }
	internal bool m_wantClickToFire = false;

    private PlayerController m_player = null;
    private Camera m_mainCamera = null;

    void Start ()
    {
        m_player = GetComponent<PlayerController>();
        m_mainCamera = Camera.main;
    }

	void Update ()
    {
        if (m_isControlable)
        {
            HandleInput();
        }

        //ApplyInput();
	}

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
#if UNITY_ANDROID || UNITY_IPHONE
            //移动端判断如下
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
            //PC端判断如下
            if (EventSystem.current.IsPointerOverGameObject())
#endif
            {
                Debug.Log("Is Clicking UI");
                return;
            }
            //摄像机到点击位置的的射线  
            Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //判断点击的是否地形  
                if (!hit.collider.tag.Equals("Ground"))
                {
                    return;
                }
                //点击位置坐标  
                Vector3 point = hit.point;

				if (m_wantClickToFire) 
				{
					m_wantClickToFire = false;
					m_player.FireToTargetPos (point);
				} 
				else 
				{
					m_player.Move (point);
				}
            }
        }
    }

    //private void ApplyInput()
    //{
    //    Debug.Log("Move : speed x is: " + SpeedX + " speed y is: " + SpeedY);
    //    m_player.Move(SpeedX, SpeedY);
    //}
}
