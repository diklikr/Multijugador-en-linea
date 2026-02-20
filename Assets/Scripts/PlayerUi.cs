using Unity.Netcode;
using UnityEngine;

public class PlayerUi : NetworkBehaviour
{
    public PlayerStats playerStats;
    public TMPro.TextMeshProUGUI scoreText;

    private void Start()
    {
        playerStats = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerStats>();
        scoreText.text = "Score: " + playerStats.Score.Value;
    }

    public override void OnNetworkSpawn()
    {

        playerStats = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerStats>();
     
        playerStats.Score.OnValueChanged += OnScoreChanged;
        
    }
    private void OnScoreChanged(int oldValue, int newValue)
    {
        scoreText.text = "Score: " + newValue;
    }

}
