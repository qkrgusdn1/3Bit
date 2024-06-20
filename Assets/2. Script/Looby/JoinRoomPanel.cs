using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoomPanel : MonoBehaviour
{
    public RoomJoinButton joinRoomJoinButton;

    


    public RectTransform roomGroupTr;
    public void ListUpRoom(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            RoomJoinButton button = Instantiate(joinRoomJoinButton, roomGroupTr);
            button.SetRoom(roomList[i]);
        }

    }
}
