using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceController : MonoBehaviour
{
    [SerializeField] int golds;
    [SerializeField] TMP_Text goldTMP;

    void Start() {
        UpdateGoldText();
    }

    void Update() {
        
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
