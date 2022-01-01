using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MaxSpeed = 5;

    private Rigidbody2D myRigidBody;

    private Vector2 playerVelocity;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        // Keyboard Input
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            playerVelocity = new Vector2(x, y) * MaxSpeed;
        }
        else
        {
            playerVelocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        myRigidBody.velocity = playerVelocity;
    }
}
