﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float maxHealth;
    float healthValue;

    public float speed = 10f;
    public float smoothMoveTime = .05f;
    public float turnSpeed = 15;
    public GameObject dashObject;
    public GameObject DashEffectEmpty;

    float angle;
    float smoothInputMagnitude;
    float smoothMoveVelocity;

    float slamCooldown = 1f;
    float prevSlamTime;

    float dashDelay = .5f;
    float dashCooldown = 0f;
    float dashDistance = 10f;
    float startDashTime;
    bool dashing = false;

    float dashEffectTime = .1f;
    float dashEffectTimeRemaining;
    GameObject dashEffect;
    
    float inputMagnitude;
    float prevInputMagnitude;

    Rigidbody rb;
    float slamForce = 350f;

    Animator[] playerAnimator;

    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();

        playerAnimator = GetComponentsInChildren<Animator>();
    }

    private void Update() {
        if (rb.velocity.magnitude < 1f) {
            if (!dashing) {
                PerformMovement();
                if (dashEffectTimeRemaining > 0)
                {
                    dashEffectTimeRemaining -= Time.deltaTime;
                }
                else
                {
                    Destroy(dashEffect);
                }
            }
            else {
                if (Time.time >= startDashTime + dashDelay) {
                    transform.Translate(transform.forward * dashDistance, Space.World);
                    dashEffectTimeRemaining = dashEffectTime;
                    dashEffect = Instantiate(dashObject, DashEffectEmpty.transform);
                    dashEffect.transform.parent = null;
                    dashing = false;
                }
            }
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
        Vector3 rawInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        foreach (Animator anim in playerAnimator) {
            anim.SetBool("MovementInput", rawInput.magnitude > 0);
        }

        Vector3 inputDirection = rawInput.normalized;
        prevInputMagnitude = inputMagnitude;
        inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg * inputMagnitude;
        angle = (prevInputMagnitude == 0) ? targetAngle : Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);
        if (inputMagnitude != 0) {
            transform.eulerAngles = Vector3.up * angle;
        }

        transform.Translate(transform.forward * speed * smoothInputMagnitude * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.E) && Time.time >= startDashTime + dashCooldown + dashDelay) {
            dashing = true;
            startDashTime = Time.time;
        }
    }

    void TakeDamage(float amount) {
        healthValue -= amount;
        GameObject.FindGameObjectWithTag("Healthbar").GetComponent<HealthBar>().SetHealthBarValue(healthValue / maxHealth);
    }
}
