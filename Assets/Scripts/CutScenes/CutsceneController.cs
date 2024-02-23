using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private float deleteTimer;
    [SerializeField] private GameObject deathBrother;

    private void Start()
    {
        Player.Instance.DisablePlayerControl();
        Invoke("EndCutscene", deleteTimer);
    }

    private void EndCutscene()
    {
        deathBrother.SetActive(true);
        Destroy(gameObject);
    }
}
