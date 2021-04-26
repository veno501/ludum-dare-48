using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSceneSwitcher : MonoBehaviour
{
public Image fadeUI;
public Text startText;
public float fadeDuration = 0.5f;

public GameObject mainMenu;
public GameObject tutorialLevel;

private bool tutorialStarted = false;
private bool enableStart = false;

void Start()
{
        StartCoroutine(EnableStart());
}

void Update()
{
        if (Input.anyKey && tutorialStarted == false && enableStart)
        {
                tutorialStarted = true;
                StartCoroutine(FadeOut(fadeDuration * 2f));
                Invoke("StartTutorial", fadeDuration * 2f);
        }
}

void StartTutorial()
{
        StartCoroutine(FadeIn(fadeDuration * 2f));
        mainMenu.SetActive(false);
        tutorialLevel.SetActive(true);
}

IEnumerator EnableStart()
{
        yield return new WaitForSeconds(2);
        startText.gameObject.SetActive(true);
        enableStart = true;
}

IEnumerator FadeOut(float _t)
{
        for (float t = _t; t > 0.0f; t -= Time.deltaTime)
        {
                Color c = fadeUI.color;
                c.a = (_t - t) / _t;
                fadeUI.color = c;
                yield return null;
        }
}

IEnumerator FadeIn(float _t)
{
        for (float t = _t; t > 0.0f; t -= Time.deltaTime)
        {
                Color c = fadeUI.color;
                c.a = t / _t;
                fadeUI.color = c;
                yield return null;
        }
}
}
