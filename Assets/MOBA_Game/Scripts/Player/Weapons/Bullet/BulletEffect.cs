using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using Photon;

// Base class to create bullet effects
public class BulletEffect : PunBehaviour
{
    // this method is called when a bullet hits a player
    public virtual void Hit(GameObject go)
    {

    }

    // this method takes care of call Finish on the current instance and on other instances over network
    public void PreFinish()
    {
        Finish();
        photonView.RPC("Finish", PhotonTargets.Others);
    }

    // this method is called when a bullet has finished its effect or has reached the max lifetime
    [PunRPC]
    public virtual void Finish()
    {
        Destroy(gameObject);
    }

}