using UnityEngine;

public class ScriptCamera : MonoBehaviour
{
    public GameObject Ship;
    [SerializeField] float y;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ship = GameObject.FindGameObjectWithTag("vehicle");
        y = Ship.transform.position.y + 3;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, y, Ship.transform.position.z - 10);
    }
}
