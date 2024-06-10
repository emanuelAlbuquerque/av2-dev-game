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

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        die = false;
    }

    void FixedUpdate()
    {
        if(!die){
            float distance = Vector3.Distance(transform.position, player.transform.position);
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
                animator.SetBool("attack_empty", true);
                playerHealth.DamagePlayer(10);

                if(playerHealth.curHealth <= 0){
                    player.die = true;
                }
            }
        }else{
            animator.SetBool("dead", true);
        }
    }
}
