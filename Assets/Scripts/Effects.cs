using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public static Effects instance;
    [SerializeField]
    GameObject smallExplosion;
    [SerializeField]
    GameObject verySmallExplosion;
    [SerializeField]
    GameObject smallerExplosion;
    // public List<AudioClip> clips = new List<AudioClip>();
    [SerializeField]
    AudioClip takeHitSound;
    [SerializeField]
    AudioClip sampleHitSound;

    void Awake()
    {
        instance = this;
    }

    public GameObject SmallExplosion(Vector3 position)
    {
        Quaternion rot = Quaternion.Euler(0,0,(int)(Random.Range(0,360)/90)*90);
        GameObject ob = Instantiate(smallExplosion, position, rot) as GameObject;

        Destroy(ob, 2f);
        return ob;
    }

    public GameObject SmallerExplosion(Vector3 position)
    {
        Quaternion rot = Quaternion.Euler(0,0,(int)(Random.Range(0,360)/90)*90);
        GameObject ob = Instantiate(smallerExplosion, position, rot) as GameObject;

        Destroy(ob, 2f);
        return ob;
    }

    public GameObject VerySmallExplosion(Vector3 position)
    {
        Quaternion rot = Quaternion.Euler(0,0,(int)(Random.Range(0,360)/90)*90);
        GameObject ob = Instantiate(verySmallExplosion, position, rot) as GameObject;

        Destroy(ob, 2f);
        return ob;
    }

    public void TakeHitSound()
    {
        PlayAudio(takeHitSound, (Vector2)Camera.main.transform.position, 1f);
    }

    public void SampleHitSound()
    {
        PlayAudio(sampleHitSound, (Vector2)Camera.main.transform.position, 0.2f);
    }

    public static void PlayAudio (AudioClip clip)
	{
		PlayAudio(clip, Vector2.zero, 1f);
	}

	public static void PlayAudio (AudioClip clip, Vector2 position, float volume)
	{
		GameObject ob = new GameObject("Audio Clip");
		AudioSource source = ob.AddComponent<AudioSource>();
		source.clip = clip;

		ob.transform.position = position;
		source.volume = volume;
        source.Play();
        Destroy(ob, 2f);
	}
}
