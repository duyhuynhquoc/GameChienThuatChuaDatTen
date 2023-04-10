using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] GameObject unit1;
    [SerializeField] GameObject unit2;
    [SerializeField] GameObject unit3;
    
    ResourceController resourceController;
    int cost1;
    int cost2;
    int cost3;

    PlayerInputActions input;

    private void Awake() {
        input = new PlayerInputActions();
    }

    private void OnEnable() {
        input.Enable();
    }

    private void OnDisable() {
        input.Disable();
    }

    void Start() {
        resourceController = GetComponent<ResourceController>();
        cost1 = unit1.GetComponent<Unit>().GetCost();
        cost2 = unit2.GetComponent<Unit>().GetCost();
        cost3 = unit3.GetComponent<Unit>().GetCost();
    }

    void Update() {
        input.Player.Spawn1.performed += Spawn1;
        input.Player.Spawn2.performed += Spawn2;
        input.Player.Spawn3.performed += Spawn3;
    }

    public void SpawnUnit(GameObject unit) {
        // Player 1
        Vector3 position = new Vector3(transform.position.x + 5, 1, transform.position.z);
        GameObject spawnedUnit = Instantiate(unit, position, Quaternion.identity);
        spawnedUnit.gameObject.tag = "Team 1";
        spawnedUnit.gameObject.layer = 6;
    }

    public void Spawn1(InputAction.CallbackContext context) {
        int golds = resourceController.GetGolds();
        
        if (golds >= cost1) {
            SpawnUnit(unit1);
            resourceController.SetGolds(golds - cost1);
        }
    }

    public void Spawn2(InputAction.CallbackContext context) {
        int golds = resourceController.GetGolds();
        
        if (golds >= cost2) {
            SpawnUnit(unit2);
            resourceController.SetGolds(golds - cost2);
        }
    }

    public void Spawn3(InputAction.CallbackContext context) {
        int golds = resourceController.GetGolds();
        
        if (golds >= cost3) {
            SpawnUnit(unit3);
            resourceController.SetGolds(golds - cost3);
        }
    }
}
