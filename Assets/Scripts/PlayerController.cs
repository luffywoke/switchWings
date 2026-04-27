using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    private float gDirection = 1f; // Direction of gravity pull (1 for down, -1 for up)
    public float pullStrength = 5f; // How strong the gravity pull is
    


   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable gravity for the player
    }

    
    void Update()
    {
        // Continuously move player forward
        transform.position += Vector3.right * speed * Time.deltaTime;

        // Detect mouse click to flip gravity
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse Clicked - Flipping Gravity");
            gDirection *= -1; // Flip gravity direction
        }
    }

    void FixedUpdate()
    {
        // Apply gravity pull in the current direction
        rb.AddForce(Vector2.down * gDirection * pullStrength, ForceMode2D.Force);
    }
}
