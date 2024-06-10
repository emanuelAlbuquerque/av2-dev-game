using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController character;
    public GameObject panelDie;
    private Transform myCamera;
    public float speed;
    private Animator animator;
    public bool attack;
    public Text textoScore;
    public int score = 0;
    public bool canMove;
    public bool die;
    public bool damage;
    
    void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        textoScore.text = $"Score: {score.ToString()}";
        myCamera = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canMove = true;
        die = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!die){
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            Vector3 direction = new Vector3(horizontal, 0, vertical);

            direction = myCamera.TransformDirection(direction);
            direction.y = 0;

            if(canMove){
                character.Move(direction * Time.deltaTime * speed);
                character.Move(new Vector3(0, -9.81f, 0) * Time.deltaTime);        
            }
            
            // Trocando a animação ao se movimentar
            if (direction != Vector3.zero)
            {
                animator.SetBool("Mover", true);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10);
            }
            else
            {
                animator.SetBool("Mover", false);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                canMove = false;
                animator.SetBool("Mover", false);
                animator.SetBool("Atacar", true);
                attack = true;
            }
            else
            {
                
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                
                if (stateInfo.IsName("Male Attack 1"))
                {
                    
                    if (stateInfo.normalizedTime >= 1.0f)
                    {
                        animator.SetBool("Atacar", false);
                        attack = false;
                        canMove = true;
                    }
                }
            }

            panelDie.SetActive(false);
        }else{
            animator.SetBool("Morte", true);
            panelDie.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        textoScore.text = $"Score: {score.ToString()}";
    }

    void Pontua(){
        score++;
    }
}

// Instantiate - Cria novos objetos na tela, ele recebe o objeto que irá ser criado, a posição dela, e a rptação dela.