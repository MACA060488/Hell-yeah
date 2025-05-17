using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed = 2f;
    public float followSpeed = 3f;

    private Transform currentTarget;
    private Transform player;
    private bool isFollowingPlayer = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTarget = pointB;
    }

    private void FixedUpdate()
    {
        if (pointA == null || pointB == null) return;

        if (isFollowingPlayer && player != null)
        {
            // Движение к игроку только по оси X
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPosition = Vector2.MoveTowards(rb.position, target, followSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }
        else
        {
            // Патрулирование между точками A и B (тоже только по X)
            Vector2 patrolTarget = new Vector2(currentTarget.position.x, rb.position.y);
            Vector2 newPosition = Vector2.MoveTowards(rb.position, patrolTarget, patrolSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);

            if (Vector2.Distance(rb.position, patrolTarget) < 0.05f)
            {
                currentTarget = currentTarget == pointA ? pointB : pointA;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowingPlayer = true;
            player = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowingPlayer = false;
            player = null;
        }
    }
}
