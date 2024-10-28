using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;//Gøre sådan så at jeg kan ændre spilleren hastighed i Unity 
    [SerializeField] private float jumpPower; 
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;


    private void Awake()
    {
        //Hjælper med at at henvende sig til rigidbody og animator fra objekt
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
       horizontalInput = Input.GetAxis("Horizontal");



        if (horizontalInput > 0)
            transform.localScale = Vector3.one;

        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1,1,1);      // Gøre sådan så at spillerens sprite kigger den retning man bevæger sig imod

        if (Input.GetKey(KeyCode.Space) && isGrounded()) 
            Jump();  // Spilleren hopper hvis man trykker Space//

        if (Input.GetKey(KeyCode.W) && isGrounded())
            Jump();   // Spiller hopper hvis man trykker W //

        if (Input.GetKey(KeyCode.UpArrow) && isGrounded())
            Jump();   // Spiller hopper hvis man trykker Up knap //

        // Sæt animator parametre
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Wall Jump logik

        if (wallJumpCooldown > 0.2f)
        {

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);  // Gør så spilleren bevæger sig højre eller vestre 

            if (onWall() && isGrounded())
            {
                body.gravityScale = 5;
                body.velocity = Vector2.zero;
            }

            else
                body.gravityScale = 7;


            if (Input.GetKey(KeyCode.Space))
                Jump();  // Spilleren hopper hvis man trykker Space//

            if (Input.GetKey(KeyCode.W))
                Jump();   // Spiller hopper hvis man trykker W //

            if (Input.GetKey(KeyCode.UpArrow))
                Jump();   // Spiller hopper hvis man trykker Up knap //
        }
        else
            wallJumpCooldown += Time.deltaTime; 
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 20, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 6, 20);

            wallJumpCooldown = 0;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);

        return raycastHit.collider != null;


    }
    

} 
