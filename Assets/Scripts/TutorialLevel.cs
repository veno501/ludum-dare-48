using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel : MonoBehaviour
{

public GameObject mineralsText;
public GameObject deeperText;

void Start()
{
        Player.tr.position = new Vector3(0.2f, 7.3f, 0f);
        Player.instance.stats.samplesRequired = 1f;
        Player.instance.stats.requiredText.text = "1";
        StartCoroutine(EnableMineralsTextCoroutine());
}

public void EnableDeeperText()
{
        Player.instance.stats.collectedText.text = "" + (int) Player.instance.stats.samplesCollected;
        deeperText.SetActive(true);
        mineralsText.SetActive(false);
}

IEnumerator EnableMineralsTextCoroutine()
{
        yield return new WaitForSeconds(1);
        mineralsText.SetActive(true);
}
}
