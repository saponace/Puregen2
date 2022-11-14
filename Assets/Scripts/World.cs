using System;
using System.Collections.Generic;
using UnityEngine;


// Main tutorial: https://www.youtube.com/watch?v=ppFUTg_-Rbg
// Alternate tutorial: https://www.youtube.com/watch?v=5rYSBO0auOY
/**
 * TODO:
 * - Cleanup code:
 *   - Create MeshData (that contains uvs, colors, vertices, etc.)
 *   - Move world generation to separate class
 *   - Create enums folder
 *   - Externalize block aspect (color, etc.)
 *   - Handle blocks smoothness and other aspects in GetBlockColor
 *   - after cleaning done: create one commit and force-push
 * - Create one gameObject per chunk  (cf. https://www.youtube.com/watch?v=ppFUTg_-Rbg)
 */
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