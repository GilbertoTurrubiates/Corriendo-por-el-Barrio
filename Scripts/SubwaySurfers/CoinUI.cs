using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public PlayerController player; // Referencia al jugador
    public TMP_Text coinText;       // Referencia al texto TMP

    void Update()
    {
        // Cambiamos esta línea para que SOLO muestre el número
        coinText.text = player.GetCoins().ToString();
    }
}