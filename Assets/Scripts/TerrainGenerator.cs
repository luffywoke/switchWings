using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public float segmentWidth = 0.05f;   // Distance between each point
    public int totalSegments = 2000;      // How many segments to generate
    public float amplitude = 1f;         // How tall the hills are
    public float frequency = 0.5f;         // How close together the hills are
    public float groundDepth = -5f;      // How far down the bottom edge goes
    private float randomOffset;            // Random offset for Perlin noise to create unique terrain each time

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private PolygonCollider2D polyCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randomOffset = Random.Range(0f, 100f); // Randomize the offset for unique terrain each time
        
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        polyCollider = gameObject.AddComponent<PolygonCollider2D>();

        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
        meshRenderer.material.color = Color.green;

        GenerateTerrain();

    }

    void GenerateTerrain()
    {
        Vector3[] vertices = new Vector3[(totalSegments + 1) * 2];

        for (int i = 0; i <= totalSegments; i++)
        {
            float x = i * segmentWidth;
            // Using Perlin noise to create more natural hills, multiplied by the sine wave for variation
            float perlin = Mathf.PerlinNoise((x + randomOffset) * 0.1f, 0f);
            float y = Mathf.Sin(x * frequency) * amplitude * perlin;
            vertices[i * 2] = new Vector3(x, y, 0); // Top vertex
            vertices[i * 2 + 1] = new Vector3(x, groundDepth, 0); // Bottom vertex
        }

        int[] triangles = new int[totalSegments * 6];
        for (int i = 0; i < totalSegments; i++)
        {
            int topLeft = i * 2;
            int bottomLeft = i * 2 + 1;
            int topRight = (i + 1) * 2;
            int bottomRight = (i + 1) * 2 + 1;

            int triIndex = i * 6;

            // First Triangle
            triangles[triIndex] = topLeft;
            triangles[triIndex + 1] = topRight;
            triangles[triIndex + 2] = bottomRight;

            // Second Triangle
            triangles[triIndex + 3] = topLeft;
            triangles[triIndex + 4] = bottomRight;
            triangles[triIndex + 5] = bottomLeft;
        }

        // Create a new mesh and assign our vertices and triangles
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        meshFilter.mesh = mesh;

        // Only use the top surface points for the collider so the player slides on the hills
        Vector2[] colliderPoints = new Vector2[totalSegments + 1];
        for (int i = 0; i <= totalSegments; i++)
        {
            colliderPoints[i] = new Vector2(vertices[i * 2].x, vertices[i * 2].y);
        }
        polyCollider.SetPath(0, colliderPoints);
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
