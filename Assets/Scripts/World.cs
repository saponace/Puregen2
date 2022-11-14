using System;
using System.Collections.Generic;
using UnityEngine;


// TODO: change vertexcolors with second video solution for solid blocks colors
// Main tutorial: https://www.youtube.com/watch?v=hRnqIqYK-4Y
// Solid color blocks tutorial: https://www.youtube.com/watch?v=esyhLeEkDvU
public class World : MonoBehaviour
{
    public Material material;

    public BlockType[] blockTypes = new BlockType[]
    {
        new() {name = "Stone"},
        new() {name = "Dirt"},
        new() {name = "Air", isSolid = false},
        new() {name = "puihp", isSolid = false},
    };
    
    public VoxelColor[] worldColors = {
        new() {
            color = Color.blue,
            metallic = 1,
            smoothness = 0.75f
        }
    };
    
    [System.Serializable]
    public class BlockType
    {
        public string name;
        public bool isSolid = true;
    }
}