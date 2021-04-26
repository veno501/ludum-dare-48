using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
public GameObject player;
public GameObject depthCounterTextParent;
public GameObject continueTextParent;

public UnityEngine.UI.Text depthCounterScoreText;

private bool playerIsDead = false;

void Update()
{
        if(playerIsDead && Input.anyKey)
        {
                // fade to main Scene
                Debug.Log("Return to main menu!");
        }
}

public void OnEliminated()
{
        depthCounterScoreText.text = Level.instance.currentLayer.depth + "00 m";
        depthCounterTextParent.SetActive(true);
        continueTextParent.SetActive(true);
        player.SetActive(false);
        playerIsDead = true;
}
}
