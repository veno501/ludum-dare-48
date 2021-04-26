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
        requiredText.text = "" + (int)samplesRequired;
}

public void CollectSample(float amount)
{
        samplesCollected += amount;
        if (samplesCollected >= samplesRequired)
        {
                samplesCollected = samplesRequired;
                OnEnoughSamplesCollected();
        }
        // update UI
        collectedText.text = "" + (int)samplesCollected;
}

public void ResetCollectedSamples()
{
        samplesCollected = 0f;
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
