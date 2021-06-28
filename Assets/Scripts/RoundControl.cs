using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundControl : MonoBehaviour
{

    public GameObject player;
    Vector3 player_pos;
    int global_round_num;

    private void Start()
    {
        player_pos = player.transform.position;
    }

    public void NewRound(int round_num = 1)
    {
        //Spawn In new players
        FindObjectOfType<EnemySpawn>().SpawnEnemies(round_num * 5);
        player.transform.position = player_pos;
        global_round_num = round_num;
    }

    public void winCheck()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        if (gos.Length > 0)
        {
            //Lost Round
        }
        else
        {
            //Won Round
            NewRound(global_round_num + 1);
        }
    }

    IEnumerator roundCountdown()
    {
        yield return new WaitForSeconds(90);
        winCheck();
    }
}
