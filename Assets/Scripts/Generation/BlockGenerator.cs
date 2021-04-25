using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{

[SerializeField]
public int blocksPerLayer = 5;
[SerializeField]
public int pathsDownPerLayer = 2;

List<Layer> layers = new List<Layer>();

Block secondEntryBlock;
Block firstEntryBlock;

Texture2D tex;

void Start()
{
        secondEntryBlock = new Block(null, 0, 0, null, null, null, true);
        firstEntryBlock = new Block(null, 0, 0, null, null, secondEntryBlock, true);

        List<Block> firstLayerBlocks = new List<Block>();
        List<Block> secondLayerBlocks = new List<Block>();

        firstLayerBlocks.Add(firstEntryBlock);
        secondLayerBlocks.Add(secondEntryBlock);

        layers.Add(new Layer(firstLayerBlocks, firstEntryBlock, secondEntryBlock));

        AddLayer(pathsDownPerLayer);
        AddLayer(pathsDownPerLayer);
        AddLayer(pathsDownPerLayer);

        int layerCounter = 0;
        foreach (Layer layer in layers)
        {
                List<Block> blocks = layer.blocks;
                int blockCounter = 0;
                foreach (Block block in blocks)
                {
                        GameObject test = new GameObject();
                        test.transform.localScale = new Vector2(5, 5);
                        test.transform.position = new Vector2(blockCounter * 5, layerCounter * -2.5f);
                        test.AddComponent<SpriteRenderer>();

                        Texture2D blankTexture = getTexture(block);
                        Sprite blankSprite = Sprite.Create(blankTexture, new Rect(0, 0, blankTexture.width, blankTexture.height), new Vector2(0.5f, 0.5f));
                        SpriteRenderer spriteRenderer = test.GetComponent<SpriteRenderer>();
                        spriteRenderer.sprite = blankSprite;

                        blockCounter++;
                }
                layerCounter++;
        }
}

void AddLayer(int numOfPathsDown)
{
        List<Block> blocks = new List<Block>();
        Block entryBlock = layers[layers.Count-1].entryBlockLower;
        Block entryBlockLower = new Block(null, 0, 0, null, null, null, true);

        Block leftMostBlock = entryBlock;
        Block rightMostBlock = entryBlock;

        blocks.Add(entryBlock);

        int[] ranArray = getRandomPathsDown(numOfPathsDown);
        int ranArrayCounter = 0;
        // foreach (int j in ranArray)


        for (int i = 0; i < blocksPerLayer-1; i++)
        {
                Block newBlock = new Block(null, 0, 0, null, null, null, false);

                if (ranArrayCounter < ranArray.Length && i == ranArray[ranArrayCounter])
                {
                        ranArrayCounter++;
                        newBlock.blockDown = entryBlockLower;
                }

                // insert left or right
                if (Random.Range(0, 2) < 1)
                        blocks.Insert(0, newBlock);
                else
                        blocks.Add(newBlock);
        }

        for (int i = 0; i < blocks.Count; i++)
        {
                Block previousBlock = (i == 0) ? null : blocks[i-1];
                Block currentBlock = blocks[i];
                Block nextBlock = (i == blocks.Count - 1) ? null : blocks[i+1];

                if (previousBlock != null)
                        currentBlock.blockLeft = previousBlock;
                if (nextBlock != null)
                        currentBlock.blockRight = nextBlock;
        }

        layers.Add(new Layer(blocks, entryBlock, entryBlockLower));
}

int[] getRandomPathsDown(int numOfPathsDown)
{
        int[] ranArray = new int[blocksPerLayer-1];
        int ranEndIndex = ranArray.Length;
        for (int i = 0; i < ranArray.Length; i++)
                ranArray[i] = i;

        int[] ranPaths = new int[numOfPathsDown];
        for (int i = 0; i < ranPaths.Length; i++)
        {
                int random = Random.Range(0, ranEndIndex);
                ranPaths[i] = ranArray[random];
                ranArray[random] = ranArray[ranArray.Length-1];
                ranEndIndex--;
        }

        System.Array.Sort(ranPaths);
        return ranPaths;
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

Texture2D getTexture(Block block)
{
        if (block.blockLeft != null && block.blockRight == null && block.blockDown == null)
        {
                if (block.isEntryBlock)
                        return LoadPNG(Application.dataPath + "/Blocks/lu.png");
                else
                        return LoadPNG(Application.dataPath + "/Blocks/l.png");
        }
        else if (block.blockLeft == null && block.blockRight != null && block.blockDown == null)
        {
                if (block.isEntryBlock)
                        return LoadPNG(Application.dataPath + "/Blocks/ru.png");
                else
                        return LoadPNG(Application.dataPath + "/Blocks/r.png");
        }
        else if (block.blockLeft != null && block.blockRight != null && block.blockDown == null)
        {
                if (block.isEntryBlock)
                        return LoadPNG(Application.dataPath + "/Blocks/lru.png");
                else
                        return LoadPNG(Application.dataPath + "/Blocks/lr.png");
        }
        else if (block.blockLeft != null && block.blockRight != null && block.blockDown != null)
        {
                return LoadPNG(Application.dataPath + "/Blocks/lrd.png");
        }
        else if (block.blockLeft != null && block.blockRight == null && block.blockDown != null)
        {
                return LoadPNG(Application.dataPath + "/Blocks/ld.png");
        }
        else if (block.blockLeft == null && block.blockRight != null && block.blockDown != null)
        {
                return LoadPNG(Application.dataPath + "/Blocks/rd.png");
        }
        else
        {
                return LoadPNG(Application.dataPath + "/Blocks/du.png");
        }
}
}
