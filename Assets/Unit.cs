using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] float health = 0f;
    [SerializeField] float sightRange = 10f;

    void Start()
    {
        
    }

    void Update()
    {
    }

    void ReceiveDamage(float damage) {
        health -= damage;
    }

    public bool canMove() {        
        RaycastHit hit;
        int layerMask = 1 << 6;

        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, sightRange, layerMask);

        if (hit.collider != null) {
            Debug.Log(hit.transform.name);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.blue);

            // if (
            //     (gameObject.tag == "Team 1" && hit.collider.gameObject.tag == "Team 2") ||
            //     (gameObject.tag == "Team 2" && hit.collider.gameObject.tag == "Team 1")
            // ) {
                
            // }
            return false;
        } else {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * sightRange, Color.white);
        }

        return true;
    }
}
