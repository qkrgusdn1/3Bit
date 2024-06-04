using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public CrystalMission crystalMission;
    public new Camera camera;

    public virtual void StartMission()
    {
        CameraMgr.Instance.gameObject.SetActive(false);
        camera.gameObject.SetActive(true);
    }
    public virtual void EndMission()
    {
        camera.gameObject.SetActive(false);
        CameraMgr.Instance.gameObject.SetActive(true);
    }
}
