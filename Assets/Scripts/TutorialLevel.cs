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

        StartCoroutine(EnableMineralsText());
}
IEnumerator EnableMineralsText()
{
        yield return new WaitForSeconds(0);
        mineralsText.SetActive(true);
}
}
