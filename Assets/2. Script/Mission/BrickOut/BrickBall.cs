using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBall : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public LayerMask wallLayerMask;

    private void Update()
    {
        direction = direction.normalized;

        RaycastHit2D hit2d = Physics2D.Raycast(transform.position, direction, Time.deltaTime * speed, wallLayerMask);

        if (hit2d)
        {
            if (hit2d.collider.CompareTag("Brick"))
            {
                hit2d.collider.gameObject.SetActive(false);
            }
            direction = Vector2.Reflect(direction, hit2d.normal);
        }
        else
        {
            transform.position += (Vector3)direction * Time.deltaTime * speed;
        }
    }
}
