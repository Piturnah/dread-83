using UnityEngine;

public class DiceScript : MonoBehaviour
{
    static Rigidbody rb;
    public static Vector3 diceVelocity;
    public Vector3 initialPosition;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        diceVelocity = rb.velocity;
    }

    public void TriggerDiceThrow() {
        rb = GetComponent<Rigidbody>();
        float dirX = Random.Range(-500, 500);
        float dirY = Random.Range(-500, 500);
        float dirZ = Random.Range(-500, 500);
        transform.position = initialPosition;
        transform.rotation = Random.rotation;
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * 500);
        rb.AddTorque(dirX, dirY, dirZ);
    }
}
