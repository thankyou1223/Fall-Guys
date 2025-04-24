using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform parentPosition;

    private Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();

    public override void OnConnectedToMaster()
    {
        // JoinLobby() : Ư�� �κ� �����Ͽ� �����ϴ� �Լ�
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void OnCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();

        // �ο� : 2 ~ 4�� ���� �� �ְ� �����մϴ�.
        roomOptions.MaxPlayers = Random.Range(2, 5);

        // ���� : ���� ������ �� �ֵ��� �����մϴ�.
        roomOptions.IsOpen = true;

        // Ȱ��ȭ : ���� ���� �� �ֵ��� �����մϴ�.
        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom("Room", roomOptions);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject prefab = null;

        foreach (RoomInfo roomInfo in roomList)
        {
            // ���� ������ ��� 
            if (roomInfo.RemovedFromList == true)
            {
                dictionary.TryGetValue(roomInfo.Name, out prefab);

                Destroy(prefab);

                dictionary.Remove(roomInfo.Name);
            }
            else // ���� ������ ����Ǵ� ���
            {
                // ���� ó�� �����Ǵ� ���
                if (dictionary.ContainsKey(roomInfo.Name) == false)
                {
                    GameObject clone = Instantiate(Resources.Load<GameObject>("Room"), parentPosition);

                    clone.GetComponent<Information>().View(roomInfo.Name, roomInfo.PlayerCount, roomInfo.MaxPlayers);

                    dictionary.Add(roomInfo.Name, clone);
                }
                else // ���� ������ ����Ǵ� ���
                {
                    dictionary.TryGetValue(roomInfo.Name, out prefab);

                    prefab.GetComponent<Information>().View(roomInfo.Name, roomInfo.PlayerCount, roomInfo.MaxPlayers);
                }
            }

        }
    }
}

