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
        }
    }

    void OnTriggerEnter(Collider other)
    {
        _MagnetizedBox = other.gameObject;
        _MagnetizedBox.transform.position = transform.position;
    }
}
