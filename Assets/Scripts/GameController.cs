using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    bool isGameStarted;
    GameObject canvas;
    BotController botController;
    UnitSpawner playerUnitSpawner;
    
    void Start() {
        canvas = GameObject.Find("Canvas");
        botController = GameObject.Find("Bot Units Spawner").GetComponent<BotController>();
        playerUnitSpawner = GameObject.Find("Player Units Spawner").GetComponent<UnitSpawner>();
        isGameStarted = false;
    }

    void Update() {
        
    }

    public void StartGame() {
        isGameStarted = true;
        canvas.transform.Find("Start Screen").gameObject.SetActive(false);
    }


    public void Win() {
        canvas.transform.Find("Win Screen").gameObject.SetActive(true);
    }

    public void Lose() {
        canvas.transform.Find("Lose Screen").gameObject.SetActive(true);
    }

    public void Reload() { 
        SceneManager.LoadScene(0);
    }

    public bool GetIsGameStarted() {
        return isGameStarted;
    }
}
