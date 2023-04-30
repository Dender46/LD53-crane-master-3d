using System;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [SerializeField] Transform _BoxesDeliveryContainer;
    [SerializeField] float _Speed = 5.0f;

    [ReadOnly] public int _NeedBoxes = 3;
    [ReadOnly] public int _BoxesOnBoard = 0;


    public void Move()
    {
        var actualSpeed = _Speed * Time.deltaTime;
        var newPos = transform.position;
        newPos.z += actualSpeed;
        transform.position = newPos;
    }

    public void ClearCargo()
    {
        _BoxesOnBoard = 0;
        foreach (Transform box in _BoxesDeliveryContainer)
        {
            Destroy(box.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeliveryBox")
        {
            other.transform.parent = _BoxesDeliveryContainer;
            GameManager.Score++;
            _BoxesOnBoard++;

            if (_BoxesOnBoard >= _NeedBoxes)
            {
                BoatsManager.RunQueue();
            }
        }
    }
}
