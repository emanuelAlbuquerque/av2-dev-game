using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public Health playerHealth;
    public float speed = 5f;
    public Rigidbody rigidbody;
    public bool die;
    private bool isAttacking = false;
    public Animator animator;
    public float distance;
    public float timerToAttack = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        die = false;
    }

    void FixedUpdate()
    {
        if(!die){
            distance = Vector3.Distance(transform.position, player.transform.position);
            Vector3 direction = player.transform.position - transform.position;
        
            if (direction != Vector3.zero)
            {
                // Muda a rotação do inimigo com base na direção que ele esta indo
                Quaternion newRotation = Quaternion.LookRotation(direction);
                rigidbody.MoveRotation(newRotation);
            }

            if (distance > 1)
            {
                animator.SetBool("attack_empty", false);
                rigidbody.MovePosition(rigidbody.position + (direction.normalized * speed * Time.deltaTime));
            }
            else 
            {
                if(playerHealth.curHealth <= 0){
                    player.die = true;
                }

                if (!isAttacking)
                {
                    StartCoroutine(AttackPlayer(timerToAttack));
                }
            }
        }else{
            animator.SetBool("dead", true);
        }
    }

    public IEnumerator AttackPlayer(float timer)
    {
        isAttacking = true;
        animator.SetBool("attack_empty", true);
        
        yield return new WaitForSeconds(timer);

        if (distance < 1)
        {
            playerHealth.DamagePlayer(10);
        }

        isAttacking = false;
    }
}
