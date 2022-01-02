
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MaxSpeed = 5;
    [SerializeField] private float BlinkPoint = 5;
    [SerializeField] private float MaxBlinkPoint = 5;
    [SerializeField] private float BlinkCD = 5;

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

        // Mouse(0) is a blink skill
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Blink(inputPosition);
        }
    }

    private void FixedUpdate()
    {
        myRigidBody.velocity = playerVelocity;
    }

    private void Blink(Vector2 position)
    {
        if (BlinkPoint > 0)
        {
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.position = position;
            playerVelocity = Vector2.zero;
            BlinkPoint--;
        }
    }
}
