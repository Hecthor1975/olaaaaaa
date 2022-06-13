using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthImg;
    bool isInmune;
    public float inmunityTime;
    // Blink material;
    SpriteRenderer sprite;
    public float knockbackForceX;
    public float knockbackForceY;
    Animator anim;
    public UnityEvent customActionZeroHP;
    


    Rigidbody2D rb;
    
    /*
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        material = GetComponent<Blink>();
        sprite = GetComponent<SpriteRenderer>(); 
        health = maxHealth;
        anim = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        healthImg.fillAmount = health / 10;

        if(health> maxHealth)
        {
            health = maxHealth;


        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
    
        if (collision.CompareTag("Enemy") && !isInmune)
        {
            
            health -= collision.GetComponent<Enemy>().damageToGive;           
            StartCoroutine(Inmunity());
            AudioManager.instance.PlayAudio(AudioManager.instance.hit);
            if (collision.transform.position.x >transform.position.x)
            {
                rb.AddForce(new Vector2(-knockbackForceX, knockbackForceY), ForceMode2D.Force);   
            }
            else
            {
                rb.AddForce(new Vector2(knockbackForceX, knockbackForceY), ForceMode2D.Force);

            }

            if (health <=0)
            {
                print("player dead");
                anim.SetBool("Death", true);
                anim.SetInteger("DeathType_int", 1);
                
                         
            }
            if (health <= 0)
            {
                customActionZeroHP.Invoke();
                



            }





        }

    }

    IEnumerator Inmunity()
    {

        isInmune = true;

        sprite.material = material.blink;
        yield return new WaitForSeconds(inmunityTime);
        sprite.material = material.original;
        isInmune = false;

    }
    */

}