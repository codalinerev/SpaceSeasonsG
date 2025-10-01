
using UnityEngine;

public class Fall : MonoBehaviour
{
    // utilisé pour faire tomber les flocons de neige; gravity fait-maison
    float minY = 0.5f;
    // test for version control
    float maxY = 10f;
    float vY = -3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, vY * Time.deltaTime, 0);
        if (transform.position.y < minY)
        {
            transform.position += new Vector3(0, maxY, 0); // respawn
        }
        
    }
}
