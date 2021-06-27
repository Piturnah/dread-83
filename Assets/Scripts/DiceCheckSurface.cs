using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckSurface : MonoBehaviour
{
    Vector3 diceVelocity;
    public static int diceResult;

    private void FixedUpdate() {
        diceVelocity = DiceScript.diceVelocity;
    }

    private void OnTriggerStay(Collider other) {
        if (diceVelocity.magnitude == 0) {
            switch (other.gameObject.name) {
                case "Side1":
                    diceResult = 6;
                    break;
                case "Side2":
                    diceResult = 5;
                    break;
                case "Side3":
                    diceResult = 4;
                    break;
                case "Side4":
                    diceResult = 3;
                    break;
                case "Side5":
                    diceResult = 2;
                    break;
                case "Side6":
                    diceResult = 1;
                    break;
            }
        }
    }
}
