using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform;

    public float yOffset;
    public Vector2 cameraBoundsX;
    public Vector2 cameraBoundsY;

    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate() {
        transform.position = new Vector3(Mathf.Clamp(playerTransform.position.x, cameraBoundsX.x, cameraBoundsX.y), transform.position.y, Mathf.Clamp(playerTransform.position.z + yOffset, cameraBoundsY.x, cameraBoundsY.y));
    }
}
