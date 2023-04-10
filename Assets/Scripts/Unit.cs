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
    [Header("Spawning properties")]
    [SerializeField] int cost;
    [SerializeField] float spawnTime;

    [Header("Fighting properties")]
    [SerializeField] float health = 0f;
    [SerializeField] AttackType attackType;
    [SerializeField] float hitRange = 10f;
    [SerializeField] float attackDamage = 0f;

    [Tooltip("Number of attacks perform in 1s")]
    [SerializeField] float attackSpeed = 2;
    
    [Tooltip("Ranged attack particle system")]
    [SerializeField] GameObject rangedAttack;
    
    float nextAttackTime = 0f;

    int allyLayerMask;
    int enemyLayerMask;

    void Start() {
        int allyLayer = (gameObject.tag == "Team 1") ? 6 : 7;
        allyLayerMask = 1 << allyLayer;

        int enemyLayer = (gameObject.tag == "Team 1") ? 7 : 6;
        enemyLayerMask = 1 << enemyLayer;

        if (attackType == AttackType.Ranged) {
            ParticleSystem particleSystem = rangedAttack.GetComponent<ParticleSystem>();
            ParticleSystem.CollisionModule collisionModule = particleSystem.collision;
            collisionModule.collidesWith = enemyLayerMask;
        }
    }

    void Update() {
    }

    private void OnParticleCollision(GameObject other) {
        Debug.Log(other.gameObject.name);
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
        RaycastHit hit;
        float stopRange = 1.5f;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, stopRange, allyLayerMask);

        if (hit.collider != null) {
            return true;
        }

        return false;
    }

    public GameObject FirstEnemyInRange() {        
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, hitRange, enemyLayerMask);

        if (hit.collider != null) {
            if (
                (gameObject.tag == "Team 1" && hit.collider.gameObject.tag == "Team 2") ||
                (gameObject.tag == "Team 2" && hit.collider.gameObject.tag == "Team 1")
            ) {
                DrawRay(hit.distance, Color.blue);
                
                return hit.collider.gameObject;
            } else {
                DrawRay(hit.distance, Color.white);
            }
        } else {
            DrawRay(hitRange, Color.white);
        }

        return null;
    }

    public void StartAttack(GameObject enemy) {
        switch (attackType) {
            case AttackType.Melee:
                if (Time.time >= nextAttackTime) {
                    Unit enemyUnit = enemy.GetComponent<Unit>();
                    enemyUnit.ReceiveDamage(attackDamage);

                    // Next attack cool down
                    nextAttackTime = Time.time + 1f / attackSpeed;
                }

                break;
            case AttackType.Ranged:
                ActivateRangedAttack();
                break;
            default:
                break;
        }
    }

    public void StopAttack() {
        DeactivateRangedAttack();
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

    public int GetCost() {
        return cost;
    }

    public float GetSpawnTime() {
        return spawnTime;
    }

    void DrawRay (float length, Color color) {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * length, color);
    }
}
