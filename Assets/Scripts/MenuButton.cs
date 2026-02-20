using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void BuscarLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
