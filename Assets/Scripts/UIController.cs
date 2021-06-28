using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static string dialogText;
    public static bool finishedDialog = true;

    public TextMeshProUGUI dialogBox;
    public Animator cameraAnimator;

    public string[] dialogs;
    int dialogIndex = 0;
    float charDelay = .04f;

    Transform mainCameraTransform;
    Vector3 overViewPosition = new Vector3(-2.32f, 105.9f, -59.4f);
    Vector3 overViewRotation = new Vector3(33.168f, 0, 0);

    private void Start() {
        CameraController.cameraState = 0;
        mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

        SetStateZero();
    }

    void SetStateOne() {
        CameraController.cameraState = 1; // state 1 is for intro scene overlooking the cringe

        mainCameraTransform.position = overViewPosition;
        mainCameraTransform.eulerAngles = overViewRotation;

        dialogBox.transform.parent.gameObject.SetActive(true);
    }

    void SetStateZero() {
        dialogBox.transform.parent.gameObject.SetActive(false);
        cameraAnimator.SetTrigger("PanToPlayer");
        StartCoroutine(KillAnimator());
    }

    private void Update() {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && UIController.finishedDialog && CameraController.cameraState == 1 && dialogIndex <= dialogs.Length) {
            if (dialogIndex == dialogs.Length) {
                SetStateZero();
                return;
            }
            StartCoroutine(Typewriter(dialogs[dialogIndex], charDelay));
        }
    }

    IEnumerator KillAnimator() {
        yield return new WaitForSeconds(4);
        cameraAnimator.enabled = false;
        CameraController.cameraState = 0;
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
