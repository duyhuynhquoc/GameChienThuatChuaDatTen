using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] unitGameObjects;
    
    string team;
    float nextSpawnTime = 0f;
    List<Unit> units;
    
    ResourceController resourceController;

    PlayerInputActions input;

    bool isBot;

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
        team = gameObject.tag;
        resourceController = GetComponent<ResourceController>();
        isBot = (GetComponent<BotController>() != null);

        units = new List<Unit>();

        foreach (GameObject unitGameObject in unitGameObjects) {
            units.Add(unitGameObject.GetComponent<Unit>());
        }
    }

    void Update() {      
        if (!isBot) {
            input.Player.Spawn1.performed += _ => Spawn(0);
            input.Player.Spawn2.performed += _ => Spawn(1);
            input.Player.Spawn3.performed += _ => Spawn(2);
            input.Player.Spawn4.performed += _ => Spawn(3);
        }
    }

    public void SpawnUnit(GameObject unit) {
        if (team == "Team 1") {
            Vector3 position = new Vector3(transform.position.x, 1, transform.position.z);
            Quaternion rotation = Quaternion.identity;

            GameObject spawnedUnit = Instantiate(unit, position, rotation);
            spawnedUnit.gameObject.tag = "Team 1";
            spawnedUnit.gameObject.layer = 6;
        } else if (team == "Team 2") {
            Vector3 position = new Vector3(transform.position.x, 1, transform.position.z);
            Quaternion rotation = Quaternion.identity;
            rotation.y += 180;

            GameObject spawnedUnit = Instantiate(unit, position, rotation);
            spawnedUnit.gameObject.tag = "Team 2";
            spawnedUnit.gameObject.layer = 7;
        }
    }

    public bool CanSpawn(int i) {
        if (Time.time < nextSpawnTime) return false;

        if (resourceController.GetGolds() < units[i].GetCost()) {
            return false;
        }

        return true;
    }

    public void Spawn(int i) {
        if (!CanSpawn(i)) return;

        SpawnUnit(unitGameObjects[i]);
        resourceController.SetGolds(resourceController.GetGolds() - units[i].GetCost());
        nextSpawnTime = Time.time + units[i].GetSpawnTime();
    }

    public GameObject[] GetUnitGameObjects => unitGameObjects;
}
