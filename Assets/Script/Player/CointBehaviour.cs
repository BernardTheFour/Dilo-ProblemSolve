using UnityEngine;

public class CointBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore();
        }

        gameObject.SetActive(false);
    }
}
