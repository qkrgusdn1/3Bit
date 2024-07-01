using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearMgr : MonoBehaviourPunCallbacks
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
    public override void OnLeftRoom()
    {
        Debug.Log("LeftRoom ¹æ¶°³²");
        PhotonNetwork.LoadLevel("SampleScene");

    }
    public void OnClickedLobbyBtn()
    {
        PhotonNetwork.LeaveRoom();

    }
}
