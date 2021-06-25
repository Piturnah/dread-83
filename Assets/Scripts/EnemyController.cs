﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float speed = 7f;
    Vector3 direction;

    Transform playerTransform;

    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        direction = (playerTransform.position - transform.position).normalized;
        Move();
    }

    void Move() {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}