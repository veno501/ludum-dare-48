using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    // current layer reference here
    [SerializeField]
    public Layer currentLayer;
    // current block reference here
    [SerializeField]
    public Block currentBlock;
    public float fadeDuration = 0.5f;
    public Image fadeUI;
    // public GameObject currentRoot;
    // block object pool
    public static Level instance;
    LayerGenerator layerGenerator;
    BlockGenerator blockGenerator;
    char previousTriggerSide;

    void Awake()
    {
        instance = this;
        layerGenerator = GetComponentInChildren<LayerGenerator>();
        blockGenerator = GetComponentInChildren<BlockGenerator>();

        // !
        StartLayer();
    }

    void StartLayer()
    {
        Player.instance.stats.ResetCollectedSamples();
        // generate layer data, set new depth score, difficulty
        float currentDepth = currentLayer == null ? 1f : currentLayer.depth;
        currentLayer = layerGenerator.GenerateLayer(currentDepth + 1.0f);
        Debug.Log("Depth: -" + currentLayer.depth + "00m");
        currentBlock = currentLayer.entryBlock;
        previousTriggerSide = 'd';
        StartBlock();
    }

    public void SwitchLayer()
    {
        // scrap current layer
        Destroy(currentBlock.root.gameObject, fadeDuration*2f);
        StartCoroutine(FadeOut(fadeDuration*2f));

        Invoke("StartLayer", fadeDuration*2f);
    }

    void StartBlock()
    {
        if (currentBlock == null)
            Debug.LogError("Block is NULL!");
        // generate block object and colliders from layer
        // !spawn samples, spawn enemies, spawn block switch triggers

        // currentBlock = _block;
        currentBlock.root = blockGenerator.GenerateColliders(currentBlock).transform;
        blockGenerator.SpawnEnemies(currentBlock);
        if (!currentBlock.isCleared)
        {
            blockGenerator.SpawnSamples(currentBlock);
        }
        blockGenerator.SetPlayerToSpawnPoint(currentBlock, previousTriggerSide);

        Invoke("EnableTriggers", 2f);
        StartCoroutine(FadeIn(fadeDuration));
        
        // fade in
        // set player position, velocity for entry
        // disable entrance tunnel for a few seconds
    }

    public void SwitchBlock(Block _block, char _triggerSide)
    {
        // fade out
        // scrap current block
        // current block is next block
        previousTriggerSide = _triggerSide;

        GameObject[] samples = GameObject.FindGameObjectsWithTag("Sample");
        if (samples.Length == 0) currentBlock.isCleared = true;
        Vector3[] pointsCopy = currentBlock.data.sampleSpawnPoints.ToArray();
        foreach (Vector3 point in pointsCopy)
        {
            bool stillAlive = false;
            foreach (GameObject ob in samples)
            {
                if (ob.transform.position.x+blockGenerator.imageWidth/2-0.5f == point.x &&
                    ob.transform.position.y+blockGenerator.imageHeight/2-0.5f == point.y)
                {
                    stillAlive = true;
                }
            }
            if (!stillAlive)
                currentBlock.data.sampleSpawnPoints.Remove(point);
        }

        StartCoroutine(FadeOut(fadeDuration));
        Destroy(currentBlock.root.gameObject, fadeDuration);

        currentBlock = _block;
        Invoke("StartBlock", fadeDuration);
        // StartBlock(_block);
    }

    public void OnGameOver()
    {
        // display finish stats, depth
    }

    public void FinishGame()
    {
        // fade out
        //-- or load menu scene
        // animate wreckage ending
        // display retry menu...
    }

    public IEnumerator FadeOut(float _t)
    {
        for (float t = _t; t > 0.0f; t -= Time.deltaTime)
        {
            Color c = fadeUI.color;
            c.a = (_t - t) / _t;
            fadeUI.color = c;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float _t)
    {
        for (float t = _t; t > 0.0f; t -= Time.deltaTime)
        {
            Color c = fadeUI.color;
            c.a = t / _t;
            fadeUI.color = c;
            yield return null;
        }
    }

    void EnableTriggers()
    {
        BlockSwitchTrigger[] exitTriggers = currentBlock.root.GetComponentsInChildren<BlockSwitchTrigger>();
        foreach (BlockSwitchTrigger trigger in exitTriggers)
        {
            trigger.isEnabled = true;
        }
    }
}
