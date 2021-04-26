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
    BlockGenerator layerGenerator;
    ColliderGenerator blockGenerator;

    void Awake()
    {
        instance = this;
        layerGenerator = GetComponentInChildren<BlockGenerator>();
        blockGenerator = GetComponentInChildren<ColliderGenerator>();

        // !
        StartLayer();
    }

    void StartLayer()
    {
        // generate layer data, set new depth score, difficulty
        currentLayer = layerGenerator.GenerateLayer();
        currentBlock = currentLayer.entryBlock;
        StartBlock();
    }

    public void SwitchLayer()
    {
        // scrap current layer
        StartCoroutine(FadeOut(fadeDuration * 2f));

        Invoke("StartLayer", fadeDuration * 2f);
    }

    void StartBlock()
    {
        // generate block object and colliders from layer
        // !spawn samples, spawn enemies, spawn block switch triggers

        // currentBlock = _block;
        currentBlock.root = blockGenerator.GenerateColliders(currentBlock).transform;
        blockGenerator.SpawnEnemies(currentBlock);
        blockGenerator.SpawnSamples(currentBlock);

        Invoke("EnableTriggers", 2f);
        StartCoroutine(FadeIn(fadeDuration));
        
        // fade in
        // set player position, velocity for entry
        // disable entrance tunnel for a few seconds

        SetPlayerToSpawnPoint();
    }

    public void SwitchBlock(Block _block)
    {
        // fade out
        // scrap current block
        // current block is next block
        StartCoroutine(FadeOut(fadeDuration));
        Destroy(currentBlock.root.gameObject, 0.5f);

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

    IEnumerator FadeOut(float _t)
    {
        for (float t = _t; t > 0.0f; t -= Time.deltaTime)
        {
            Color c = fadeUI.color;
            c.a = (_t - t) / _t;
            fadeUI.color = c;
            yield return null;
        }
    }

    IEnumerator FadeIn(float _t)
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

    void SetPlayerToSpawnPoint()
    {
        Player.tr.position = new Vector3(-Player.tr.position.x, -Player.tr.position.y, 0f);
    }
}
