using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSwitchTrigger : MonoBehaviour
{
    public char direction;
    public bool isEnabled = true;
    // public Block link;

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (!isEnabled) return;

        if (hit.GetComponentInParent<Player>()) {
            Debug.Log("Switching block \'" + direction + "\'");
            Level.instance.SwitchBlock(/* link, direction */);
        }
    }
}
