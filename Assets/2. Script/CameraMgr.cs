using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviourPunCallbacks
{
    public static CameraMgr Instance;
    Player playerTargetTr;

    public Transform xRotationTr;
    public void Awake()
    {
        Instance = this;
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

        xRotationTr.localEulerAngles = new Vector3(playerTargetTr.rotationX, 0, 0);
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
