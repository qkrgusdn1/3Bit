using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkLine : MonoBehaviour
{
    public LayerMask startPointMask;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //���縶�콺�� Ŭ���� ��ǥ
            Collider2D col = Physics2D.OverlapPoint(new Vector2(0,0),startPointMask);

        }
    }
}
