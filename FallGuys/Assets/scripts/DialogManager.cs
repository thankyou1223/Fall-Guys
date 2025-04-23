using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogManager : MonoBehaviourPunCallbacks
{

    [SerializeField] InputField inputField;
    [SerializeField] Transform parentTransform;
    [SerializeField] ScrollRect scrollRect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (photonView.IsMine == false) return;

            if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<UnityEngine.UI.InputField>() != null)
            {
                return;
            }

            inputField.ActivateInputField();

            if(inputField.text.Length <= 0)
            {
                return;

            }

            string talk = inputField.text;

            photonView.RPC("Talk", RpcTarget.All, talk);
        }
    }

    [PunRPC]

    void Talk(string message)
    {
        //
        GameObject talk = Instantiate(Resources.Load<GameObject>("Talk"));

        //
        talk.GetComponent<Text>().text = message;

        //
        talk.transform.SetParent(parentTransform);

        //
        inputField.ActivateInputField();

        //
        scrollRect.verticalNormalizedPosition = 0.0f;

        //
        Canvas.ForceUpdateCanvases();

        //
        inputField . text = "";
    }

}

