using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderGenerator : MonoBehaviour
{

// image height in px
int imageSizeWidth = 32;
int imageSizeHeight = 18;
public Sprite tileSprite;
public GameObject tilePrefab;

void Start()
{
        Texture2D bgTexture = createBackgroundTexture(Application.dataPath + "/Blocks/lu.png");

        createBackgroundColliders(bgTexture);
}

void createBackgroundColliders(Texture2D bgTexture)
{
        for(int x = 0; x < bgTexture.height; x++)
        {
                GameObject obParent = new GameObject("Row " + x);
                obParent.transform.SetParent(this.transform);

                for(int y = 0; y < bgTexture.width; y++)
                {
                        Color pix = bgTexture.GetPixel(y, x);
                        if (pix == Color.black)
                        {
                                GameObject ob = Instantiate(tilePrefab, new Vector3(-imageSizeWidth/2+y, -imageSizeHeight/2+x, 0), Quaternion.identity) as GameObject;
                                ob.transform.SetParent(obParent.transform);
                        }

                }
        }
        this.transform.position = new Vector2(0.5f, 0.5f);
        this.transform.localScale = new Vector3(1, 1, 1);
}

Texture2D createBackgroundTexture(string path)
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
