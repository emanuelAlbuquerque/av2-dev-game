using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController character;
    private Transform myCamera;
    public float speed = 7.5f;
    private Animator animator;
    public bool attack;
    public Text textoScore;
    public int score = 0;
    
    void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        textoScore.text = $"Score: {score.ToString()}";
        myCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 direction = new Vector3(horizontal, 0, vertical);

        direction = myCamera.TransformDirection(direction);
        direction.y = 0;

        character.Move(direction * Time.deltaTime * speed);
        character.Move(new Vector3(0, -9.81f, 0) * Time.deltaTime);
        
        // Trocando a animação ao se movimentar
        if (direction != Vector3.zero)
        {
            animator.SetBool("mover", true);
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * 10);
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

        textoScore.text = $"Score: {score.ToString()}";
    }

    void Pontua(){
        score++;
    }
}

// Instantiate - Cria novos objetos na tela, ele recebe o objeto que irá ser criado, a posição dela, e a rptação dela.