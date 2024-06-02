using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController character;
    public float speed = 7.5f;
    public Rigidbody rigidbody;
    private Vector3 direction;
    private Animator animator;
    public Transform playerCameraParent;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    Vector2 rotation = Vector2.zero;
    public bool attack;
    [HideInInspector]
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rotation.y = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        character.Move(direction * Time.deltaTime);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
        direction = (forward * curSpeedX) + (right * curSpeedY);

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

        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
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