using UnityEngine;

public class playerCombat : MonoBehaviour
{
    //NoahCorreia


    public Animator anim;
    public Transform attackPoint;
    public float attackRange = .5f;
    public LayerMask enemyLayer;
    public int attackDamage = 5;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        //keep track of current time
        if (Time.time >= nextAttackTime)
        {
            //attacking
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                //if attackrate is 2 we add 1 / attackRate(2) to prevent spam
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }

    void Attack()
    {
        //play attack anim
        anim.SetTrigger("attacking");
        // detect enemies in range if attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        //damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}