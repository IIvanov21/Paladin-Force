using System;
using Unity.Collections;
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

    private int playerScore = 0;

    public GameObject spawnedObject;

    NetworkVariable<int> score = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public struct PlayerCustomData : INetworkSerializable
    {
        public ulong id;
        public float health;
        public float mana;
        public FixedString32Bytes name;//When we want to use strings, don't use the direct string class, instead use FixedString32Bytes.

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref id);
            serializer.SerializeValue(ref health);
            serializer.SerializeValue(ref mana);
            serializer.SerializeValue(ref name);
        }
    }

    //private NetworkVariable<PlayerCustomData> data;

    public override void OnNetworkSpawn()//OnEnable
    {
        if(IsOwner)
        {
            //data.Value = new PlayerCustomData { health = 10, mana = 100, id = OwnerClientId, name = $"Player{OwnerClientId}" };
        }
        score.OnValueChanged += DisplayScore;

    }

    public override void OnNetworkDespawn()//OnDisable
    {
        score.OnValueChanged -= DisplayScore;
    }

    

    private void Update()
    {
        if(IsOwner)
        {
            //capturing the input from the client and sending to the server
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 input= new Vector2(horizontal, vertical);
            if (Input.GetKeyDown(KeyCode.P)) SendInfoRpc(UnityEngine.Random.Range(5,100));
            SendInputServerRpc(input);

            if (Input.GetKeyDown(KeyCode.O))
            {
                spawnedObject=Instantiate(spawnedObject);
                SpawnObjectRpc();
            }
            if(Input.GetKeyDown(KeyCode.I))
            {
                spawnedObject.GetComponent<NetworkObject>().Despawn();
                Destroy(spawnedObject);
            }


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

    [Rpc(SendTo.Server)]
    private void SendInfoRpc(int incomingScore)
    {
        score.Value = incomingScore;
       /* var newData = data.Value;
        newData.health -= 10;
        newData.mana -= 10;
        data.Value = newData;*/
        
    }

    [Rpc(SendTo.Server)]
    void SpawnObjectRpc()
    {
        spawnedObject.GetComponent<NetworkObject>().Spawn();
    }

    private void DisplayScore(int previousVal, int newVal)
    {
        Debug.Log($"The player's score has changed from {previousVal} to {newVal}");
    }

    private void DisplayData(PlayerCustomData previousVal, PlayerCustomData newVal)
    {
        Debug.Log($"The player's healt has changed from {previousVal.health} to {newVal.health}");
    }
}
