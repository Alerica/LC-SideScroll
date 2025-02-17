using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering;

public class HealthPickup : MonoBehaviour
{
    public int healthtRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    AudioSource pickUpSource;

    void Awake()
    {
        pickUpSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable= collision.GetComponent<Damageable>();

        if (damageable)
        {
            bool wasHealed = damageable.Heal(healthtRestore);
            if(wasHealed)
            {
                if(pickUpSource)
                    AudioSource.PlayClipAtPoint(pickUpSource.clip, gameObject.transform.position, pickUpSource.volume);
                Destroy(gameObject);
            }
        }
    }

    private void ObjectRotation() {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

    void Update()
    {
        ObjectRotation();
    }
}
