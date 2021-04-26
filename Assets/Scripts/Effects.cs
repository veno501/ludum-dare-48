using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public static Effects instance;
    [SerializeField]
    GameObject smallExplosion;

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
}
