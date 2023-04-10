using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceController : MonoBehaviour
{
    [SerializeField] int golds;
    [SerializeField] int increasingGoldsByTime = 5;
    [SerializeField] TMP_Text goldTMP;

    void Start() {
        UpdateGoldText();
        StartCoroutine(IncreaseGoldsByTime());
    }

    void Update() {
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
        goldTMP.text = golds.ToString();
    }
}
