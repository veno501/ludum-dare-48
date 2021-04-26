using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockData
{
// public Block currentlyGeneratedBlock;
public Texture2D texture;
// public Vector3 samples
public List<Vector3> enemySpawnPoints = new List<Vector3>();     // Color.red
public List<Vector3> sampleSpawnPoints = new List<Vector3>();     // Color.blue
public List<Vector3> entryPoints = new List<Vector3>();     // Color.green

public BlockData(Texture2D _texture)
{
        texture = _texture;
        ParseTexture();
}

public void ParseTexture()
{
        for(int x = 0; x < texture.height; x++)
        {
                for(int y = 0; y < texture.width; y++)
                {
                        Color pixel = texture.GetPixel(y, x);
                        if (pixel == Color.red)
                        {
                                enemySpawnPoints.Add(new Vector3(y, x, 0.0f));
                        }
                        if (pixel == Color.blue)
                        {
                                sampleSpawnPoints.Add(new Vector3(y, x, 0.0f));
                        }
                        if (pixel == Color.green)
                        {
                                entryPoints.Add(new Vector3(y, x, 0.0f));
                        }
                }
        }
}
}
