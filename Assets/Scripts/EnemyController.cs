﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float speed = 5f;
    float turnSpeed = 13f;

    float angle;
    Vector3 targetDirection;

    Transform playerTransform;
    Animator animator;
    bool slamming = false;
    float startSlamTime;
    float slamDuration = 1;

    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update() {
        Move();
    }

    void Move() {
        if (!slamming) {
            targetDirection = (playerTransform.position - transform.position).normalized;
            targetDirection = new Vector3(targetDirection.x, 0, targetDirection.z);

            float targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
            angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed);
            transform.eulerAngles = Vector3.up * angle;

            if ((playerTransform.position - transform.position).magnitude > 2f) {
                transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
            }
            else if (slamming == false) {
                slamming = true;
                startSlamTime = Time.time;
                animator.Play("Slam");
                Debug.Log("Slam time");
            }
        } else { // Slamming
            if (startSlamTime + slamDuration <= Time.time) {
                slamming = false;
            }
        }
    }
}
