using TMPro;
using UnityEngine;

public class ScriptFrozen : MonoBehaviour
{
    public bool frozen = false;
    public float timer = 1;
    public float elapsedTime = 0f;
    public int chargesFrozen = 3;
    public TMP_Text chargesLeft;
   // 3 activations
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.F)) && (frozen == false))
        {
            ActivateFrost();
        }
        if (frozen && (elapsedTime < timer))
        {
            elapsedTime += Time.deltaTime;
        }
        else if (frozen)
        {
            frozen = false;
            elapsedTime = 0;
        }
        gameObject.GetComponentInParent<ShipController>().isFrozen = frozen;
        chargesLeft.text = "FROST " + chargesFrozen.ToString();

    }
    /*void OnTriggerStay(Collider other)
    {
        gameObject.GetComponentInParent<ShipController>().isFrozen = true;
    }*/

    public void ActivateFrost()
    {
        if (chargesFrozen > 0)
        {
            frozen = true;
            elapsedTime = 0;
            chargesFrozen -= 1;
            Debug.Log($"Frost activated, charges {chargesFrozen}");            
        }
        else Debug.Log("Frost is discharged");
        gameObject.GetComponentInParent<ShipController>().isFrozen = frozen;
    }
}
