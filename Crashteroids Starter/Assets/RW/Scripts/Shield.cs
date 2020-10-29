using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private Ship ship;
    SpriteRenderer spriteRenderer;
    Collider collider;
    Rigidbody rigidbody;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.position = ship.transform.position;
        if (ship.shielded)
        {
            collider.isTrigger = true;
            spriteRenderer.enabled = true;
            rigidbody.isKinematic = false;
        }
        else
        {
            spriteRenderer.enabled = false;
            collider.isTrigger = false;
            rigidbody.isKinematic = true;
        }
    }
}
