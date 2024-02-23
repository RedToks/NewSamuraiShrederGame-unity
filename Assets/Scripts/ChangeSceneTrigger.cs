using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    [SerializeField] private string newEntranceScene;
    private HouseEntrance houseEntrance;

    private void Start()
    {
        houseEntrance = FindObjectOfType<HouseEntrance>();
    }

    public void ChangeScene()
    {
        houseEntrance.sceneToLoad = newEntranceScene;
    }
}
