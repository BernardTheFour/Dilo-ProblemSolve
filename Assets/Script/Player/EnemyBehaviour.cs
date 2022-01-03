using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    [SerializeField] private Orientation orientasiEnemy;
    [SerializeField] private RangeNumber CooldownRange;
    [SerializeField] private RangeNumber SpeedRange;

    [Space(5)]
    [Header("Limit")]
    [SerializeField] private Transform LowLimit = default;
    [SerializeField] private Transform HighLimit = default;

    private Rigidbody2D myRb;
    private RangeNumber limit;
    private float cooldown, speed;
    private Vector2 newPosition;

    private void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (orientasiEnemy == Orientation.Horizontal)
        {
            limit = new RangeNumber(LowLimit.position.y, HighLimit.position.y);
        }
        else
        {
            limit = new RangeNumber(LowLimit.position.x, HighLimit.position.x);
        }
    }

    private void FixedUpdate()
    {
        cooldown -= Time.fixedDeltaTime;

        if (cooldown <= 0)
        {
            RandomizeMovement();
            newPosition = RandomizePosition(orientasiEnemy);
        }

        myRb.MovePosition(Vector2.MoveTowards(transform.position, newPosition, Time.fixedDeltaTime * speed));
    }

    private Vector2 RandomizePosition(Orientation orientation)
    {
        float number = Random.Range(limit.start, limit.end);



        if (orientation == Orientation.Horizontal)
        {
            return new Vector2(0, number);
        }
        else
        {
            return new Vector2(number, 0);
        }
    }

    private void RandomizeMovement()
    {
        cooldown = Random.Range(CooldownRange.start, CooldownRange.end);
        speed = Random.Range(SpeedRange.start, SpeedRange.end);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.Instance.ChangeScore(-10);
        }
    }
}
