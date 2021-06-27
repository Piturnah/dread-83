using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float speed = 5f;
    float separationSpeed = 5f;
    float turnSpeed = 13f;

    public float angle;
    Vector3 targetDirection;

    Transform playerTransform;
    Animator animator;
    public bool slamming = false;
    public bool slamDamage = false;
    float startSlamTime;
    float slamDuration = 1f;

    float separateRadius = 2f;

    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update() {
        Move();
    }

    void Move() {
        if (!slamming) {
            targetDirection = (playerTransform.position - transform.position);
            targetDirection = new Vector3(targetDirection.x, 0, targetDirection.z).normalized;

            Collider[] hits = Physics.OverlapSphere(transform.position, separateRadius);
            Vector3 congregateDirection = new Vector3(0, 0, 0);
            int avoidInRange = 0;
            foreach(Collider hit in hits) {
                if (hit.GetComponent<EnemyController>() && hit.transform != transform) {
                    Vector3 displacement = (hit.transform.position - transform.position);
                    Vector3 enemyDir = new Vector3(displacement.x, 0, displacement.y).normalized;
                    congregateDirection += enemyDir;
                    avoidInRange += 1;
                }
            }
            congregateDirection /= avoidInRange;
            congregateDirection = congregateDirection.normalized;

            targetDirection = (targetDirection - congregateDirection).normalized;

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
            }
        } else { // Slamming
            if (startSlamTime + slamDuration <= Time.time) {
                slamming = false;
                slamDamage = false;
            } else if (startSlamTime + 0.2f <= Time.time) {
                slamDamage = true;
            }
        }
    }
}
