using UnityEngine;
using TMPro;

public class UImanager : MonoBehaviour
{
   public static UImanager instance;

    [SerializeField]
    private TextMeshProUGUI _texto;
     
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void ClearMessage()
    {
        _texto.text = "";
    } 

    public void ShowMessage(string message){
        _texto.text = message;
        CancelInvoke();
        Invoke("ClearMessage", 3f);
    }
}
