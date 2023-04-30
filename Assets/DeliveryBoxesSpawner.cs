using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryBoxesSpawner : MonoBehaviour
{
    [Serializable]
    struct MinMax
    {
        public float Min, Max;
    }

    [SerializeField] GameObject _BoxPrefab;

    [SerializeField] float _SpawnInterval = 1.0f;
    [SerializeField] MinMax _SpawnLimitZ = new MinMax(){ Min = 0.0f, Max = 10.0f };
    [SerializeField] MinMax _Strength = new MinMax(){ Min = 10.0f, Max = 20.0f };
    [SerializeField] Vector3 _Direction = Vector3.one;

    Transform _BoxesParent;
    float _Timer = 0.0f;

    void Start()
    {
        _BoxesParent = GameObject.Find("(C) Delivery Boxes").transform;
    }

    void Update()
    {
        TrySpawning();

        if (Input.GetKeyDown(KeyCode.V))
        {
            SpawnNewBox();
        }
    }

    void TrySpawning()
    {
        _Timer += Time.deltaTime;
        if (_Timer < _SpawnInterval)
            return;

        _Timer = 0.0f;
        var newPos = transform.position;
        newPos.z = Random.Range(_SpawnLimitZ.Min, _SpawnLimitZ.Max);
        transform.position = newPos;

        SpawnNewBox();
    }

    void SpawnNewBox()
    {
        var newBox = Instantiate(_BoxPrefab, transform.position, Quaternion.identity, _BoxesParent);
        var newBoxRB = newBox.GetComponent<Rigidbody>();

        newBoxRB.velocity = _Direction.normalized * Random.Range(_Strength.Min, _Strength.Max);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _Direction.normalized * 5.0f);

        var spawnZLimit = transform.position;
        spawnZLimit.z = _SpawnLimitZ.Min;
        Gizmos.color = Color.white;
        Gizmos.DrawCube(spawnZLimit, Vector3.one);
        Gizmos.color = Color.black;
        spawnZLimit.z = _SpawnLimitZ.Max;
        Gizmos.DrawCube(spawnZLimit, Vector3.one);
    }
}
