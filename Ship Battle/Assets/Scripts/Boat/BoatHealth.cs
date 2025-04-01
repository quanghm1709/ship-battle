using UnityEngine;
using UnityEngine.UI;

public class BoatHealth : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;

    [SerializeField] Slider healthSlider;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealth(0);
    }

    public void UpdateHealth(int value)
    {
        currentHealth += value;

        if(currentHealth <= 0)
        {
            if(GetComponent<BoatAI>() != null)
                GetComponent<BoatAI>().DisableBoat();
            else
            {
                FindFirstObjectByType<GameplayUI>().GameOver();
            }
                
        }


        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
