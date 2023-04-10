using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceController : MonoBehaviour
{
    [SerializeField] int golds;
    [SerializeField] int increasingGoldsByTime = 5;
    [SerializeField] TMP_Text goldTMP;


    GameController gameController;
    bool isBot = false;
    bool hasStartedIncreasingGold = false;

    void Start() {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();

        if (GetComponent<BotController>() != null) {
            isBot = true;
        }

        UpdateGoldText();
        
    }

    void Update() {
        if (gameController.GetIsGameStarted() && !hasStartedIncreasingGold) {
            hasStartedIncreasingGold = true;
            StartCoroutine(IncreaseGoldsByTime());
        }
    }

    IEnumerator IncreaseGoldsByTime() {
        SetGolds(golds + increasingGoldsByTime);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(IncreaseGoldsByTime());
    }

    public int GetGolds() {
        return golds;
    }

    public void SetGolds(int golds) {
        this.golds = golds;
        UpdateGoldText();
    }

    public void UpdateGoldText() {
        if (!isBot) {
            goldTMP.text = golds.ToString();
        }
    }
}
