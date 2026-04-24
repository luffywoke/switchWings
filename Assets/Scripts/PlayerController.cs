using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    private float gDirection = 0f; // Direction of gravity pull (1 for down, -1 for up)
    public float pullStrength = 5f; // How strong the gravity pull is
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable gravity for the player
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
