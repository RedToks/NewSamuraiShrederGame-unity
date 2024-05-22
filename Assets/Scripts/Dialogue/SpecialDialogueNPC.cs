using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class SpecialNPC : BaseNPC, IInteractable
{
    [SerializeField] private string newSceneToTeleport;
    [SerializeField] private ScreenFade screenFader;

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
        screenFader.Fade();
        FindObjectOfType<PlayerTeleport>()?.ChangeTargetScene(newSceneToTeleport);
    }
}
