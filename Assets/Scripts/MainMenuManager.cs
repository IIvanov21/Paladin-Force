using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public string sceneName;
    public GameObject playerPrefab;
    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        if(NetworkManager.Singleton != null) NetworkManager.Singleton.SceneManager.OnLoadComplete -= OnSceneLoadComplete;
    }
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();//Always start Host first before anything e.g. establish server before everything else!

        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.SceneManager.OnLoadComplete += OnSceneLoadComplete;
        }

        if (NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();//Establish our connection to server
        if (NetworkManager.Singleton != null) NetworkManager.Singleton.SceneManager.OnLoadComplete += OnSceneLoadComplete;

    }

    private void OnSceneLoadComplete(ulong clientId, string sceneName, LoadSceneMode mode)
    {
        if (!NetworkManager.Singleton.IsServer) return;

        Debug.Log($"[Server] Scene load complete for client {clientId} in scene '{sceneName}'");


        GameObject player = Instantiate(playerPrefab, GetSpawnPoint(clientId), Quaternion.identity);
            player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
    }

    private Vector3 GetSpawnPoint(ulong clientId)
    {
        int index = (int)(clientId % 4);
        float spacing = 3.0f;
        return new Vector3(index * spacing, 1.0f, 0.0f);
    }

}
