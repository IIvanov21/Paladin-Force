using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    /* private float horizontal, vertical;
     public float playerSpeed;

     // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
     {

     }

     // Update is called once per frame
     void Update()
     {
         if (!IsOwner) return;

         horizontal = Input.GetAxis("Horizontal");
         vertical = Input.GetAxis("Vertical");

         Vector3 move = new Vector3(horizontal, 0, vertical);

         transform.Translate(move * Time.deltaTime * playerSpeed);

         if (IsClient && Input.GetKeyDown(KeyCode.P))
         {
             PingRpc(5);
         }
     }

     //Server RPC function which gets called by our Client and gets executed on the server
     [Rpc(SendTo.Server)]
     public void PingRpc(int pingCount)
     {
         PongRPC(pingCount, "PONG!");
     }

     //Client RPC function gets called by our Server and gets executed on all running clients
     [Rpc(SendTo.NotServer)]
     public void PongRPC(int pingCount, string message)
     {
         Debug.Log($"Received pong from server for ping {pingCount} and message {message}");

     }*/

    public float playerSpeed = 5f;

    private Vector2 inputDirection;

    private void Update()
    {
        if(IsOwner)
        {
            //capturing the input from the client and sending to the server
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 input= new Vector2(horizontal, vertical);

            SendInputServerRpc(input);
        }

        if(IsServer && IsSpawned && inputDirection != Vector2.zero)
        {
            //We execute the movement on server side as usual
            //We dont need to worry about calling a client rpc because the Network Object is responsible for syncing the transfrom properties
            Vector3 move=new Vector3(inputDirection.x,0,inputDirection.y);
            transform.position += move * playerSpeed * Time.deltaTime;
        }
    }

    [Rpc(SendTo.Server)]
    private void SendInputServerRpc(Vector2 input)
    {
        inputDirection = input;
    }
}
