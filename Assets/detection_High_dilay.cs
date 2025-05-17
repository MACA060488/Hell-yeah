using UnityEngine;

public class detection_High : MonoBehaviour
{
    public float jumpForce = 5f;
    public float jumpHeightTolerance = 0.5f;
    public float jumpDelay = 0.5f; // Настраиваемая задержка перед прыжком
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool canJump = true;
    private bool isPreparingJump = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float deltaY = other.transform.position.y - transform.position.y;

            if (deltaY > jumpHeightTolerance && canJump && !isPreparingJump)
            {
                StartCoroutine(DelayedJump(other.transform));
            }
        }
    }

    private System.Collections.IEnumerator DelayedJump(Transform playerTransform)
    {
        isPreparingJump = true;
        yield return new WaitForSeconds(jumpDelay);

        float deltaY = playerTransform.position.y - transform.position.y;

        if (deltaY > jumpHeightTolerance && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
        }

        isPreparingJump = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            canJump = true;
        }
    }
}
