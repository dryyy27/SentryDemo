using System.Collections;
using System.Collections.Generic;
using Sofunny.BiuBiuBiu2.Util;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SentrySDKAgent.Instance.Init("myTest", "test", 1);
        Debug.LogError("[ydr] test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
