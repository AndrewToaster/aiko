using UnityEngine;

public class AikoHurtbox : MonoBehaviour
{
    public Transform arena;
    public BoxCollider2D bbox;
    public float moveSpeed;

    public Vector2 aExt, bExt, mv, pos;

    private void Update()
    {
        RectTransform tf = (RectTransform)transform;

        aExt = RectTransformUtility.CalculateRelativeRectTransformBounds(arena).extents;
        bExt = bbox.size / 2;
        mv = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        pos = tf.anchoredPosition + moveSpeed * Time.deltaTime * mv;

        if (pos.x - bExt.x < -aExt.x)
        {
            pos.x = bExt.x - aExt.x;
        }
        if (pos.y - bExt.y < -aExt.y)
        {
            pos.y = bExt.y - aExt.y;
        }
        if (pos.x + bExt.x > aExt.x)
        {
            pos.x = aExt.x - bExt.x;
        }
        if (pos.y + bExt.y > aExt.y)
        {
            pos.y = aExt.y - bExt.y;
        }
        tf.anchoredPosition = pos;
    }
}
