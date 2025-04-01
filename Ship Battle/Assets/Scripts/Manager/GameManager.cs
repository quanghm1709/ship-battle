using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool IsPlaying = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        IsPlaying = true;
    }
}
