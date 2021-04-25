using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
public Block blockLeft, blockRight, blockDown;

public bool isEntryBlock;

public int numOfMinerals;
public int numOfEnemies;

public Texture2D texture;

public Block(Texture2D _texture, int _numOfMinerals, int _numOfEnemies, Block _blockLeft, Block _blockRight, Block _blockDown, bool _isEntryBlock)
{
        numOfMinerals = _numOfMinerals;
        numOfEnemies = _numOfEnemies;
        texture = _texture;
        blockLeft = _blockLeft;
        blockRight = _blockRight;
        blockDown = _blockDown;
        isEntryBlock = _isEntryBlock;
}
}
