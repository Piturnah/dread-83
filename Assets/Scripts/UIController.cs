using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static string dialogText;
    public static bool finishedDialog = true;

    public TextMeshProUGUI dialogBox;

    public string[] dialogs;
    int dialogIndex = 0;
    float charDelay = .04f;

    private void Start() {
        CameraController.cameraState = 1; // state 1 is for intro scene overlooking the cringe


    }

    private void Update() {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && UIController.finishedDialog) {
            StartCoroutine(Typewriter(dialogs[dialogIndex], charDelay));
        }
    }

    IEnumerator Typewriter(string story, float btwCharDelay) {
        dialogIndex += 1;
        finishedDialog = false;
        dialogBox.text = "";

        foreach (char c in story) {
            dialogBox.text += c;
            if (dialogBox.text.Length == story.Length) {
                finishedDialog = true;
            }
            yield return new WaitForSeconds(btwCharDelay);
        }
    }
}
