using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] int health = 5;

    string enemyTag;
    
    void Start()
    {
        enemyTag = (gameObject.tag == "Team 1") ? "Team 2" : "Team 1";
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == enemyTag) {
            health--;
            Destroy(other.gameObject);
        }
    }
}
