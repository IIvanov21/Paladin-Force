using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerNetworkManager : NetworkBehaviour
{
    public PlayerStateMachine playerStateMachine;
    public CharacterController characterController;
    public InputReader inputReader;

    //Cameras
    public GameObject mainCamera;
    public GameObject stateDrivenCamera;


    public override void OnNetworkSpawn()
    {
        if(!IsOwner)
        {

            playerStateMachine.enabled = false;
            inputReader.enabled = false;
            mainCamera.SetActive(false);
            stateDrivenCamera.SetActive(false);
        }

        characterController.enabled = true;
    }

    
}
