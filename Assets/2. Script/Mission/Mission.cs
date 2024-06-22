using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public ConnectionCrystal connectionCrystal;
    public CrystalMission crystalMission;
    public Camera missionCamera;
    

    public virtual void StartMission()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        CameraMgr.Instance.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
    public virtual void EndMission()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
        CameraMgr.Instance.gameObject.SetActive(true);
    }

    public virtual void CheckClear()
    {
        Debug.Log("Clear");
        MissionMgr.Instance.UpMissionCount();
        MissionMgr.Instance.missionClearText.gameObject.SetActive(true);
        connectionCrystal.gameObject.SetActive(false);
        EndMission();
    }
}
