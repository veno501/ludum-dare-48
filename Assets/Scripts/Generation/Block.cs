using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Block
{
public Block blockLeft, blockRight;

public bool isEntryBlock;
public bool isExitBlock;

// public int numOfMinerals;
// public int numOfEnemies;

public BlockData data;
public Transform root;

public Block(Block _blockLeft, Block _blockRight, bool _isEntryBlock, bool _isExitBlock, BlockData _data)
{
        // numOfMinerals = _numOfMinerals;
        // numOfEnemies = _numOfEnemies;
        blockLeft = _blockLeft;
        blockRight = _blockRight;
        // blockDown = _blockDown;
        isEntryBlock = _isEntryBlock;
        isExitBlock = _isExitBlock;
        data = _data;
}
}
