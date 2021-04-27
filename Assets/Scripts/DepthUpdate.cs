using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthUpdate : MonoBehaviour
{

// Update is called once per frame
void Update()
{

        int depth = (int) ((((16 - (((int) Player.tr.position.y + 8))) / 16f)) * 100);
        if (depth < 10)
                GetComponent<UnityEngine.UI.Text>().text = Level.instance.currentLayer.depth + "0" + depth + " M";
        else if (depth >= 100)
                GetComponent<UnityEngine.UI.Text>().text = Level.instance.currentLayer.depth+1 + "00 M";
        else
                GetComponent<UnityEngine.UI.Text>().text = Level.instance.currentLayer.depth + "" + depth + " M";



}
}
