using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    float speed = 10f;

    float dashAcc = 40f;
    float dashSpeed = 70f;
    bool dashing = false;
    float dashPeriod = 0.2f;
    float dashStart;

    Vector3 currentDirection;
    Vector3 dashVelocity;

    private void Update() {

        if (Input.GetAxisRaw("Fire1") == 1) {
            dashing = true;
            dashStart = Time.time;
        }

        if (!dashing) {
            currentDirection = DetectMovementInput();
            UpdatePosition(currentDirection, speed);
        } else {
            Dash();
        }
    }

    Vector3 DetectMovementInput() {

        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void UpdatePosition(Vector3 dir, float multiplier) {
        transform.Translate(dir.normalized * multiplier * Time.deltaTime);
    }

    void Dash() {
        speed = Mathf.Min(speed + dashAcc, dashSpeed);
        UpdatePosition(currentDirection, speed);

        if (Time.time >= dashStart + dashPeriod) {
            dashing = false;
            speed = 10;
        }
    }
}
