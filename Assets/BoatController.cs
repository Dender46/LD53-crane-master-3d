using UnityEngine;
using UnityEngine.UI;

public class BoatController : MonoBehaviour
{
    [SerializeField] Text _UIMissionText;
    [Space(10)]
    [SerializeField] Transform _BoxesDeliveryContainer;
    [SerializeField] float _Speed = 5.0f;

    [ReadOnly] public int _NeedBoxes = 3;
    [ReadOnly] public int _BoxesOnBoard = 0;

    void Start()
    {
        _UIMissionText.text = "0 / " + _NeedBoxes;
    }

    void Update()
    {
        _UIMissionText.rectTransform.parent.position = Camera.main.WorldToScreenPoint(transform.position);
        if (transform.position.z > 5)
        {
            var newScale = _UIMissionText.rectTransform.parent.localScale;
            newScale.x -= 0.3f * Time.deltaTime;
            newScale.y -= 0.3f * Time.deltaTime;
            _UIMissionText.rectTransform.parent.localScale = newScale;
        }
    }

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
            _UIMissionText.text = _BoxesOnBoard + " / " + _NeedBoxes;

            if (_BoxesOnBoard >= _NeedBoxes)
            {
                BoatsManager.RunQueue();
            }
        }
    }
}
