using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearMgr : MonoBehaviour
{
    private static ClearMgr instance;
    public static ClearMgr Instance
    {
        get { return instance; }
    }

    public bool win;
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


}
