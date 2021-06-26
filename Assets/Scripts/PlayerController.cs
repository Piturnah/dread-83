using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float maxHealth;
    float healthValue;

    public float speed = 10f;
    public float smoothMoveTime = .05f;
    public float turnSpeed = 15;

    float angle;
    float smoothInputMagnitude;
    float smoothMoveVelocity;

    float inputMagnitude;
    float prevInputMagnitude;

    private void Update() {
        PerformMovement();
    }

    void PerformMovement() {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        prevInputMagnitude = inputMagnitude;
        inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg * inputMagnitude;
        angle = (prevInputMagnitude == 0) ? targetAngle : Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);
        if (inputMagnitude != 0) {
            transform.eulerAngles = Vector3.up * angle;
        }

        transform.Translate(transform.forward * speed * smoothInputMagnitude * Time.deltaTime, Space.World);
    }

    void TakeDamage(float amount) {
        healthValue -= amount;
        GameObject.FindGameObjectWithTag("Healthbar").GetComponent<HealthBar>().SetHealthBarValue(healthValue / maxHealth);
    }
}
