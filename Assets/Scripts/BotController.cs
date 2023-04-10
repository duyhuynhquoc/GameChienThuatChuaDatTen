using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    UnitSpawner unitSpawner;
    Queue<int> commandList;

    void Start() {
        unitSpawner = GetComponent<UnitSpawner>();
        commandList = new Queue<int>();
    }

    void Update() {
        if (commandList.Count == 0) {
            commandList.Enqueue(1);
            commandList.Enqueue(2);
            commandList.Enqueue(3);
            commandList.Enqueue(4);
        }

        int command = commandList.Peek();
        if (unitSpawner.Spawn(command - 1)) {
            commandList.Dequeue();
        }
    }
}
