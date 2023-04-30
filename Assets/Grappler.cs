using UnityEngine;

public class Grappler : MonoBehaviour
{
    GameObject _MagnetizedBox = null;

    void Start()
    {

    }

    void Update()
    {
        if (_MagnetizedBox)
        {
            _MagnetizedBox.transform.position = transform.position;
            _MagnetizedBox.transform.rotation = transform.rotation;

            var boxRB = _MagnetizedBox.GetComponent<Rigidbody>();
            var grapplerRB = transform.parent.GetComponent<Rigidbody>();
            boxRB.velocity = grapplerRB.velocity;
            boxRB.angularVelocity = grapplerRB.angularVelocity;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _MagnetizedBox = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeliveryBox")
        {
            _MagnetizedBox = other.gameObject;
            _MagnetizedBox.transform.position = transform.position;
        }
    }
}
