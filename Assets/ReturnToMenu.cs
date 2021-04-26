using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
public GameObject player;
public GameObject depthCounterTextParent;
public GameObject continueTextParent;

public UnityEngine.UI.Text depthCounterScoreText;

public void OnEliminated()
{
        depthCounterScoreText.text = Level.instance.currentLayer.depth + "00 m";
        depthCounterTextParent.SetActive(true);
        continueTextParent.SetActive(true);
        player.SetActive(false);

        StartCoroutine(WaitForConfirmation());
}

IEnumerator WaitForConfirmation()
{
        yield return new WaitForSeconds(1.0f);
        while (!Input.anyKeyDown)
        {
                yield return null;
        }
        StartCoroutine(Level.instance.FadeOut(1.5f));
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
}
}
