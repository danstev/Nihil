using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Weapon : Item {

    public int health, power, attack, defense;
    public Animator anim;
    public float atkTime, attackTimer;
    public WeaponCollision c;

    [SyncVar(hook = "OnChangeState")]
    public string state = ""; //spin, idle, attack'1,2,3' etc


    public void OnStart()
    {
        state = "spin";
        anim.PlayInFixedTime("spin", -1, 0);
    }

    public void Update()
    {
        Timers();
    }

    void Timers()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                CmdStopAttack();
            }
        }
    }

    [Command]
    public new void CmdInteract(GameObject person)
    {
        //Place in inventory
        Inventory invent = person.GetComponent<Inventory>();
        if (invent.weapon == null)
        {
            invent.weapon = this.gameObject;
        }
        else
        {
            return;
        }

        NetworkIdentity conn = person.GetComponent<NetworkIdentity>();
        TransferAuth(gameObject.GetComponent<NetworkIdentity>(), conn);

        //Eliminiate capsule collider
        CapsuleCollider cap = GetComponent<CapsuleCollider>();
        cap.isTrigger = false;
        cap.radius = 0.0001f;
        cap.height = .0001f;

        //Move to correct places
        transform.parent = person.transform;
        transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        transform.localRotation = Quaternion.Euler(0,0,0);

        //Update stats from newly equipped item.
        Stats s = person.GetComponent<Stats>();
        s.UpdateStats(health, power, defense, attack);

        //state
        state = "idle";

        RpcInteract(person);
    }

    [ClientRpc]
    public void RpcInteract(GameObject person)
    {
        //Place in inventory
        Inventory invent = person.GetComponent<Inventory>();
        if (invent.weapon == null)
        {
            invent.weapon = this.gameObject;
        }
        else
        {
            return;
        }

        NetworkIdentity conn = person.GetComponent<NetworkIdentity>();
        TransferAuth(gameObject.GetComponent<NetworkIdentity>(), conn);

        //Eliminiate capsule collider
        CapsuleCollider cap = GetComponent<CapsuleCollider>();
        cap.isTrigger = false;
        cap.radius = 0.0001f;
        cap.height = .0001f;

        //Move to correct places
        transform.parent = person.transform;
        transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);

        //Update stats from newly equipped item.
        Stats s = person.GetComponent<Stats>();
        s.UpdateStats(health, power, defense, attack);

        //state
        state = "idle";
    }

    [Command]
    public void CmdUnequip(GameObject person)
    {
        NetworkIdentity conn = this.GetComponent<NetworkIdentity>();
        RemoveAuth(conn);

        //Remove in inventory
        Inventory invent = person.GetComponent<Inventory>();
        if (invent.weapon != null)
        {
            invent.weapon = null;
        }
        else
        {
            return;
        }

        //Remake capsule collider
        CapsuleCollider cap = GetComponent<CapsuleCollider>();
        cap.isTrigger = true;
        cap.radius = 1f;
        cap.height = 1f;

        //Move to correct places
        transform.parent = null;

        //Update stats from newly equipped item.
        Stats s = person.GetComponent<Stats>();
        s.UpdateStats(health, power, defense, attack);

        //state
        state = "spin";
        RpcUnequip(person);
    }

    [ClientRpc]
    public void RpcUnequip(GameObject person)
    {
        NetworkIdentity conn = this.GetComponent<NetworkIdentity>();
        RemoveAuth(conn);

        //Remove in inventory
        Inventory invent = person.GetComponent<Inventory>();
        if (invent.weapon != null)
        {
            invent.weapon = null;
        }
        else
        {
            return;
        }

        //Remake capsule collider
        CapsuleCollider cap = GetComponent<CapsuleCollider>();
        cap.isTrigger = true;
        cap.radius = 1f;
        cap.height = 1f;

        //Move to correct places
        transform.parent = null;

        //Update stats from newly equipped item.
        Stats s = person.GetComponent<Stats>();
        s.UpdateStats(health, power, defense, attack);

        //state
        state = "spin";
    }

    [Command]
    public void CmdStartAttack()
    {
        state = "attack";
        attackTimer = atkTime;
        RpcStartAttack();
    }

    [Command]
    public void CmdStopAttack()
    {
        state = "idle";
        RpcStopAttack();
    }

    [ClientRpc]
    public void RpcStartAttack()
    {
        state = "attack";
        attackTimer = atkTime;
    }

    [ClientRpc]
    public void RpcStopAttack()
    {
        state = "idle";
    }

    public void OnChangeState(string s)
    {
        state = s;
        switch (state)
        {
            case "spin":
                anim.PlayInFixedTime("spin", -1, 0);
                break;
            case "idle":
                anim.PlayInFixedTime("idle", -1, 0);
                break;
            case "attack":
                anim.PlayInFixedTime("attack1", -1, 0);
                break;
        }
    }

    
}

