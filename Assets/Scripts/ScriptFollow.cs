using UnityEngine;

public class ScriptF : MonoBehaviour
{
    GameObject target;
    Vector3 position;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("vehicle")[0];
        Debug.Log("found target " + target.name);
        position = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0f, 2f,target.transform.position.z-10);
    }
}
