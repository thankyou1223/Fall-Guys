using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Information : MonoBehaviourPunCallbacks
{
    [SerializeField] string roomName;

    [SerializeField] Text description;

    public void OnConnectRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void View(string name, int currentPersonnel, int maxPersonnel)
    {
        roomName = name;

        description.text = roomName + " ( " + currentPersonnel + " / " + maxPersonnel + " )";
    }
}