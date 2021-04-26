using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{

// image height in px
public int imageWidth = 32;
public int imageHeight = 18;
// public Sprite tileSprite;
public GameObject tilePrefab;
public GameObject blockBlueprint;
public List<GameObject> enemyPrefabs = new List<GameObject>();
public GameObject samplePrefab;

public GameObject GenerateColliders(Block _block)
{
        GameObject newBlock = Instantiate(blockBlueprint) as GameObject;
        GameObject colliderRoot = new GameObject("colliders");
        colliderRoot.transform.SetParent(newBlock.transform);

        for(int x = 0; x < imageHeight; x++)
        {
                GameObject colliderRow = new GameObject("row " + x);
                colliderRow.transform.SetParent(colliderRoot.transform);

                for(int y = 0; y < imageWidth; y++)
                {
                        Color pixel = _block.data.texture.GetPixel(y, x);
                        if (pixel == Color.black)
                        {
                                Vector2 spawnPosition = new Vector3(-imageWidth/2+y, -imageHeight/2+x, 0) + new Vector3(0.5f, 0.5f, 0f);
                                GameObject ob = Instantiate(tilePrefab, spawnPosition, Quaternion.identity) as GameObject;
                                ob.transform.SetParent(colliderRow.transform);
                        }

                }
        }
        this.transform.position = new Vector2(0.5f, 0.5f);
        this.transform.localScale = new Vector3(1, 1, 1);

        return newBlock;
}

public void SpawnEnemies(Block _block)
{
        GameObject enemyRoot = new GameObject("enemies");
        enemyRoot.transform.SetParent(_block.root);

        List<Vector3> points = _block.data.enemySpawnPoints;
        foreach (Vector3 point in points)
        {
                GameObject ob = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], 
                        new Vector3(point.x-imageWidth/2, point.y-imageHeight/2, 0f), Quaternion.identity) as GameObject;

                ob.transform.SetParent(enemyRoot.transform);
        }
}

public void SpawnSamples(Block _block)
{
        GameObject sampleRoot = new GameObject("samples");
        sampleRoot.transform.SetParent(_block.root);

        List<Vector3> points = _block.data.sampleSpawnPoints;
        foreach (Vector3 point in points)
        {
                GameObject ob = Instantiate(samplePrefab, 
                        new Vector3(point.x-imageWidth/2+0.5f, point.y-imageHeight/2+0.5f, 0f), Quaternion.identity) as GameObject;

                ob.transform.SetParent(sampleRoot.transform);
        }
}

public void SetPlayerToSpawnPoint(Block _block, char _previousTriggerSide)
{
        foreach (Vector3 point in _block.data.entryPoints)
        {
                // point is LEFT spawn
                if (point.x == 0 && _previousTriggerSide == 'r') {
                        Player.tr.position = new Vector3(point.x-imageWidth/2+0.5f, point.y-imageHeight/2+0.5f, 0f);
                        Debug.Log("Spawning at " + Player.tr.position);
                }
                // point is RIGHT spawn
                else if (point.x == imageWidth-1 && _previousTriggerSide == 'l') {
                        Player.tr.position = new Vector3(point.x-imageWidth/2+0.5f, point.y-imageHeight/2+0.5f, 0f);
                        Debug.Log("Spawning at " + Player.tr.position);
                }
                // point is UP spawn
                else if (point.y == imageHeight-1 && _previousTriggerSide == 'd') {
                        Player.tr.position = new Vector3(point.x-imageWidth/2+0.5f, point.y-imageHeight/2+0.5f, 0f);
                        Debug.Log("Spawning at " + Player.tr.position);
                }
        }
        // Player.tr.localRotation *= Quaternion.Euler(0, 0, 180);
}

// Texture2D GenerateDebugVisualization(string path)
// {
//         GameObject bg = new GameObject();
//         bg.transform.localScale = new Vector2(100, 100);
//         bg.transform.position = new Vector2(0, 0);
//         bg.AddComponent<SpriteRenderer>();

//         Texture2D bgTexture = LoadPNG(path);
//         Sprite bgSprite = Sprite.Create(bgTexture, new Rect(0, 0, bgTexture.width, bgTexture.height), new Vector2(0.5f, 0.5f));
//         SpriteRenderer spriteRenderer = bg.GetComponent<SpriteRenderer>();
//         spriteRenderer.sprite = bgSprite;

//         return bgTexture;
// }

// Texture2D LoadPNG(string filePath)
// {
//         Texture2D tex = null;
//         byte[] fileData;

//         if (System.IO.File.Exists(filePath)) {
//                 fileData = System.IO.File.ReadAllBytes(filePath);
//                 tex = new Texture2D(2, 2);
//                 tex.LoadImage(fileData);
//         }
//         return tex;
// }
}
