using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialSceneSwitcher : MonoBehaviour
{
public Image fadeUI;
public Text startText;
public float fadeDuration = 0.5f;

public GameObject mainMenu;
public GameObject tutorialLevel;

private bool tutorialStarted = false;
private bool enableStart = false;

public static TutorialSceneSwitcher instance;

void Start()
{
        instance = this;
        StartCoroutine(EnableStartText());
}

void Update()
{
        if (Input.anyKey && tutorialStarted == false && enableStart)
        {
                tutorialStarted = true;
                StartCoroutine(FadeOut(fadeDuration));
                Invoke("StartTutorial", fadeDuration);
        }
}

void StartTutorial()
{
        StartCoroutine(FadeIn(fadeDuration));
        mainMenu.SetActive(false);
        tutorialLevel.SetActive(true);
}

IEnumerator EnableStartText()
{
        yield return new WaitForSeconds(2);
        startText.gameObject.SetActive(true);
        enableStart = true;
}

public static IEnumerator LoadMainScene()
{
        instance.StartCoroutine(instance.FadeOut(1.5f));
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
}

public IEnumerator FadeOut(float _t)
{
        for (float t = _t; t > 0.0f; t -= Time.deltaTime)
        {
                Color c = fadeUI.color;
                c.a = (_t - t) / _t;
                fadeUI.color = c;
                yield return null;
        }
}

public IEnumerator FadeIn(float _t)
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
