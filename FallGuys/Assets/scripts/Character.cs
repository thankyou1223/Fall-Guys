using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviourPun
{
    [SerializeField] Camera virtualCamera;

    void Start()
    {
        DisableCamera();
    }

    void Update()
    {

    }

    void DisableCamera()
    {
        // photonView.IsMine : ���� ��ü�� �� �ڽ��̶��
        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            virtualCamera.gameObject.SetActive(false);
        }
    }
}