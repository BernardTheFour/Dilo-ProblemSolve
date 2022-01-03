using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelepointBehaviour : MonoBehaviour
{   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.Instance.TeleportCharges(+1);
        }

        Generator.Instance.Regenerate(gameObject);
    }
}
