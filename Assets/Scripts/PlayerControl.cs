using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerJump))]
public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
    }

    public void EnableControl(bool enable)
    {
        if (enable)
        {
            playerMovement.enabled = true;
            playerJump.enabled = true;
        }
        else
        {
            rb.velocity = Vector2.zero; 
            playerMovement.enabled = false;
            playerJump.enabled = false;
        }
    }
}
