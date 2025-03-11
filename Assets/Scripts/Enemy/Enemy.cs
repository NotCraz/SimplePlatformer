using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //NoahCorreia

    public Animator anim;

    public int maxHealth = 5;
    int currentHealth;
    public float Damage;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        anim.SetTrigger("Hurt");

        //play hurt anim
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        anim.SetBool("isDead", true);

        GetComponent<Collider2D>().enabled = false;

        this.enabled = false;

    }
}