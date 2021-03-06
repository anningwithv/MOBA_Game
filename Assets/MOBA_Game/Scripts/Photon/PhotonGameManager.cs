﻿using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonGameManager : PunBehaviour
{
    public static PhotonGameManager Instance = null;

	public string m_playerName = string.Empty;
    //public RPGCamera Camera;

    internal PlayerController m_localPlayer = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    private void SpawnPlayer()
    {
        Vector3 position = new Vector3(1f, 1f, 10f);

		GameObject newPlayerObject = PhotonNetwork.Instantiate(m_playerName, position, Quaternion.identity, 0);
        newPlayerObject.GetComponent<PlayerInputController>().m_isControlable = true;
        m_localPlayer = newPlayerObject.GetComponent<PlayerController>();

        //Camera.Target = newPlayerObject.transform;
    }

    public virtual void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { maxPlayers = 4 }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");

        SpawnPlayer();
    }
}
