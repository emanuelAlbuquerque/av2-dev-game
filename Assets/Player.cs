using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    public Rigidbody rigidbody;
    private Vector3 direction;
    private Animator animator;
    public bool attack;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float axis_x = Input.GetAxis("Horizontal");
        float axis_z = Input.GetAxis("Vertical");

        direction = new Vector3(axis_x, 0, axis_z);

        // Trocando a animação ao se movimentar
        if (direction != Vector3.zero)
        {
            animator.SetBool("mover", true);
        }
        else
        {
            animator.SetBool("mover", false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("attack", true);
            animator.SetBool("mover", false);
            attack = true;
        }
        else
        {
            // Obter informações do estado atual da animação
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            // Verificar se a animação "attack" está ativa
            if (stateInfo.IsName("Male Attack 1"))
            {
                // Verificar se a animação está no final (normalizedTime >= 1.0f significa que terminou)
                if (stateInfo.normalizedTime >= 1.0f)
                {
                    animator.SetBool("attack", false);
                    attack = false;
                }
            }
        }
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + (direction * Time.deltaTime * speed));

        if (direction != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(direction);
            rigidbody.MoveRotation(newRotation);
        }
    }
}

// Instantiate - Cria novos objetos na tela, ele recebe o objeto que irá ser criado, a posição dela, e a rptação dela.