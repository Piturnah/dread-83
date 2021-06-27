using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static string dialogText;
    public static bool finishedDialog;

    IEnumerator Typewriter(string story, float btwCharDelay) {
        finishedDialog = false;
        dialogText = "";

        foreach (char c in story) {
            dialogText += c;
            if (dialogText.Length == story.Length) {
                finishedDialog = true;
            }
            yield return new WaitForSeconds(btwCharDelay);
        }
    }
}
