using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class move : MonoBehaviour
{
   
    
   
    private Rigidbody2D rb;
        private Collider2D coll;
        private Animator anim;
        public float speed, jumpForce;
        public LayerMask ground;
        public Transform foot;
        private bool isJump, isGround;
        public float hp=100;
    public static bool faceRight = true;
    private float TotalHp;
    public Slider hpslider;
    public Slider hpslider2;
    private int jumpCount;
  
   
    private bool isHurt;

            // Start is called before the first frame update
            void Start()
    {
        TotalHp = hp;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            isJump = true;
        }
    }
    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(foot.position, 0.1f, ground);
        if(!isHurt)
        {
        Move();
        }
        Jump();
        Change();
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");//左右
        if (horizontal>0)
        {
            faceRight=true;
        }
        if (horizontal < 0)
        {
            faceRight =false;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if (horizontal != 0)
        {
            transform.localScale = new Vector3(horizontal, 1, 1);
            anim.SetBool("run", true);
        }
        else
            anim.SetBool("run", false);
    }
    void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
        }
        if (isJump && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            isJump = false;
        }
        else if (isJump && !isGround && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            isJump = false;
        }
    }
    void Change()
    {
        if (rb.velocity.y < 0 && !isGround)//不在地上并且要下落
        {
            anim.SetBool("fall", true);
            anim.SetBool("jump", false);
        }
        if (!isGround && rb.velocity.y > 0)
        {
            anim.SetBool("jump", true);
        }
       else if(isHurt)
        {
            anim.SetBool("hurt", true);
            if(Mathf.Abs(rb.velocity.x)<0.1f)
            {
                anim.SetBool("hurt", false);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("fall", false);
        }
    }
    void Restart()//换场
    {
        SceneManager.LoadScene(1);
    }
    void life()
    {
        hp = hp - 20;
        hpslider.value = (float)hp / TotalHp;
        Invoke("life2", 0.4f);

        if (hp<=0)
        {
           
            anim.SetBool("die", true);
            Invoke("Restart", 0.5f);
            
        }
    }
    void life2()
    {
        hpslider2.value = (float)hp / TotalHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="collect")
            {
            Destroy(collision.gameObject);
           
           
            if(hp<=80)
            hp = hp + 20;
            hpslider.value = (float)hp / TotalHp;
        }
        if(collision.tag == "deadline")
        {
            life();
          
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
           
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
             if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-8, rb.velocity.y);
                isHurt = true;
                life();
            }
            
             if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(8, rb.velocity.y);
                isHurt = true;
                life();
            }
        }
    }

}

