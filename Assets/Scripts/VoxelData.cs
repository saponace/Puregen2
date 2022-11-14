using UnityEngine;

public static class VoxelData
{
    
    // Position of vertices in a block
    public static readonly Vector3[] BlockMeshVertices = new Vector3[]
    {
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 1.0f),
        new Vector3(0.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 1.0f, 1.0f),
        new Vector3(1.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 1.0f, 0.0f),
        new Vector3(1.0f, 1.0f, 1.0f)
    };

    /**
     * Triangles composition of vertices, in order
     * 3 vertices per triangle
     * 2 triangles per block face
     * 6 faces
     */
    public static readonly int[,] BlockMeshTriangles = new int[,]
    {
        {2, 0, 3, 1}, // Back Face
        {5, 4, 7, 6}, // Front Face
        {7, 6, 3, 2}, // Top Face
        {1, 0, 5, 4}, // Bottom Face
        {4, 0, 6, 2}, // Left Face
        {1, 5, 3, 7} // Right Face
    };

    public static readonly Vector2[] BlockMeshUvs = new Vector2[4]
    {
        new Vector2(0, 0),
        new Vector2(0, 1),
        new Vector2(1, 0),
        new Vector2(1, 1)
    };
    
    // Specifies where is the adjacent face (notably used for conditional rendering of blocks faces: do not render face against an opaque block)
    public static readonly Vector3Int[] FaceCheck = new Vector3Int[]
    {
        new Vector3Int(-1, 0, 0), // Back face
        new Vector3Int(1, 0, 0), // Front face
        new Vector3Int(0, 1, 0), // Top face
        new Vector3Int(0, -1, 0), // Bottom face
        new Vector3Int(0, 0, -1), // Left face
        new Vector3Int(0, 0, 1) // Right face
    };
}
