using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject player;
    private Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider objectColision)
    {
        if (objectColision.tag == "enemy" && playerScript.attack)
        {
            Destroy(objectColision.gameObject);
            player.SendMessage("Pontua");
        }
    }
}
