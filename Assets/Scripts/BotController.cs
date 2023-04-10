using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField] bool isActive = true;
    UnitSpawner unitSpawner;
    Queue<int> commandList;
    GameController gameController;


    void Start() {
        unitSpawner = GetComponent<UnitSpawner>();
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        commandList = new Queue<int>();
    }

    void Update() {
        if (commandList.Count == 0) {
            commandList.Enqueue(1);
            commandList.Enqueue(2);
            commandList.Enqueue(3);
            commandList.Enqueue(4);
        }

        if (gameController.GetIsGameStarted() && isActive) {
            int command = commandList.Peek();
            if (unitSpawner.CanSpawn(command - 1)) {
                unitSpawner.Spawn(command - 1);
                commandList.Dequeue();
            }
        }
    }
}
