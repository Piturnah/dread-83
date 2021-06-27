﻿using System.Collections;
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

    float slamCooldown = 1f;
    float prevSlamTime;

    float inputMagnitude;
    float prevInputMagnitude;

    Rigidbody rb;
    float slamForce = 350f;

    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update() {
        if (rb.velocity.magnitude < 1f) {
            PerformMovement();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (Time.time > prevSlamTime + slamCooldown && other.tag == "EnemyAttack") {
            EnemyController enemyController = other.GetComponentInParent<EnemyController>();
            if (enemyController.slamDamage) {
                prevSlamTime = Time.time;
                rb.AddForce(slamForce * new Vector3(Mathf.Sin(Mathf.Deg2Rad * enemyController.angle), 0, Mathf.Cos(Mathf.Deg2Rad * enemyController.angle)));
            }
        }
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
