using System.Collections;
using System.Drawing;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpHeight;
    [SerializeField] private GameObject splashPrefab;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("RingPlatform"))
        {
            AudioManager.Instance.Play("Bounce");
            float jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
            rb.velocity = new (rb.velocity.x, jumpVelocity, rb.velocity.z);

            if(collision.contactCount > 0)
            {
                Vector3 collisionPoint = collision.GetContact(0).point;
                SpawnSplash(collisionPoint, collision.gameObject);
            }
        }
        if(collision.gameObject.CompareTag("DeadZone"))
        {
            AudioManager.Instance.Play("GameOver");
            GameManager.Instance.GameOver();
        }
    }

    private void SpawnSplash(Vector3 position, GameObject platform)
    {
        if (splashPrefab != null)
        {
            GameObject splash = Instantiate(splashPrefab, position, Quaternion.identity);
            splash.transform.SetParent(platform.transform);
            if (splash.transform.localPosition.z < 0.0015f)
                splash.transform.localPosition += new Vector3(0, 0, 0.002f);
            else
                splash.transform.localPosition += new Vector3(0, 0, 0.0012f);
            splash.transform.localRotation = Quaternion.Euler(180,0,0);
            splash.transform.localScale = new (0.001f, 0.001f, 0.001f);
            StartCoroutine(DisappearSplash(splash));
        }
    }
    private IEnumerator DisappearSplash(GameObject splash)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(splash);
    }
}
