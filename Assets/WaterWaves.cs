using UnityEngine;

public class WaterWaves : MonoBehaviour
{
    [SerializeField] Material _WaterMaterial;
    [Space(10)]
    [SerializeField] Transform _WaterPlane;
    [SerializeField] float _Speed = 2.0f;
    [SerializeField] float _Amplitude = 2.0f;

    float _Wobble = 0.0f;

    void Update()
    {
        _Wobble = Mathf.Sin(Time.timeSinceLevelLoad) / _Amplitude;
        Debug.Log(_Wobble);

        _WaterPlane.position = new Vector3(_WaterPlane.position.x, _WaterPlane.position.y + _Wobble, _WaterPlane.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y + _Wobble, transform.position.z);
    }
}
