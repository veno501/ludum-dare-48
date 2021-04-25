using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderGenerator : MonoBehaviour
{

// image height in px
public int imageWidth = 32;
public int imageHeight = 18;
// public Sprite tileSprite;
public GameObject tilePrefab;
public GameObject blockBlueprint;

void GenerateAll()
{
        // Texture2D bgTexture = GenerateDebugVisualization(Application.dataPath + "/Blocks/lu.png");

        // GenerateColliders(bgTexture);
}

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

Texture2D GenerateDebugVisualization(string path)
{
        GameObject bg = new GameObject();
        bg.transform.localScale = new Vector2(100, 100);
        bg.transform.position = new Vector2(0, 0);
        bg.AddComponent<SpriteRenderer>();

        Texture2D bgTexture = LoadPNG(path);
        Sprite bgSprite = Sprite.Create(bgTexture, new Rect(0, 0, bgTexture.width, bgTexture.height), new Vector2(0.5f, 0.5f));
        SpriteRenderer spriteRenderer = bg.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = bgSprite;

        return bgTexture;
}

Texture2D LoadPNG(string filePath)
{
        Texture2D tex = null;
        byte[] fileData;

        if (System.IO.File.Exists(filePath)) {
                fileData = System.IO.File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2);
                tex.LoadImage(fileData);
        }
        return tex;
}
}
