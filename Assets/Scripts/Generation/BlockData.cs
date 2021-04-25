using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockData
{
    // public Block currentlyGeneratedBlock;
    public Texture2D texture;
    // public Vector3 samples
    public List<Vector3> enemySpawnPoints; // Color.red
    public List<Vector3> sampleSpawnPoints; // Color.blue
    public List<Vector3> entryPoints; // Color.green

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
                    enemySpawnPoints.Add(new Vector3(x, y, 0.0f));
                }
                if (pixel == Color.blue)
                {
                    sampleSpawnPoints.Add(new Vector3(x, y, 0.0f));
                }
                if (pixel == Color.green)
                {
                    entryPoints.Add(new Vector3(x, y, 0.0f));
                }
            }
        }
    }
}
