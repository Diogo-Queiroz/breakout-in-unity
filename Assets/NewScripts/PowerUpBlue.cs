using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBlue : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var transform1 = other.transform;
            var localScale = transform1.localScale;
            localScale = new Vector3(localScale.x + 0.2f, localScale.y, 1);
            transform1.localScale = localScale;
            Destroy(gameObject);
        }

        if (other.CompareTag("Respawn"))
        {
            Destroy(gameObject);
        }
    }
}
