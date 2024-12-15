using System.Collections;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public float moveDistance = 0.2f;
    public float moveSpeed = 2f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(MoveItem());
    }

    private IEnumerator MoveItem()
    {
        while (true)
        {
            for (float t = 0; t < moveDistance; t += Time.deltaTime * moveSpeed)
            {
                transform.position = originalPosition + new Vector3(0, t, 0);
                yield return null;
            }

            for (float t = moveDistance; t > 0; t -= Time.deltaTime * moveSpeed)
            {
                transform.position = originalPosition + new Vector3(0, t, 0);
                yield return null;
            }
        }
    }
}
