using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AttackType 
{
  Melee,
  Ranged,
}

public class Unit : MonoBehaviour
{
    [SerializeField] float health = 0f;
    [SerializeField] float hitRange = 10f;
    [SerializeField] AttackType attackType;
    [SerializeField] float attackDamage = 0f;

    [Tooltip("Number of attacks perform in 1s")]
    [SerializeField] float attackSpeed = 2;
    
    [Tooltip("Ranged attack particle system")]
    [SerializeField] GameObject rangedAttack;
    
    float nextAttackTime = 0f;


    void Start()
    {
        
    }

    void Update()
    {
    }

    private void OnParticleCollision(GameObject other) {
        float damage = other.GetComponent<RangedAttack>().getDamage();
        ReceiveDamage(damage);
    }

    void ReceiveDamage(float damage) {
        health -= damage;

        if (health <= 0) {
            health = 0;
            Destroy(gameObject);
        }
    }

    public bool IsAllyAhead() {        
        int allyLayer = (gameObject.tag == "Team 1") ? 6 : 7;
        int layerMask = 1 << allyLayer;

        RaycastHit hit;
        float stopRange = 1.5f;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, stopRange, layerMask);

        if (hit.collider != null) {
            return true;
        }

        return false;
    }

    public GameObject FirstEnemyInRange() {        
        int enemyLayer = (gameObject.tag == "Team 1") ? 7 : 6;
        int layerMask = 1 << enemyLayer;

        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, hitRange, layerMask);

        if (hit.collider != null) {
            if (
                (gameObject.tag == "Team 1" && hit.collider.gameObject.tag == "Team 2") ||
                (gameObject.tag == "Team 2" && hit.collider.gameObject.tag == "Team 1")
            ) {
                DrawRay(hit.distance, Color.blue);

                if (Time.time >= nextAttackTime && attackType == AttackType.Melee) {
                    Attack(hit.collider.gameObject);
                    nextAttackTime = Time.time + 1f / attackSpeed;
                }
                
                return hit.collider.gameObject;
            } else {
                DrawRay(hit.distance, Color.white);
            }
        } else {
            DrawRay(hitRange, Color.white);
        }

        return null;
    }

    void Attack(GameObject enemy) {
        Unit enemyUnit = enemy.GetComponent<Unit>();
        enemyUnit.ReceiveDamage(attackDamage);
    }

    void DrawRay (float length, Color color) {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * length, color);
    }

    public void ActivateRangedAttack() {
        if (rangedAttack != null) {
            rangedAttack.SetActive(true);
        }
    }

    public void DeactivateRangedAttack() {
        if (rangedAttack != null) {
            rangedAttack.SetActive(false);
        }
    }
}
