﻿using System.Collections;
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
public Animator playerStartAnimation;
public GameObject tryAgainWreck;

private static bool tutorialStarted = false;
private bool enableStart = false;

public static TutorialSceneSwitcher instance;

void Start()
{
        instance = this;

        if (tutorialStarted) {
                StartCoroutine(EnableTryAgain());
        }
        else {
                StartCoroutine(EnableStartText());
        }
}

void Update()
{
        if (Input.anyKeyDown && tutorialStarted == false && enableStart)
        {
                tutorialStarted = true;
                StartCoroutine(StartTutorialAnimation());
        }
}

IEnumerator EnableTryAgain()
{
        StartCoroutine(FadeIn(fadeDuration));

        tryAgainWreck.SetActive(true);
        startText.gameObject.SetActive(true);
        startText.text = "Press a key to try again";
        
        while (!Input.anyKeyDown)
        {
                yield return null;
        }
        
        playerStartAnimation.enabled = true;
        yield return new WaitForSeconds(2f);

        StartCoroutine(LoadMainScene());
}

IEnumerator StartTutorialAnimation()
{
        playerStartAnimation.enabled = true;
        yield return new WaitForSeconds(2f);
        
        StartCoroutine(FadeOut(fadeDuration));
        yield return new WaitForSeconds(fadeDuration);
        StartTutorial();
}

void StartTutorial()
{
        StartCoroutine(FadeIn(fadeDuration));
        mainMenu.SetActive(false);
        tutorialLevel.SetActive(true);
}

IEnumerator EnableStartText()
{
        StartCoroutine(FadeIn(fadeDuration));
        yield return new WaitForSeconds(4.5f);
        startText.gameObject.SetActive(true);
        enableStart = true;
}

public static IEnumerator LoadMainScene()
{
        instance.StartCoroutine(instance.FadeOut(1.0f));
        yield return new WaitForSeconds(1.0f);
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
        Color c1 = fadeUI.color;
        c1.a = 1.0f;
        fadeUI.color = c1;
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
        Color c1 = fadeUI.color;
        c1.a = 0.0f;
        fadeUI.color = c1;
}

public void QuitGame()
{
        Application.Quit();
}
}
