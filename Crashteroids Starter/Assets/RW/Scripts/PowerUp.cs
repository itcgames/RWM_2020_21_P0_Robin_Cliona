using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float maxY = -5;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < maxY)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Laser")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
