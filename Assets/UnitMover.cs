using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    Unit unit;

    void Start()
    {
        unit = GetComponent<Unit>();
    }

    void Update()
    {   
        GameObject enemy = unit.FirstEnemyInRange();
        
        if (enemy != null) {
            unit.StartAttack(enemy);
        } else {
            unit.StopAttack();
        }

        if (!unit.IsAllyAhead() && enemy == null) {
            Move();
        }

    }

    public void Move() {
        float x = Time.deltaTime * moveSpeed;
        
        transform.Translate(x, 0, 0);
    }
}
