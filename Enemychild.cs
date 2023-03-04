using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemychild : MonoBehaviour
{
    private Collider2D coll;
    private Rigidbody2D rb;
    public int speed;
    private Animator anim;
    float time;
    private bool faceRight = true;
     void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        time = time + Time.deltaTime;//敌人移动
        if (faceRight)
        {
            
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (time>2)//大于2秒转身
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                faceRight = false;
            }
        }
        else
        {
            
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (time>4)//大于4秒
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                faceRight = true;
                time = 0;
            }
        }
    }
     void Death()//死亡
    {
        anim.SetBool("dead",true);//动画
        Invoke("end", 0.2f);//延时0.2秒到end
    }
    void end()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "fire")
        {
            Death();
        }
    }
}

