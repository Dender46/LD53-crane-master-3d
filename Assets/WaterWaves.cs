using UnityEngine;

public class WaterWaves : MonoBehaviour
{
    [SerializeField] Material _WaterMaterial;
    [Space(10)]
    [SerializeField] Transform _WaterPlane;
    [SerializeField] float _Speed = 2.0f;
    [SerializeField] float _Amplitude = 2.0f;

    float _Wobble = 0.0f;

    void FixedUpdate()
    {
        _Wobble = Mathf.Sin(Time.timeSinceLevelLoad) / _Amplitude;

        _WaterPlane.position = new Vector3(_WaterPlane.position.x, _WaterPlane.position.y + _Wobble, _WaterPlane.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y + _Wobble, transform.position.z);

        var textureOffset = _WaterMaterial.mainTextureOffset;
        textureOffset.x -= _Speed * Time.deltaTime;
        textureOffset.y -= _Speed * Time.deltaTime;
        _WaterMaterial.mainTextureOffset = textureOffset;
    }
}
