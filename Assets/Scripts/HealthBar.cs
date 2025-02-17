using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;
    Damageable playerDamageable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            Debug.LogWarning("No player found. Please put a tag 'Player'");
        }
        playerDamageable = player.GetComponent<Damageable>();
    }
    void Start()
    {
        
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text ="HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private float CalculateSliderPercentage(float currentHealth, float MaxHealth)
    {
        return currentHealth / MaxHealth;
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text ="HP " + newHealth + " / " + maxHealth;
    }

    
}
