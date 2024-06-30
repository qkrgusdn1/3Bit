using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameMgr : MonoBehaviourPunCallbacks
{
    private static GameMgr instance;
    public static GameMgr Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;
    }
    public GameObject escPanel;
    public GameObject settingPanel;

    public List<Player> players = new List<Player>();
    public Player player;
    public GameObject diePanel;
    public GameObject connection;
    public GameObject lobbyLodingPanel;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
        StartCoroutine(CoUpdate());
    }
    public void MoveClearScenes()
    {
        photonView.RPC("RPCMoveClearScenes", RpcTarget.All);
    }

    [PunRPC]
    public void RPCMoveClearScenes()
    {
        PhotonNetwork.LoadLevel("ClearScenes");
    }
    public void AddPlayer()
    {
        for (int i = 0; i < players.Count; i++)
        {
            Player player = players[i];
            if (player.photonView.IsMine)
            {
                this.player = player;
                break;
            }
        }
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("MultiGameMgr 현재 방의 접속한 플레이어 수 확인 {PhotonNetwork.PlayerList.Length}");
        }
    }

    public override void OnLeftRoom()
    {
        Debug.Log("LeftRoom 방떠남");
        lobbyLodingPanel.SetActive(true);
        PhotonNetwork.LoadLevel("SampleScene");
    }

    public void OnClickedLobbyBtn()
    {
        SoundMgr.Instance.lobbyMusic.gameObject.SetActive(true);
        SoundMgr.Instance.inGameMusic.gameObject.SetActive(false);
        for (int i = 0; i < SoundMgr.Instance.attackSounds.Count; i++)
        {
            SoundMgr.Instance.attackSounds[i].gameObject.SetActive(false);
        }

        PhotonNetwork.LeaveRoom();
    }

    public IEnumerator CoUpdate()
    {

        while (true)
        {
            for (int i = players.Count - 1; i >= 0; i--)
            {
                if (players[i] == null)
                {
                    players.RemoveAt(i);
                }
            }
            yield return new WaitForSeconds(1);
        }

    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && !player.esc)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player.esc = true;
            escPanel.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && player.esc)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.esc = false;
            settingPanel.SetActive(false);
            escPanel.SetActive(false);
        }
    }



}
