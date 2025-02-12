using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    public UnityEvent NoColliderRemains;
    Collider2D collision;
    
    void Awake()
    {
        collision = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        detectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        detectedColliders.Remove(collision);
        if(detectedColliders.Count <= 0)
        {
            NoColliderRemains.Invoke();
        }
    }
}
