using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Sprite[] buttonImages;

    GameObject canvas;
    UnitSpawner unitSpawner;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        unitSpawner = GameObject.Find("Player Units Spawner").GetComponent<UnitSpawner>();

        for (int i = 0; i < unitSpawner.GetUnitGameObjects.Length; ++i) {
            Vector3 position = new Vector3(66 + 98 * i, -150, 0);

            GameObject button = Instantiate(buttonPrefab, position, Quaternion.identity);
            button.transform.SetParent(canvas.transform, false);

            int index = i;

            button.GetComponent<Image>().sprite = buttonImages[i];

            button.GetComponent<Button>().onClick.AddListener(
                delegate {
                    unitSpawner.Spawn(index);
                }
            );
        }
    }

    void Update()
    {
        
    }
}
