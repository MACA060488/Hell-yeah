using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Transform target;
    private bool isFollowing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isFollowing && target != null)
        {
            float direction = target.position.x > transform.position.x ? 1f : -1f;
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Начинаем следовать за игроком
        if (other.CompareTag("Player"))
        {
            isFollowing = true;
            target = other.transform;
        }

        // Прекращаем следование, если зашли в зону DeadEnd
        if (other.CompareTag("DeadEnd"))
        {
            isFollowing = false;
            target = null;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Прекращаем следование, если игрок ушёл
        if (other.CompareTag("Player"))
        {
            isFollowing = false;
            target = null;
        }
    }
}