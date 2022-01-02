using UnityEngine;

public class CointBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore();
        }

        CointGenerator.Instance.Regenerate(gameObject);
    }
}
