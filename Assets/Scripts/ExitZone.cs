using Unity.VisualScripting;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        
    }
}
