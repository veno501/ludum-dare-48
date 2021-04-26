using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Layer
{
        public List<Block> blocks = new List<Block>();
        public Block entryBlock;
        public float depth;
        // public Block entryBlockLower;

        public Layer(List<Block> _blocks, Block _entryBlock, float _depth)
        {
                blocks = _blocks;
                entryBlock = _entryBlock;
                depth = _depth;
                // entryBlockLower = _entryBlockLower;
        }
}
