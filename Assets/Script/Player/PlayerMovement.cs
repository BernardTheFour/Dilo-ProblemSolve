
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MaxSpeed = 5;
    [SerializeField] private int TeleportPoint = 5;

    
    [SerializeField] AudioClip audioCoint;
    [SerializeField] AudioClip audioTelepoint;
    [SerializeField] AudioClip audioTeleport;
    [SerializeField] AudioClip audioEnemy;

    private Rigidbody2D myRigidBody;

    private Vector2 playerVelocity;

    private AudioSource myAudio;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAudio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.Instance.TeleportCharges(TeleportPoint);
    }

    private void Update()
    {
        // Keyboard Input
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        playerVelocity = new Vector2(x, y) * MaxSpeed;

        // Mouse(0) is a teleport skill
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Teleport(inputPosition);
        }
    }

    private void FixedUpdate()
    {
        myRigidBody.velocity = playerVelocity;
    }

    private void Teleport(Vector2 position)
    {
        if (ScoreManager.Instance.TeleportPoint > 0)
        {
            myAudio.PlayOneShot(audioTeleport);
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.position = position;
            playerVelocity = Vector2.zero;
            ScoreManager.Instance.TeleportCharges(-1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Coint":
                myAudio.PlayOneShot(audioCoint);
                break;
            case "Telepoint":
                myAudio.PlayOneShot(audioTelepoint);
                break;
            case "Enemy":
                myAudio.PlayOneShot(audioEnemy);
                break;
        }
    }
}
