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
    public List<AudioClip> clips = new List<AudioClip>();

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

    public GameObject VerySmallExplosion(Vector3 position)
    {
        Quaternion rot = Quaternion.Euler(0,0,(int)(Random.Range(0,360)/90)*90);
        GameObject ob = Instantiate(verySmallExplosion, position, rot) as GameObject;

        Destroy(ob, 2f);
        return ob;
    }

    public GameObject PlayClip()
    {
        return null;
    }
}
