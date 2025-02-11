using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 75, 0);
    public float timeToFade = 1f;
    private float timeElapsed;
    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;
    private Color startColor;
    void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro =   GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }
    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;
        if(timeElapsed < timeToFade)
        {
            float fadeAlpha = startColor.a * (1 - timeElapsed / timeToFade);
            textMeshPro.color = new Color(startColor.r, startColor.b, startColor.g, fadeAlpha);
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}
