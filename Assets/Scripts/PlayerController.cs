using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 0.03f;
    private float gDirection = 1f; // Direction of gravity pull (1 for down, -1 for up)
    public float pullStrength = 5f; // How strong the gravity pull is
    


   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable gravity for the player
    }

    
    void Update()
    {
        // Detect mouse click to flip gravity
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Key Pressed - Flipping Gravity");
            gDirection *= -1; // Flip gravity direction
        }
    }

    void FixedUpdate()
    {
        // Move player forward
        rb.AddForce(Vector2.right * speed, ForceMode2D.Force);

        // Apply gravity pull in the current direction
        rb.AddForce(Vector2.down * gDirection * pullStrength, ForceMode2D.Force);
    }
}
