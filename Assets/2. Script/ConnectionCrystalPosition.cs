using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

public class ConnectionCrystalPosition : MonoBehaviourPunCallbacks
{
    public InteractObject crystalPrefab;
    public List<Transform> crystalPositions = new List<Transform>();
    public const int CRYSTAL_SPAWN_COUNT = 3;

    void Start()
    {
        crystalPositions = GetComponentsInChildren<Transform>().ToList();
        // ù ��°�� �ڽ��� Transform�̹Ƿ� ����
        crystalPositions.RemoveAt(0);
        //�׽�Ʈ �Ҷ���
        StartGame();
    }

    public void StartGame()
    {
        photonView.RPC("RPCStartGame", RpcTarget.All);
    }

    [PunRPC]
    public void RPCStartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = crystalPositions.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                Transform change = crystalPositions[i];
                crystalPositions[i] = crystalPositions[randomIndex];
                crystalPositions[randomIndex] = change;
            }

            for(int i = 0; i < CRYSTAL_SPAWN_COUNT; i++)
            {
                InteractObject crystal = Instantiate(crystalPrefab, crystalPositions[i]);
                crystal.transform.position = crystalPositions[i].position;
            }
        }
    }

}
