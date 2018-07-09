using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    public PlayerStats[] ps = new PlayerStats[8]; //Amount of players here;

    public void AddExp(int exp)
    {
        foreach(PlayerStats p in ps)
        {
            if (p == null)
            {

            }
            else
            {
                p.AddExp(exp);
            }
        }
    }

    void OnStart()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int count = 0;
        foreach(GameObject player in players)
        {
            ps[count] = player.GetComponent<PlayerStats>();
            count++;
        }
    }

    public void AddPlayer(GameObject newPlayer)
    {
        PlayerStats p = newPlayer.GetComponent<PlayerStats>();
        for (int i = 0; i < 8; i++)
        {
            if(ps[i] == null)
            {
                ps[i] = p;
                break;
            }
        }
    }


}
