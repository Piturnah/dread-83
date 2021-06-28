using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static int cameraState = 1;

    Transform playerTransform;

    public float yOffset;
    public Vector2 cameraBoundsX;
    public Vector2 cameraBoundsY;

    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate() {
        if (cameraState == 0) {
            transform.position = new Vector3(Mathf.Clamp(playerTransform.position.x, cameraBoundsX.x, cameraBoundsX.y), 11.6f, Mathf.Clamp(playerTransform.position.z + yOffset, cameraBoundsY.x, cameraBoundsY.y));
        }
    }
}
