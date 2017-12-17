using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoomPanel : MonoBehaviour
{
    public event OnStartJoinRoom OnStartJoinRoom = null;

    public Button m_btnJoinRoom = null;

    private void Start()
    {
        m_btnJoinRoom.onClick.AddListener(delegate() 
        {
            PhotonLobbyManager.Instance.StartJoinRoom();
        });
    }
}
