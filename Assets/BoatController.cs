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
    [SerializeField] BonusShower _UIBonusShower;
    [Space(10)]
    [SerializeField] Transform _BoxesDeliveryContainer;
    [SerializeField] float _Speed = 5.0f;
    [Space(10)]
    [SerializeField] float _TimeForDelivery = 60.0f;
    [SerializeField] MinMax _MinMaxNeededBoxes;
    [SerializeField, ReadOnly] int _NeedBoxes = -1;
    [SerializeField, ReadOnly] int _BoxesOnBoard = 0;
    [Space(10)]
    [SerializeField] AudioClip _BoxScored;
    [SerializeField] AudioClip _MissionAccomplishedAudio;
    [Space(20)]
    [SerializeField] bool _IsMissionStarted = false;

    [SerializeField, ReadOnly] float _Timer;

    void Start()
    {
        _UIMissionText.text = "0 / " + _NeedBoxes;
    }

    void Update()
    {
        if (!GameManager.IsPlaying())
        {
            return;
        }

        _UIMissionText.rectTransform.parent.position = Camera.main.WorldToScreenPoint(transform.position);

        if (!_IsMissionStarted)
            return;

        _Timer += Time.deltaTime;
        _UIMissionTimer.fillAmount = (_TimeForDelivery - _Timer) / _TimeForDelivery;
        if (_Timer >= _TimeForDelivery)
        {
            GameManager.GameOver();
            return;
        }

        if (transform.localPosition.z > 5)
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
        if (_BoxesOnBoard >= _NeedBoxes)
        {
            return;
        }

        if (other.gameObject.tag == "DeliveryBox")
        {
            other.gameObject.tag = "UnpickableDeliveryBox";
            other.transform.parent = _BoxesDeliveryContainer;
            GameManager.Score++;

            var bonusType = other.gameObject.GetComponent<BoxBonus>()._BonusType;
            switch (bonusType)
            {
                case BoxBonus.BonusType.Regular:
                    break;
                case BoxBonus.BonusType.BonusPoints:
                    _UIBonusShower.ShowBonus("+2 Points");
                    GameManager.Score += 2;
                    break;
                case BoxBonus.BonusType.AddTime:
                    _UIBonusShower.ShowBonus("More Time");
                    _Timer -= 10.0f;
                    break;
            }

            GetComponent<AudioSource>().PlayOneShot(_BoxScored);

            _BoxesOnBoard++;
            _UIMissionText.text = _BoxesOnBoard + " / " + _NeedBoxes;

            if (_BoxesOnBoard >= _NeedBoxes)
            {
                GetComponent<AudioSource>().PlayOneShot(_MissionAccomplishedAudio);
                BoatsManager.RunQueue();
            }
        }
    }
}
