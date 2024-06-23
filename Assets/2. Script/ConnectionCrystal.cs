using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ConnectionCrystal : InteractObject
{
    public CrystalMission crystalMission;

    public override void Start()
    {
        base.Start();
        MissionMgr.Instance.connectionCrystals.Add(this);
        MissionMgr.Instance.MissionArray();
    }

    private void Update()
    {
        if(watched && enterd && Input.GetKeyDown(KeyCode.F))
        {
            Mission mission = MissionMgr.Instance.GetMission(crystalMission, this);
            mission.StartMission();
        }
    }
} 

public enum CrystalMission
{
    Pattern,
    BrickOutMission,
    LinkLine
}
