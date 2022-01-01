using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MaxSpeed = 5;
    [SerializeField] private float BoostPower = 10;
    [SerializeField] private float BoostPoint = 5;
    [SerializeField] private float MaxBoostPoint = 5;
    [SerializeField] private float BoostCD = 5;

    private Rigidbody2D myRigidBody;
    private bool isBoosted = false;

    private Vector2 playerVelocity;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BoostRefresh", 0,BoostCD);
    }

    private void Update()
    {
        // Keyboard Input
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            playerVelocity = new Vector2(x, y) * MaxSpeed;
            isBoosted = false;
            myRigidBody.drag = 0;
        }

        // Mouse(0) is a blink skill
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Boost(inputPosition.normalized);
        }
    }

    private void FixedUpdate()
    {
        if (!isBoosted)
        {
            myRigidBody.velocity = playerVelocity;
        }
    }

    private void Boost(Vector2 direction)
    {
        if (BoostPoint > 0)
        {
            isBoosted = true;
            myRigidBody.AddForce(direction * BoostPower, ForceMode2D.Impulse);
            myRigidBody.drag = 2;
            BoostPoint--;
        }
    }

    void BoostRefresh()
    {
        if (BoostPoint <= MaxBoostPoint)
        {
            BoostPoint++;
        }
    }
}
