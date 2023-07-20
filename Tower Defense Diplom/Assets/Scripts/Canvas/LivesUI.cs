using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [Tooltip("Отображение жизней игрока")] public Text livesText;

    private void Update()
    {
        livesText.text = PlayerStats.Lives.ToString() + " LIVES";
    }
}
