using UnityEngine;

public class ScriptBehind : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        Destroy(other);
        Debug.Log("destroy object " + other.name);
    }
}
