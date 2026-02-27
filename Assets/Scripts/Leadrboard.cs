using UnityEngine;
using Unity.Netcode;
using TMPro;
using System.Linq;
using System.Text;
public class Leadrboard : NetworkBehaviour
{
    public TextMeshProUGUI leaderboardText;
    public GameObject leaderboard;
    public void Update()
    {
        leaderboard.SetActive(false);
        if (Input.GetKey(KeyCode.Space))
        { 
            leaderboard.SetActive(true);
            UpdateLeaderboard();
        }
        if (Input.GetKey(KeyCode.C)){
            Debug.Log("Enviando datos al backend");
            SendToBack();
        }
    }

    public void UpdateLeaderboard()
    {
        var players = FindObjectsByType<PlayerStats>(FindObjectsSortMode.None);

        var ordered = players.OrderByDescending(p => p.Score.Value).ToList();

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Leaderboard:");

        for (int i = 0; i < ordered.Count; i++)
        {
            sb.AppendLine($"#{i + 1}Player{ordered[i].OwnerClientId} - {ordered[i].Score.Value}pts");
        }
        leaderboardText.text = sb.ToString();
    }

    void SendToBack()
    {
        var player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerStats>();
        Object.FindAnyObjectByType<BackendManager>().SendScore($"Player{player.OwnerClientId}", player.Score.Value);
    }
}
