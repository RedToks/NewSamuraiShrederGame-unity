using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpecialNPC : BaseNPC
{
    [SerializeField] private string newSceneToTeleport;
    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        OnLastDialogueFinished += PerformLastDialogueAction;
    }

    private void OnDisable()
    {
        OnLastDialogueFinished -= PerformLastDialogueAction;
    }

    private void PerformLastDialogueAction()
    {
        FindObjectOfType<PlayerTeleport>()?.ChangeTargetScene(newSceneToTeleport);
    }
}
