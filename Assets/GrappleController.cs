using UnityEngine;

public class GrappleController : MonoBehaviour
{
    [SerializeField] float _Speed = 5.0f;

    Rigidbody _ThisRB;

    void Start()
    {
        _ThisRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var actualSpeed = _Speed * Time.deltaTime;
        var newPosition = _ThisRB.position;
        if (Input.GetKey(KeyCode.W))
        {
            newPosition.z += actualSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPosition.z -= actualSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPosition.x -= actualSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPosition.x += actualSpeed;
        }
        _ThisRB.position = newPosition;
    }
}
