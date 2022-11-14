using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Chunk : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    
    private static readonly int ChunkWidth = 10;
    private static readonly int ChunkLength = 10;
    private static readonly int ChunkHeight = 15;
    
    private readonly List<Vector3> _vertices = new List<Vector3>();
    private readonly List<int> _triangles = new List<int>();
    private readonly List<Color> _colors = new List<Color>();
    private readonly List<Vector2> _uvs = new List<Vector2>();

    readonly BlockType[,,] _worldContent;
    
    private World _world;

    
    public Chunk()
    {
        _worldContent = CreateWorld();
    }


    private void Start()
    {
        _world = GameObject.Find("World").GetComponent<World>();
        CreateMeshData();
        RenderMesh();
    }
    
    private BlockType[,,] CreateWorld()
    {
        BlockType[,,] world = new BlockType[ChunkLength, ChunkHeight, ChunkWidth];
        
        for (int x = 0; x < ChunkLength; x++)
        {
            for (int z = 0; z < ChunkWidth; z++)
            {
                var surfaceHeight = new Random().Next(ChunkHeight);
                for (int y = 0; y < ChunkHeight; y++)
                {
                    if(y <= surfaceHeight)
                    {
                        world[x, y, z] = BlockType.Dirt;
                    } else
                    {
                        world[x, y, z] = BlockType.Air;
                    }
                }
            }
        }
        return world;
    }

    private bool IsBlockSolid(Vector3Int position)
    {
        try
        {
            return _world.blockTypes[(int)GetBlock(position)].isSolid;
        }
        catch (Exception)
        {
            // Outside of world boundaries
            return false;
        }
    }

    private BlockType GetBlock(Vector3Int position)
    {
        return _worldContent[position.x, position.y, position.z];
    }

    private void CreateBlockMeshData(Vector3Int position)
    {
        for (var face = 0; face < 6; face++)
        {
            if (ShouldDrawBlockFace(position, face))
            {
                var vertexIndex = _vertices.Count;

                // Declares the four corners of the face
                _vertices.Add(position + VoxelData.BlockMeshVertices[VoxelData.BlockMeshTriangles[face, 0]]);
                _vertices.Add(position + VoxelData.BlockMeshVertices[VoxelData.BlockMeshTriangles[face, 1]]);
                _vertices.Add(position + VoxelData.BlockMeshVertices[VoxelData.BlockMeshTriangles[face, 2]]);
                _vertices.Add(position + VoxelData.BlockMeshVertices[VoxelData.BlockMeshTriangles[face, 3]]);

                // Declares the two triangles of the face
                // Triangle 1
                _triangles.Add(vertexIndex);
                _triangles.Add(vertexIndex + 1);
                _triangles.Add(vertexIndex + 2);
                // Triangle 2
                _triangles.Add(vertexIndex + 2);
                _triangles.Add(vertexIndex + 1);
                _triangles.Add(vertexIndex + 3);

                _uvs.Add(VoxelData.BlockMeshUvs[0]);
                _uvs.Add(VoxelData.BlockMeshUvs[1]);
                _uvs.Add(VoxelData.BlockMeshUvs[2]);
                _uvs.Add(VoxelData.BlockMeshUvs[3]);
            }

            // Declare vertices colors
            // var blockColor = GetBlockColor(GetBlock(position));
            // _colors.Add(blockColor);
            // _colors.Add(blockColor);
            // _colors.Add(blockColor);
            // _colors.Add(blockColor);
        }
    }

    private Color GetBlockColor(BlockType blockType)
    {
        switch (blockType)
        {
            case BlockType.Air:
                return new Color(1, 1, 1, 0);
            case BlockType.Stone:
                return Color.grey;
            case BlockType.Dirt:
                return Color.red;
            default:
                return new Color(0, 0, 0, 0);
        }
    }

    private bool ShouldDrawBlockFace(Vector3Int position, int face)
    {
        return IsBlockSolid(position) && !IsBlockSolid(VoxelData.FaceCheck[face] + position);
    }

    private void CreateMeshData()
    {
        for (int x = 0; x < ChunkLength; x++)
        {
            for (int z = 0; z < ChunkWidth; z++)
            {
                for (int y = 0; y < ChunkHeight; y++)
                {
                    CreateBlockMeshData(new Vector3Int
                    {
                        x = x, y = y, z = z
                    });
                }
            }
        }
    }

    private void RenderMesh()
    {
        var mesh = new Mesh
        {
            vertices = _vertices.ToArray(),
            triangles = _triangles.ToArray(),
            uv = _uvs.ToArray()
            // colors = _colors.ToArray()
        };
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();
        
        // Set index buffer to 32bits, which allow up to 2^32 vertices per mesh (default: 16bits)
        // mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        
        meshRenderer.material.color = Color.white;
        
        meshFilter.mesh = mesh;

        Debug.Log("vertices: " + mesh.vertices.Length);
        Debug.Log("triangles: " + mesh.triangles.Length);
        Debug.Log("uvs: " + mesh.uv.Length);
    }
}