using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BoatController : MonoBehaviour
{
    [Serializable]
    struct MinMax
    {
        public int Min, Max;
    }
    [SerializeField] Text _UIMissionText;
    [SerializeField] Image _UIMissionTimer;
    [Space(10)]
    [SerializeField] Transform _BoxesDeliveryContainer;
    [SerializeField] float _Speed = 5.0f;
    [Space(10)]
    [SerializeField] float _TimeForDelivery = 60.0f;
    [SerializeField] MinMax _MinMaxNeededBoxes;
    [SerializeField, ReadOnly] int _NeedBoxes = 3;
    [SerializeField, ReadOnly] int _BoxesOnBoard = 0;
    
    [Space(20)]
    [SerializeField] bool _IsMissionStarted = false;

    float _Timer;

    void Start()
    {
        _NeedBoxes = Random.Range(_MinMaxNeededBoxes.Min, _MinMaxNeededBoxes.Max);
        _UIMissionText.text = "0 / " + _NeedBoxes;
    }

    void Update()
    {
        _UIMissionText.rectTransform.parent.position = Camera.main.WorldToScreenPoint(transform.position);

        if (!_IsMissionStarted)
            return;

        _Timer += Time.deltaTime;
        _UIMissionTimer.fillAmount = (_TimeForDelivery - _Timer) / _TimeForDelivery;
        if (_Timer >= _TimeForDelivery)
        {
            // Game Over
            return;
        }

        if (transform.position.z > 5)
        {
            var newScale = _UIMissionText.rectTransform.parent.localScale;
            newScale.x -= 0.3f * Time.deltaTime;
            newScale.y -= 0.3f * Time.deltaTime;
            _UIMissionText.rectTransform.parent.localScale = newScale;
        }
        else
        {
            _UIMissionText.rectTransform.parent.localScale = Vector3.one;
        }
    }

    public void Move()
    {
        var actualSpeed = _Speed * Time.deltaTime;
        var newPos = transform.position;
        newPos.z += actualSpeed;
        transform.position = newPos;
    }

    public void ResetStatus()
    {
        _UIMissionText.rectTransform.parent.localScale = Vector3.one;
        foreach (Transform box in _BoxesDeliveryContainer)
        {
            Destroy(box.gameObject);
        }

        _IsMissionStarted = false;
        _BoxesOnBoard = 0;
        _Timer = 0;

        _NeedBoxes = Random.Range(_MinMaxNeededBoxes.Min, _MinMaxNeededBoxes.Max);
        _UIMissionText.text = "0 / " + _NeedBoxes;
        _UIMissionTimer.fillAmount = 1.0f;
    }

    public void StartMission()
    {
        _IsMissionStarted = true;
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
