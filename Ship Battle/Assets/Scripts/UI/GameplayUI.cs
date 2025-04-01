using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] GameObject overPanel;

    public void GameOver()
    {
        overPanel.SetActive(true);
    }
}
