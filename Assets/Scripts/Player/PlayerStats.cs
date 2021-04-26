using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : PlayerModule
{
public float samplesRequired = 5f;
public float samplesCollected = 0f;
public Text collectedText;
public Text requiredText;

protected override void Awake()
{
        base.Awake();
        requiredText.text = "" + samplesRequired;
}

public void CollectSample(float amount)
{
        samplesCollected += amount;
        if (samplesCollected >= samplesRequired/2)
        {
                samplesCollected = 0f;
                OnEnoughSamplesCollected();
        }
        // update UI
        collectedText.text = "" + (int)samplesCollected;
}

void OnEnoughSamplesCollected()
{
        GameObject tutorialLeveL = GameObject.Find("TutorialLevel");
        if (tutorialLeveL != null)
        {
                tutorialLeveL.GetComponent<TutorialLevel>().EnableDeeperText();
        }
}
}
