using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer
{
public List<Block> blocks = new List<Block>();
public Block entryBlock;
public Block entryBlockLower;

public Layer(List<Block> _blocks, Block _entryBlock, Block _entryBlockLower)
{
        blocks = _blocks;
        entryBlock = _entryBlock;
        entryBlockLower = _entryBlockLower;
}
}
