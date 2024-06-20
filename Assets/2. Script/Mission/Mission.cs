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
}
