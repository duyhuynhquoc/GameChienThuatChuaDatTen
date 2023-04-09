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
        if (unit.FirstEnemyInRange() == null) {
            unit.DeactivateRangedAttack();
        } else {
            unit.ActivateRangedAttack();
        }

        if (!unit.IsAllyAhead() && unit.FirstEnemyInRange() == null) {
            Move();
        }

    }

    public void Move() {
        float x = Time.deltaTime * moveSpeed;
        
        transform.Translate(x, 0, 0);
    }
}
