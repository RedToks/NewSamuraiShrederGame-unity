using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private void Awake()
    {
        // Сделать этот объект постоянным при загрузке новых сцен
        DontDestroyOnLoad(gameObject);
    }
}

