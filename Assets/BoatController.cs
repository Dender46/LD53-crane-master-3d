using System;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [Serializable]
    struct MinMax
    {
        public float Start, End;
    }

    [SerializeField] Transform _BoxesDeliveryContainer;
    [SerializeField] float _Speed = 5.0f;

    [SerializeField] MinMax _StartEnd = new MinMax{ Start = -31.0f, End = 40.0f };

    int _NeedBoxes = 3;
    int _BoxesOnBoard = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (_BoxesOnBoard >= _NeedBoxes)
        {
            var newPos = transform.position;
            newPos.z += _Speed * Time.deltaTime;
            transform.position = newPos;

            if (newPos.z > _StartEnd.End)
            {
                // Set boat at the beggining of queue
                SetBack();
            }
        }
    }

    void SetBack()
    {
        var newPos = transform.position;
        newPos.z = _StartEnd.Start;
        transform.position = newPos;

        foreach (Transform child in _BoxesDeliveryContainer)
        {
            Destroy(child.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeliveryBox")
        {
            other.transform.parent = _BoxesDeliveryContainer;
            GameManager.Score++;
            _BoxesOnBoard++;
        }
    }
}
