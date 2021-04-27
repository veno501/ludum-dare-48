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
public List<Vector3> entryPoints = new List<Vector3>();     // Color.green
public List<Vector3> sampleSpawnPoints = new List<Vector3>();     // yellow
public List<int> sampleSpawnPointsRotations = new List<int>();

private Color yellow = new Color(1,1,0);

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
                        else if (pixel == yellow)
                        {
                                int rotation = 0;
                                if (texture.GetPixel(y, x-1) == Color.blue)
                                        rotation = 0;
                                else if (texture.GetPixel(y, x+1) == Color.blue)
                                        rotation = 180;
                                else if (texture.GetPixel(y-1, x) == Color.blue)
                                        rotation = 270;
                                else if (texture.GetPixel(y+1, x) == Color.blue)
                                        rotation = 90;

                                sampleSpawnPointsRotations.Add(rotation);
                                sampleSpawnPoints.Add(new Vector3(y, x, 0.0f));
                        }
                        else if (pixel == Color.green)
                        {
                                entryPoints.Add(new Vector3(y, x, 0.0f));
                        }

                }
        }
        int numSamples = Random.Range(1, 4);
        for (; numSamples > sampleSpawnPoints.Count;)
        {
                int i = Random.Range(0, sampleSpawnPoints.Count);
                sampleSpawnPoints.RemoveAt(i);
                sampleSpawnPointsRotations.RemoveAt(i);
        }

        int numEnemies = Random.Range(2, 5);
        for (; numEnemies > enemySpawnPoints.Count;)
        {
                int j = Random.Range(0, enemySpawnPoints.Count);
                enemySpawnPoints.RemoveAt(j);
        }
}
}
