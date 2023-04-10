using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    GameController gameController;

    [SerializeField] int health = 5;

    string enemyTag;
    
    void Start()
    {
        enemyTag = (gameObject.tag == "Team 1") ? "Team 2" : "Team 1";
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();

    }

    void Update()
    {
        if (health <= 0) {
            if (gameObject.tag == "Team 1") {
                gameController.Lose();
            } else {
                gameController.Win();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == enemyTag) {
            health--;
            Destroy(other.gameObject);
        }
    }
}
