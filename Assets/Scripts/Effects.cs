using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public static Effects instance;
    public GameObject explosion;

    void Awake()
    {
        instance = this;
    }
}
