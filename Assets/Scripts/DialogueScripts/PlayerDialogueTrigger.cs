using UnityEngine;

public class PlayerDialogueTrigger : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TryInteractWithNPC();
        }
    }

    private void TryInteractWithNPC()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 2f);
        if (hit.collider != null)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
        Debug.DrawRay(transform.position, Vector2.zero, Color.red, 2f);
    }
}
