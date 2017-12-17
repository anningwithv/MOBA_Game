using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonGameManager : PunBehaviour
{
    public static PhotonGameManager Instance = null;

    public RPGCamera Camera;

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
        Vector3 position = new Vector3(33.5f, 1.5f, 20.5f);

        GameObject newPlayerObject = PhotonNetwork.Instantiate("Robot Kyle RPG", position, Quaternion.identity, 0);

        Camera.Target = newPlayerObject.transform;
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
