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
        time = time + Time.deltaTime;//�����ƶ�
        if (faceRight)
        {
            
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (time>2)//����2��ת��
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                faceRight = false;
            }
        }
        else
        {
            
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (time>4)//����4��
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                faceRight = true;
                time = 0;
            }
        }
    }
     void Death()//����
    {
        anim.SetBool("dead",true);//����
        Invoke("end", 0.2f);//��ʱ0.2�뵽end
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

