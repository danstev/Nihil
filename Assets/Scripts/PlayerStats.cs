using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerStats : Stats
{
    public int experience;
    public int expToGo;

    //[SyncVar(hook = "AddExp")]
    public int level;
	
	void Start () {
        imp = Vector3.zero;

        if (isLocalPlayer)
        {
            SetExpToGo();
            UpdateStatsUI();
            GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
            GameManager game = gm.GetComponent<GameManager>();
            game.AddPlayer(gameObject);
            //hover.enabled = false;
        }
        else
        {
            //hover.enabled = true;
        }
    }
	
	
	void Update () {
        if (imp.magnitude > 0.2f)
        {
            player.Move(imp * Time.deltaTime);
        }
        else
        {
            imp = Vector3.zero;
        }
        imp = Vector3.Lerp(imp, Vector3.zero, 5 * Time.deltaTime);

        if (isLocalPlayer)
        {
            PowerLevel();
        }
    }

    void SetExpToGo()
    {
        int expNeeded = (int)Mathf.Pow(level + 1, 2);
        expToGo = expNeeded - experience;
    }

    public void AddExp(int toAdd)
    {
        experience += toAdd;
        SetExpToGo();
        CheckLevel();
    }

    void CheckLevel()
    {
        if(experience > (int)Mathf.Pow(level + 1, 2))
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        maxHealthBase += 10;
        maxPowerBase += 5;
        health = maxHealthBase;
        power = maxPowerBase;
        UpdateStats();
        UpdateStatsUI();
        SetExpToGo();
    }

    private void OnConnectedToServer()
    {
        
    }
}
