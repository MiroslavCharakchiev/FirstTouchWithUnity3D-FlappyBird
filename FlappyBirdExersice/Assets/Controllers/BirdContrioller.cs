using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdContrioller : MonoBehaviour
{
    public float flapSpeed = 600f;
    public float forwardSpeed = 2.5f;
    public float maxFlapSpeed = 8f;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private bool isDeath;
    private bool didFlap;
    private bool gameStarted;
    private Vector2 originalPosition;
    private GameObject startButton;
    private int currentScore;

    public void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
        this.originalPosition = new Vector2(this.transform.position.x, this.transform.position.y);
        this.startButton = GameObject.Find("StartButton");
        this.rigidBody.gravityScale = 0;
        this.forwardSpeed = 0;

    }
    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isDeath)
            {
                if (!this.gameStarted)
                {
                    var renderer = startButton.GetComponent<SpriteRenderer>();
                    renderer.enabled = false;

                    this.rigidBody.gravityScale = 1;
                    this.forwardSpeed = 2.5f;
                }
                this.didFlap = true;
            }
            else
            {
                SceneManager.LoadScene("Play");
            }
        }
    }
    public void FixedUpdate()
    {
        var velocity = this.rigidBody.velocity;
        velocity.x = this.forwardSpeed;
        this.rigidBody.velocity = velocity;
        if (this.rigidBody.velocity.y > 0)
        {
            this.rigidBody.MoveRotation(30);
        }
        else
        {
            this.rigidBody.MoveRotation(0);
        }

        if (didFlap)
        {
            this.didFlap = false;

            this.rigidBody.AddForce(new Vector2(0, flapSpeed), ForceMode2D.Impulse);
            var updatedVelocity = this.rigidBody.velocity;
            if (updatedVelocity.y > maxFlapSpeed)
            {
                updatedVelocity.y = maxFlapSpeed;
                this.rigidBody.velocity = updatedVelocity;
            }

        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("PipeTouch"))
        {
            this.isDeath = true;
            this.animator.SetBool("dead", true);
            this.forwardSpeed = 0;

            var highScore = PlayerPrefs.GetInt("HighScore", 0);
            if (currentScore > highScore)
            {
                PlayerPrefs.SetInt("HighScore", this.currentScore);
            }

            var renderer = startButton.GetComponent<SpriteRenderer>();
            renderer.enabled = true;

            var startButtonX = Camera.main.transform.position.x;
            var startButtonY = Camera.main.transform.position.y;
            var startButtonPosition = this.startButton.transform.position;
            startButtonPosition.x = startButtonX;
            startButtonPosition.y = startButtonY;
            this.startButton.transform.position = startButtonPosition;

        }
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Pipe"))
        {
            currentScore++;
        }
       
    }
}
