using Photon.Pun;
using Photon.Realtime;
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
        if (GameMgr.Instance.player != null)
            GameMgr.Instance.player.canvas.SetActive(false);
    }
    public virtual void EndMission()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CameraMgr.Instance.gameObject.SetActive(true);
        GameMgr.Instance.player.canvas.SetActive(true);
        if (MissionMgr.Instance.missionCountBar.fillAmount >= 1)
        {
            GameMgr.Instance.connection.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    public virtual void CheckClear()
    {
        Debug.Log("Clear");
        MissionMgr.Instance.UpMissionCount();
        MissionMgr.Instance.clearSound.Play();
        MissionMgr.Instance.missionClearText.gameObject.SetActive(true);
        connectionCrystal.gameObject.SetActive(false);
        EndMission();
    }
}
