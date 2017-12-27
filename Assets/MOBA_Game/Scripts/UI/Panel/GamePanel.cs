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
			PlayerController player = PhotonGameManager.Instance.m_localPlayer;

				if(player.m_attackRange == PlayerController.AttackRange.Remote){
					player.m_inputController.m_wantClickToFire = true;
				}
				else
				{
					player.SetState(PlayerController.State.Attack);
				}
        });

    }

}
