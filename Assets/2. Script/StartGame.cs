using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartGame : MonoBehaviourPunCallbacks
{
    public TMP_Text countTxt;

    float count;
    public float maxCount;
    float currentCount;

    public GameObject Barrier;

    public AudioSource music;

    public List<string> powers = new List<string>();

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("Player entered room");

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPCCurrentCount", newPlayer, currentCount);

            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                photonView.RPC("RPCCountDown", RpcTarget.All);
                Debug.Log("LogLog");
            }
        }
    }

    //private void Start()
    //{
    //    photonView.RPC("RPCCountDown", RpcTarget.All);
    //}


    IEnumerator CountDown()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            count = maxCount;
        }
        else
        {
            count = currentCount;
        }

        while (true)
        {
            yield return null;
            if (count >= 0)
            {
                count -= Time.deltaTime;
                currentCount = count;
                photonView.RPC("RPCCurrentCount", RpcTarget.All, currentCount);

                countTxt.text = count.ToString("F0");
            }
            else
            {
                countTxt.gameObject.SetActive(false);
                if (PhotonNetwork.IsMasterClient)
                {
                    RandomPowers();
                    CountingPlayerPowers();
                }
                music.gameObject.SetActive(false);
                break;

            }
        }

    }

    [PunRPC]
    public void RPCCurrentCount(float value)
    {
        currentCount = value;
    }

    //private void FindAllPlayers()
    //{
    //    players.Clear();
    //    Player[] foundPlayers = FindObjectsOfType<Player>();
    //    players.AddRange(foundPlayers);
    //}
    public void RandomPowers()
    {
        for (int i = powers.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            string changePower = powers[i];
            powers[i] = powers[randomIndex];
            powers[randomIndex] = changePower;
        }
    }

    [PunRPC]
    public void RPCCountDown()
    {
        Debug.Log("Log");
        StartCoroutine(CountDown());
    }
    public void CountingPlayerPowers()
    {
        Player[] players = FindObjectsOfType<Player>();
        for (int i = 0; i < players.Length; i++)
        {
            if (i < powers.Count)
            {
                players[i].SetPower(powers[i]);
            }
        }
    }
}
