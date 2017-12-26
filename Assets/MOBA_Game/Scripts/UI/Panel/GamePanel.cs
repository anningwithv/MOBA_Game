using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public Button m_btnFire = null;

	private PlayerInputController m_playerInputContoller = null;

	private void Start ()
    {
        m_btnFire.onClick.AddListener(delegate()
        {
            //PhotonGameManager.Instance.m_localPlayer.SetState(PlayerController.State.Shoot);
			PhotonGameManager.Instance.m_localPlayer.m_inputController.m_wantClickToFire = true;
        });

    }

}
