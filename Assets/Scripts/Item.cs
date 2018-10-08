using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Item : NetworkBehaviour {

    public bool equipable;
    public int slotTaken;

    //public int health, power, attack, defense; not needed here C: 

    void Use()
    {

    }

    [Command]
    public void CmdInteract(GameObject go)
    {
        Stats s = go.GetComponent<Stats>();
        s.RecoverDamage(10);
        Destroy(gameObject);
        Debug.Log("Destroying self.");
    }

    public void TransferAuth(NetworkIdentity from, NetworkIdentity to)
    {
        if (from.clientAuthorityOwner != null)
        {
            from.RemoveClientAuthority(from.clientAuthorityOwner);
        }

        if (!isServer)
        {
            //from.AssignClientAuthority(to.connectionToServer);
            //RpcSetParent(to);
            //from.AssignClientAuthority(to.connectionToClient);
        }
        else
        {
            from.AssignClientAuthority(to.connectionToClient);
            RpcSetParent(to);

        }

        //from.gameObject.transform.parent = to.gameObject.transform;
    }

    [ClientRpc]
    public void RpcSetParent(NetworkIdentity to)
    {
        transform.SetParent(to.gameObject.transform);
    }

    public void RemoveAuth(NetworkIdentity from)
    {
        if (from.clientAuthorityOwner != null)
        {
            from.RemoveClientAuthority(from.clientAuthorityOwner);
        }
    }


}