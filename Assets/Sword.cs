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
            Enemy enemyScript = objectColision.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.die = true;
                player.SendMessage("Pontua");
                StartCoroutine(DestroyEnemyAfterDelay(objectColision.gameObject, 2.0f)); // 1 segundo de delay
            }
        }
    }

    private IEnumerator DestroyEnemyAfterDelay(GameObject enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(enemy);
    }
}
