using TMPro;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobby : NetworkBehaviour
{
    public Button hostButton;
    public Button clientButton;
    public Button startGameButton;
    public TMP_Text playerText;

    private void Start()
    {
        hostButton.onClick.AddListener(StartHost);
        clientButton.onClick.AddListener(StartClient);
        startGameButton.onClick.AddListener(StartGame);

        startGameButton.gameObject.SetActive(false);
    }

    void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        hostButton.gameObject.SetActive(false);
        clientButton.gameObject.SetActive(false);
    }

    void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        hostButton.gameObject.SetActive(false);
        clientButton.gameObject.SetActive(false);
    }

    void StartGame()
    {
        if (!IsHost)return;
        NetworkManager.Singleton.SceneManager.LoadScene("Game",LoadSceneMode.Single);
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += UpdatePlayerCount;
            NetworkManager.Singleton.OnClientDisconnectCallback += UpdatePlayerCount;

            UpdatePlayerCount(0);
        }
    }

    private void OnEnable()
    {
        if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer)
        {
           int count = NetworkManager.Singleton.ConnectedClients.Count;
              UpdatePlayerCountClientRpc(count);
        }
        NetworkManager.Singleton.SceneManager.OnLoadComplete += OnSceneLoaded;
    }

    void UpdatePlayerCount(ulong clientId)
    {
        if(!IsServer)return;
        int count = NetworkManager.Singleton.ConnectedClients.Count;
        UpdatePlayerCountClientRpc(count);
    }
    [ClientRpc]

    void UpdatePlayerCountClientRpc(int count)
    {
        playerText.text = $"Players Connected: {count}";
        startGameButton.gameObject.SetActive(IsHost);
    }

    private void OnSceneLoaded(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        if (sceneName == "GameScene")
        {
            UpdatePlayerCount(0);
        }
    }
}
