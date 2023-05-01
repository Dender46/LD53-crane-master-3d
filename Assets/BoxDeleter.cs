using UnityEngine;

public class BoxDeleter : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeliveryBox")
        {
            Destroy(other.gameObject);
        }
    }
}
