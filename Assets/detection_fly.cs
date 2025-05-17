using UnityEngine;

public class FlyingEnemyTrigger : MonoBehaviour
{
    public float followSpeed = 2f;
    public float desiredDistanceX = 2f;
    public float desiredDistanceY = 1.5f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Transform player = other.transform;
        Vector3 playerPos = player.position;
        Vector3 enemyPos = transform.position;
        Vector3 moveDirection = Vector3.zero;

        float diffX = playerPos.x - enemyPos.x;
        float diffY = playerPos.y - enemyPos.y;

        // Если дистанция меньше желаемой — отдаляемся (движение в противоположную сторону)
        if (Mathf.Abs(diffX) > desiredDistanceX)
            moveDirection.x = Mathf.Sign(diffX);
        else if (Mathf.Abs(diffX) < desiredDistanceX * 0.9f) // слишком близко
            moveDirection.x = -Mathf.Sign(diffX);

        if (Mathf.Abs(diffY) > desiredDistanceY)
            moveDirection.y = Mathf.Sign(diffY);
        else if (Mathf.Abs(diffY) < desiredDistanceY * 0.9f)
            moveDirection.y = -Mathf.Sign(diffY);

        transform.position += moveDirection.normalized * followSpeed * Time.deltaTime;
    }
}
