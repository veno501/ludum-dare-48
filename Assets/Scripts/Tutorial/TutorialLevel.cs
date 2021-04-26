using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel : MonoBehaviour
{

public GameObject mineralsText;
public GameObject deeperText;

public static bool tutorialFinished = false;

private bool advanceTextShowed = false;

void Start()
{
        Player.tr.position = new Vector3(0.2f, 7.3f, 0f);
        Player.instance.stats.samplesRequired = 1f;
        Player.instance.stats.requiredText.text = "1";
}

void Update() {
        if (advanceTextShowed == false && Player.tr.position.y < 4f)
        {
                advanceTextShowed = true;
                mineralsText.SetActive(true);
        }
}

public void EnableDeeperText()
{
        Player.instance.stats.collectedText.text = "" + (int) Player.instance.stats.samplesCollected;
        deeperText.SetActive(true);
        mineralsText.SetActive(false);

        BlockSwitchTrigger[] exitTriggers = GetComponentsInChildren<BlockSwitchTrigger>();
        foreach (BlockSwitchTrigger trigger in exitTriggers)
        {
                trigger.isEnabled = true;
        }
}
}
