using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float speed = 5f;
    public Rigidbody rigidbody;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        Vector3 direction = player.transform.position - transform.position;
    
        if (direction != Vector3.zero)
        {
            // Muda a rotação do inimigo com base na direção que ele esta indo
            Quaternion newRotation = Quaternion.LookRotation(direction);
            rigidbody.MoveRotation(newRotation);
        }

        if (distance > 2)
        {
            animator.SetBool("attack_empty", false);
            rigidbody.MovePosition(rigidbody.position + (direction.normalized * speed * Time.deltaTime));
        }
        else 
        {
            animator.SetBool("attack_empty", true);
        }
    }
}
