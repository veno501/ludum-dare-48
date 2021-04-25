using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // current layer reference here
    // current block reference here
    // block object pool
    public static Level instance;

    void Awake()
    {
        instance = this;
    }

    void StartLayer()
    {
        GenerateLayer();
        // current block is entrance block

        StartBlock();
    }

    void GenerateLayer()
    {
        // generate layer data, set new depth score, difficulty
    }

    public void SwitchLayer()
    {
        // scrap current layer

        StartLayer();
    }

    void StartBlock()
    {
        GenerateBlock();
        // fade in
        // set player position, velocity for entry
        // disable entrance tunnel for a few seconds
    }

    void GenerateBlock()
    {
        // generate block object and colliders from layer
        // spawn samples, spawn enemies, spawn block switch triggers
    }

    public void SwitchBlock()
    {
        // fade out
        // scrap current block
        // current block is next block
        
        StartBlock();
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
}
