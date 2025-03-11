using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public class Player : MonoBehaviour
{

    //NoahCorreia


    //health and gems
    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthBar;
    public TextMeshProUGUI GemCount;
    public int gemCount = 0;


    //animator setup
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    //Movement
    public float walkSpeed = 10f;

    //jump stuff
    public float jumpHeight = 8;
    public float fallMultiplier = 5f;
    public float lowJumpMultiplier = 2f;
    public bool isGrounded = false;
    public bool jump, fall, land;
    public LayerMask groundLayer;
    private bool canDoubleJump;
    public float jumpForce = 8;
    public float MaxJumpForce = 18;

    //start 
    private Vector3 StartPos;
   

    void Start()
    {
        //Initialize the Animator variable using GetComponent 
        //Can access Components on other objects as well
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        //setting player health to max health on start
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        StartPos = transform.position;
    }

    void Update()
    {
        
        //Input.GetAxis is a value from -1 to 0 to +1
        //Store the value in a float variable 
        float h = Input.GetAxis("Horizontal");

        //raycast is a single pixel line that checks for collisions 
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.10f, groundLayer);

        //Call the function "Movement" and send it the value of 'h'
        Movement(h);
        HandleAnimations(h);
        Jumping();
        
        PlayerDied();
    }


    void HandleAnimations(float hSpeed)
    {
        //if hspeed is less than 0 the character is moving left 
        //else if hspeed is greater than 0 the character is moving right
        //not listed, if hspeed is exactly 0
        if (hSpeed < 0)
        {
            sr.flipX = true;
        }
        else if (hSpeed > 0)
        {
            sr.flipX = false;
        }

        if (hSpeed != 0)
        {
            //specifically modify the animator paramaters/variables
            //SetBool has two paramaters ("which paramater",true or false
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        anim.SetBool("isJumping", !isGrounded);
    }

    void Movement(float hSpeed)
    {
        //Set the rigidbody2d's velocity to be a new vector2
        //Vectors are x, y, and z values. Since velocity is speed in
        //a direction, we have to assign values to both x and y.
        //Here, we are setting the velocity of x to be whatever the xVel 
        //calculation is, and y isn't changing.
        float xVel = hSpeed * walkSpeed;

        rb.velocity = new Vector2(xVel, rb.velocity.y);


    }

    void Jumping()
    {

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            canDoubleJump = true;
        }
        else if (canDoubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            canDoubleJump = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }
    public void PlayerDied()
    {
        if (currentHealth == 0)
        {
            SceneManager.LoadScene("GameOver");
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gem")
        {
            gemCount++;
            GemCount.text = gemCount.ToString();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "DeathBox")
        {
            transform.position = StartPos;
            TakeDamage(1);
        }

       

        if (collision.gameObject.tag == "Flag")
        {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (collision.gameObject.tag == "JumpPlant")
        {
            jumpForce = MaxJumpForce;
            canDoubleJump = false;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "JumpPlant")
        {
            jumpForce = 8;
        }
    }

   public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        anim.SetTrigger("Hit");
    }


}
