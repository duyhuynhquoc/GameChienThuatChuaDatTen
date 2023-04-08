using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSpawner : MonoBehaviour
{

    [SerializeField] GameObject unit1;
    [SerializeField] GameObject unit2;
    [SerializeField] GameObject unit3;

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

    }

    void Update() {
        input.Player.Spawn1.performed += Spawn1;
        input.Player.Spawn2.performed += Spawn2;
        input.Player.Spawn3.performed += Spawn3;
    }

    public void SpawnUnit(GameObject unit) {
        Vector3 position = new Vector3(transform.position.x + 10, 1, transform.position.z);
        Instantiate(unit, position, Quaternion.identity);
    }

    public void Spawn1(InputAction.CallbackContext context) {
        SpawnUnit(unit1);
    }

    public void Spawn2(InputAction.CallbackContext context) {
        SpawnUnit(unit2);
    }

    public void Spawn3(InputAction.CallbackContext context) {
        SpawnUnit(unit3);
    }
}
