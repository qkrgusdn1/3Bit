using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoomPanel : MonoBehaviourPunCallbacks
{
    public RoomJoinButton joinRoomJoinButton;

    public RectTransform roomGroupTr;

    public override void OnEnable()
    {
        base.OnEnable();
        if (PhotonMgr.Instance.roomList != null && PhotonMgr.Instance.roomList.Count > 0)
        {
            ListUpRoom(PhotonMgr.Instance.roomList);
        }
    }
    public void ListUpRoom(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            RoomJoinButton button = Instantiate(joinRoomJoinButton, roomGroupTr);
            button.SetRoom(roomList[i]);
        }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("JoinRoomPanel OnRoomListUpdate");
        ListUpRoom(roomList);
    }
}
