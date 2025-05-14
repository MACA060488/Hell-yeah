using UnityEngine;

public class SpriteFlipperByPosition : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 lastPosition;

   public float flipThreshold = 0.01f;
   
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        lastPosition = transform.position;
    }

    private void LateUpdate()
    {
        float deltaX = transform.position.x - lastPosition.x;

        if (Mathf.Abs(deltaX) > flipThreshold)
        {
            spriteRenderer.flipX = deltaX < 0;
        }

        lastPosition = transform.position;
    }
}
