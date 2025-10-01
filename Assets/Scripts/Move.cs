using UnityEngine;

public class Move : MonoBehaviour
{
    //** pour les obstacles mouvants (bonhomme de neige); fait faire des aller-retours sur l'axe x  **********
    float minX = -4f;
    float maxX = 8f;
    float vX = 2f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += new Vector3(vX * Time.deltaTime, 0, 0);
        if (transform.position.x < minX || transform.position.x > maxX)
        {
            vX = -vX;
        }
    }
}
