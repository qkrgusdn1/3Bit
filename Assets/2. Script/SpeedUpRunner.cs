using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpRunner : MonoBehaviour
{
    public float destroyTime;
    private int shooterID;

    public void SetShooterID(int id)
    {
        shooterID = id;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player.CompareTag("Tagger"))
        {
            player.moveSpeed = 1;
        }
        else
        {
            player.moveSpeed = 2 * player.moveSpeed;
        }

        StartCoroutine(CoDestroy(player));
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        player.moveSpeed = player.maxMoveSpeed;
    }

    private IEnumerator CoDestroy(Player player)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(player);
    }

    void Destroy(Player player)
    {
        player.moveSpeed = player.maxMoveSpeed;
        Destroy(gameObject);
    }
}
