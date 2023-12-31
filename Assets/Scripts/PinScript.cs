using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScript : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    private Rigidbody rb;

    [SerializeField] bool touchedGround = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
    }

    public void restartPin()
    {
        touchedGround = false;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero; // Reset angular velocity
        gameObject.SetActive(true);
    }
    public bool isGrounded()
    {
        return touchedGround;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            touchedGround = true;
        }
    }
}
