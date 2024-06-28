using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ConnectionCrystal : InteractObject
{
    public CrystalMission crystalMission;

    public bool taggerCome;

    public float taggerRange;

    Collider[] taggerInRange;

    public override void Start()
    {
        base.Start();
        MissionMgr.Instance.connectionCrystals.Add(this);
        MissionMgr.Instance.MissionArray();
    }

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, taggerRange);
    }


    [PunRPC]
    public void RPCTaggerRange()
    {
        taggerInRange = Physics.OverlapSphere(transform.position, taggerRange, taggerLayer);

        if (taggerInRange.Length <= 0)
        {
            taggerCome = true;
        }
        else
        {
            taggerCome = false;
        }
    }

    public override IEnumerator CoUpdate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            Range();
            photonView.RPC("RPCTaggerRange", RpcTarget.All);
            CheckLookAt();

            if (enterd && watched)
            {
                text.SetActive(true);
            }
            else
            {
                text.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (GameMgr.Instance.player == null)
            return;
        if (!GameMgr.Instance.player.gameObject.CompareTag("Tagger"))
        {
            if (watched && enterd && Input.GetKeyDown(KeyCode.F))
            {
                Mission mission = MissionMgr.Instance.GetMission(crystalMission, this);
                mission.StartMission();
            }
        }
    }
} 

public enum CrystalMission
{
    Pattern,
    BrickOutMission,
    LinkLine
}
