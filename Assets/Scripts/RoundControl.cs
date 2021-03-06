using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundControl : MonoBehaviour
{
    public static int NumberOfHostages = 15;
    public GameObject player;
    public GameObject UI;
    public GameObject UI2;
    Vector3 player_pos;
    int global_round_num;
    int max_round_num = -10;

    public GameObject dice;
    bool waitingForDiceResult = false;

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
        StartCoroutine(roundCountdown(global_round_num));
    }

    private void Update() {
        if (waitingForDiceResult && DiceCheckSurface.diceResult != 0) {
            waitingForDiceResult = false;
            NumberOfHostages -= DiceCheckSurface.diceResult;
            FindObjectOfType<HostageSpawner>().spawnHostage(DiceCheckSurface.diceResult);
            StartCoroutine(PanTime());
        }

        if (NumberOfHostages <= 0)
        {
            //You've won
            UI2.SetActive(true);
        }
    }

    IEnumerator NextRoundAfterHostageDeath(Animator anim) {
        yield return new WaitForSeconds(4);
        anim.enabled = false;
        CameraController.cameraState = 0;
        FindObjectOfType<PlayerController>().roundEnded = false;
        NewRound(global_round_num + 1);

    }

    IEnumerator PanTime() {
        yield return new WaitForSeconds(4);
        Animator camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        camAnim.SetTrigger("Back to idle");
        StartCoroutine(NextRoundAfterHostageDeath(camAnim));
    }

    public void InitRoundLoss() {
        FindObjectOfType<PlayerController>().roundEnded = true;
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");

        //Lost Round
        foreach (GameObject go in gos) {
            Destroy(go.gameObject);
        }

        Animator camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        CameraController.cameraState = 1;
        camAnim.enabled = true;
        camAnim.Play("IdleAbove");
        GameObject.FindGameObjectWithTag("DiceBro").GetComponent<Animator>().Play("Roll");
        StartCoroutine(wait2SecsNGo(camAnim));
    }

    public void WonRound()
    {
        //Won Round
        if (global_round_num + 1 > max_round_num)
        {
            //You've won
            UI.SetActive(true);
            Application.Quit();
        }
        else
        {
            Animator camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
            CameraController.cameraState = 1;
            camAnim.enabled = false;
            camAnim.SetTrigger("Back to idle");
            StartCoroutine(NextRoundAfterHostageDeath(camAnim));
        }
    }

    public void winCheck()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        if (gos.Length < 0)
        {

            InitRoundLoss();

        }
        else
        {
            WonRound();
        }
    }

    IEnumerator wait2SecsNGo(Animator anim) {
        yield return new WaitForSeconds(2);

        anim.Play("HostageIdle");

        dice.SetActive(true);
        DiceCheckSurface.diceResult = 0;
        dice.GetComponent<DiceScript>().TriggerDiceThrow();
        waitingForDiceResult = true;
    }

    IEnumerator roundCountdown(int currentRound)
    {
        yield return new WaitForSeconds(60);
        if (global_round_num == currentRound) {
            winCheck();
        }
    }
}
