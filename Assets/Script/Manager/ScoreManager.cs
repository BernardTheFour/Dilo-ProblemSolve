using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private Text UIScore = default;
    [SerializeField] private Text UITeleport = default;

    public int Score { private set; get; }
    public int TeleportPoint { private set; get; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScore(int value)
    {
        Score += value;
        UIScore.text = "Score: " + Score;
    }

    public void TeleportCharges(int value)
    {
        TeleportPoint += value;
        UITeleport.text = "Teleport: " + TeleportPoint;
    }
}

