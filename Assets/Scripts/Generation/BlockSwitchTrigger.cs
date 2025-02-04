﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSwitchTrigger : MonoBehaviour
{
public char direction;
public bool isEnabled = false;
// public Block link;

void OnTriggerEnter2D(Collider2D hit)
{
        if (!isEnabled) return;
        // isEnabled = false;

        if (hit.GetComponentInParent<Player>()) {
                Debug.Log("Switching block \'" + direction + "\'");

                // Level.instance.SwitchBlock(Level.instance.currentBlock.block);
                if (direction == 'l') {
                        Level.instance.SwitchBlock(Level.instance.currentBlock.blockLeft, direction);
                }
                else if (direction == 'r') {
                        Level.instance.SwitchBlock(Level.instance.currentBlock.blockRight, direction);
                }
                else if (direction == 'd') {
                        if (Player.instance.stats.samplesCollected >= Player.instance.stats.samplesRequired) {
                                if (GameObject.Find("TutorialLevel") && TutorialLevel.tutorialFinished == false)
                                {
                                        TutorialLevel.tutorialFinished = true;
                                        // switch scene
                                        StartCoroutine(TutorialSceneSwitcher.LoadMainScene());
                                }
                                else
                                {
                                        Level.instance.SwitchLayer();
                                }
                        }
                        else {
                                OnInsufficientSamples();
                                return;
                        }
                }
                else {
                        return;
                }

                isEnabled = false;
        }
}

void OnInsufficientSamples()
{
        Debug.Log("Collect more samples before exploring deeper!");
}
}
