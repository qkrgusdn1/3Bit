using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionMgr : MonoBehaviour
{
    public static MissionMgr Instance;
    public Mission[] missions;

    public TMP_Text missionCountText;

    public List<ConnectionCrystal> connectionCrystals = new List<ConnectionCrystal>();

    public List<CrystalMission> crystalMissions = new List<CrystalMission>();

    public void Awake()
    {
        Instance = this;
    }

    public void MissionArray()
    {
        if (connectionCrystals.Count == crystalMissions.Count)
        {
            for (int i = 0; i < crystalMissions.Count; i++)
            {
                connectionCrystals[i].crystalMission = crystalMissions[i];
                
            }
        }
        
    }

    public Mission GetMission(CrystalMission crystalMission, ConnectionCrystal connectionCrystalGameObj)
    {
        for (int i = 0; i < missions.Length; i++)
        {
            if (missions[i].crystalMission == crystalMission)
            {
                missions[i].connectionCrystal = connectionCrystalGameObj;
                return missions[i];
            }
        }
        return null;
    }
}

