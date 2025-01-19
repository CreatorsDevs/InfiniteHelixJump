using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpHeight;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RingPlatform"))
        {
            float jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
            rb.velocity = new (rb.velocity.x, jumpVelocity, rb.velocity.z);
        }
    }
}
