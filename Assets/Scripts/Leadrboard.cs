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

}
