using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerGenerator : MonoBehaviour
{

[SerializeField]
public int blocksPerLayer = 5;
[SerializeField]
public int pathsDownPerLayer = 2;

public List<Texture2D> L = new List<Texture2D>();
public List<Texture2D> R = new List<Texture2D>();
public List<Texture2D> LR = new List<Texture2D>();
public List<Texture2D> LD = new List<Texture2D>();
public List<Texture2D> RD = new List<Texture2D>();
public List<Texture2D> LRD = new List<Texture2D>();

[Space]
public List<Texture2D> LU = new List<Texture2D>();
public List<Texture2D> RU = new List<Texture2D>();
public List<Texture2D> LRU = new List<Texture2D>();

// List<Layer> layers = new List<Layer>();

// Block secondEntryBlock;
// Block firstEntryBlock;

public Layer GenerateLayer(float _depth)
{
        List<Block> blocks = new List<Block>();
        // Block entryBlock = layers[layers.Count-1].entryBlockLower;
        Block entryBlock = new Block(null, null, true, false, null);
        // Block entryBlockLower = new Block(null, 0, 0, null, null, null, true);
        blocks.Add(entryBlock);

        int[] randArray = getUniqueRandomRange(pathsDownPerLayer);
        int randArrayIndex = 0;

        // create blocks, insert in list
        for (int i = 0; i < blocksPerLayer-1; i++)
        {
                Block newBlock = new Block(null, null, false, false, null);

                // add path down to chosen blocks
                if (randArrayIndex < randArray.Length && i == randArray[randArrayIndex])
                {
                        randArrayIndex++;
                        // newBlock.blockDown = entryBlockLower;
                        newBlock.isExitBlock = true;
                }

                // insert left or right
                if (Random.Range(0, 2) < 1)
                        blocks.Insert(0, newBlock);
                else
                        blocks.Add(newBlock);
        }

        // link blocks in list and add textures
        for (int i = 0; i < blocks.Count; i++)
        {
                Block previousBlock = (i == 0) ? null : blocks[i-1];
                Block currentBlock = blocks[i];
                Block nextBlock = (i == blocks.Count - 1) ? null : blocks[i+1];

                if (previousBlock != null)
                        currentBlock.blockLeft = previousBlock;
                if (nextBlock != null)
                        currentBlock.blockRight = nextBlock;

                // currentBlock.texture = getTexture(currentBlock);


                // currentBlock.data = new BlockData(getTexture(currentBlock)); // !!
                currentBlock.data = new BlockData(
                        GetBlockTexture(currentBlock.blockLeft != null, currentBlock.blockRight != null, currentBlock.isExitBlock, currentBlock.isEntryBlock)
                        );
        }

        // layers.Add(new Layer(blocks, entryBlock, entryBlockLower));
        Layer newLayer = new Layer(blocks, entryBlock, _depth);
        return newLayer;
}

// void GenerateLayers(int n)
// {
//         secondEntryBlock = new Block(null, 0, 0, null, null, null, true);
//         firstEntryBlock = new Block(null, 0, 0, null, null, secondEntryBlock, true);

//         List<Block> firstLayerBlocks = new List<Block>();
//         List<Block> secondLayerBlocks = new List<Block>();

//         firstLayerBlocks.Add(firstEntryBlock);
//         secondLayerBlocks.Add(secondEntryBlock);

//         layers.Add(new Layer(firstLayerBlocks, firstEntryBlock, secondEntryBlock));

//         for (int i = 0; i < n; i++)
//         {
//                 AddLayer(pathsDownPerLayer);
//         }

//         int layerCounter = 0;
//         foreach (Layer layer in layers)
//         {
//                 List<Block> blocks = layer.blocks;
//                 int blockCounter = 0;
//                 foreach (Block block in blocks)
//                 {
//                         GameObject test = new GameObject();
//                         test.transform.localScale = new Vector2(5, 5);
//                         test.transform.position = new Vector2(blockCounter * 5, layerCounter * -2.5f);
//                         test.AddComponent<SpriteRenderer>();

//                         Texture2D blankTexture = getTexture(block);
//                         Sprite blankSprite = Sprite.Create(blankTexture, new Rect(0, 0, blankTexture.width, blankTexture.height), new Vector2(0.5f, 0.5f));
//                         SpriteRenderer spriteRenderer = test.GetComponent<SpriteRenderer>();
//                         spriteRenderer.sprite = blankSprite;

//                         blockCounter++;
//                 }
//                 layerCounter++;
//         }
// }

// void AddLayer(int numOfPathsDown)
// {
//         List<Block> blocks = new List<Block>();
//         Block entryBlock = layers[layers.Count-1].entryBlockLower;
//         Block entryBlockLower = new Block(null, 0, 0, null, null, null, true);

//         Block leftMostBlock = entryBlock;
//         Block rightMostBlock = entryBlock;

//         blocks.Add(entryBlock);

//         int[] ranArray = getUniqueRandomRange(numOfPathsDown);
//         int ranArrayCounter = 0;

//         // create blocks, insert in list
//         for (int i = 0; i < blocksPerLayer-1; i++)
//         {
//                 Block newBlock = new Block(null, 0, 0, null, null, null, false);

//                 // add path down to chosen blocks
//                 if (ranArrayCounter < ranArray.Length && i == ranArray[ranArrayCounter])
//                 {
//                         ranArrayCounter++;
//                         newBlock.blockDown = entryBlockLower;
//                 }

//                 // insert left or right
//                 if (Random.Range(0, 2) < 1)
//                         blocks.Insert(0, newBlock);
//                 else
//                         blocks.Add(newBlock);
//         }

//         // link blocks in list
//         for (int i = 0; i < blocks.Count; i++)
//         {
//                 Block previousBlock = (i == 0) ? null : blocks[i-1];
//                 Block currentBlock = blocks[i];
//                 Block nextBlock = (i == blocks.Count - 1) ? null : blocks[i+1];

//                 if (previousBlock != null)
//                         currentBlock.blockLeft = previousBlock;
//                 if (nextBlock != null)
//                         currentBlock.blockRight = nextBlock;
//         }

//         layers.Add(new Layer(blocks, entryBlock, entryBlockLower));
// }

int[] getUniqueRandomRange(int numOfPathsDown)
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

Texture2D GetBlockTexture(bool left, bool right, bool down, bool up)
{
        if (up) {
                if (left && !right && !down)
                        return LU[Random.Range(0, LU.Count)];
                else if (!left && right && !down) {
                        return RU[Random.Range(0, RU.Count)];
                }
                else if (left && right && !down) {
                        return LRU[Random.Range(0, LRU.Count)];
                }
        }
        else if (!up) {
                if (left && !right && !down) {
                        return L[Random.Range(0, L.Count)];
                }
                else if (!left && right && !down) {
                        return R[Random.Range(0, R.Count)];
                }
                else if (left && right && !down) {
                        return LR[Random.Range(0, LR.Count)];
                }
                else if (left && !right && down) {
                        return LD[Random.Range(0, LD.Count)];
                }
                else if (!left && right && down) {
                        return RD[Random.Range(0, RD.Count)];
                }
                else if (left && right && down) {
                        return LRD[Random.Range(0, LRD.Count)];
                }
        }
        return null;
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

public Texture2D getTexture(Block block)
{
        if (block.blockLeft != null && block.blockRight == null && block.isExitBlock == false)
        {
                if (block.isEntryBlock)
                        return LoadPNG(Application.dataPath + "/Resources/Blocks/lu.png");
                else
                        return LoadPNG(Application.dataPath + "/Resources/Blocks/l.png");
        }
        else if (block.blockLeft == null && block.blockRight != null && block.isExitBlock == false)
        {
                if (block.isEntryBlock)
                        return LoadPNG(Application.dataPath + "/Resources/Blocks/ru.png");
                else
                        return LoadPNG(Application.dataPath + "/Resources/Blocks/r.png");
        }
        else if (block.blockLeft != null && block.blockRight != null && block.isExitBlock == false)
        {
                if (block.isEntryBlock)
                        return LoadPNG(Application.dataPath + "/Resources/Blocks/lru.png");
                else
                        return LoadPNG(Application.dataPath + "/Resources/Blocks/lr.png");
        }
        else if (block.blockLeft != null && block.blockRight != null && block.isExitBlock == true)
        {
                return LoadPNG(Application.dataPath + "/Resources/Blocks/lrd.png");
        }
        else if (block.blockLeft != null && block.blockRight == null && block.isExitBlock == true)
        {
                return LoadPNG(Application.dataPath + "/Resources/Blocks/ld.png");
        }
        else if (block.blockLeft == null && block.blockRight != null && block.isExitBlock == true)
        {
                return LoadPNG(Application.dataPath + "/Resources/Blocks/rd.png");
        }
        else
        {
                return LoadPNG(Application.dataPath + "/Resources/Blocks/du.png");
        }
}
}
