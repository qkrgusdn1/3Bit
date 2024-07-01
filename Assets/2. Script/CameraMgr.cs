using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviourPunCallbacks
{
    Player playerTargetTr;

    public Transform xRotationTr1;
    public Transform xRotationTr3;
    private static CameraMgr instance;
    public static CameraMgr Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;
    }

    public void SetTarget(Player player)
    {
        playerTargetTr = player;
    }

    private void Update()
    {
        if (playerTargetTr == null)
            return;

        transform.position = playerTargetTr.transform.position;

        //transform.forward = targetTr.forward;�� ���� ��� ����������
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTargetTr.bodyTr.forward), Time.deltaTime * 5);

        xRotationTr1.localEulerAngles = new Vector3(playerTargetTr.rotationX, 0, 0);
        xRotationTr3.localEulerAngles = new Vector3(playerTargetTr.rotationX, 0, 0);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        //������ Ŭ���̾�Ʈ���� Ȯ�� == ���� ���� �÷��̾�
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("GameMgr ���� ���� ������ �÷��̾� �� Ȯ�� {PhotonNetwork.PlayerList.Length}");
        }
    }
}
