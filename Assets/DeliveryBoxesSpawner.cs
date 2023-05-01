using System;
using System.Collections.Generic;
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

    [SerializeField] int _MaxBoxes = 15;
    [SerializeField] float _SpawnInterval = 1.0f;
    [SerializeField] MinMax _SpawnLimitZ = new MinMax(){ Min = 0.0f, Max = 10.0f };
    [SerializeField] MinMax _Strength = new MinMax(){ Min = 10.0f, Max = 20.0f };
    [SerializeField] Vector3 _Direction = Vector3.one;
    [SerializeField] List<Material> _BoxMaterials;

    Transform _BoxesContainer;
    float _Timer = 0.0f;

    void Start()
    {
        _BoxesContainer = GameObject.Find("(C) Delivery Boxes").transform;
    }

    void Update()
    {
        if (!GameManager.IsPlaying())
        {
            return;
        }

        TrySpawning();

        if (Input.GetKeyDown(KeyCode.V))
        {
            SpawnNewBox();
        }
    }

    void TrySpawning()
    {
        if (_BoxesContainer.childCount > _MaxBoxes)
        {
            _Timer = 0.0f;
            return;
        }

        _Timer += Time.deltaTime;
        if (_Timer < _SpawnInterval)
        {
            return;
        }

        _Timer = 0.0f;
        var newPos = transform.position;
        newPos.z = Random.Range(_SpawnLimitZ.Min, _SpawnLimitZ.Max);
        transform.position = newPos;

        SpawnNewBox();
    }

    void SpawnNewBox()
    {
        var newBox = Instantiate(_BoxPrefab, transform.position, Quaternion.identity, _BoxesContainer);
        var newBoxRB = newBox.GetComponent<Rigidbody>();

        newBoxRB.velocity = _Direction.normalized * Random.Range(_Strength.Min, _Strength.Max);

        var boxMaterial = _BoxMaterials[Random.Range(0, _BoxMaterials.Count)];
        newBox.transform.GetChild(0).GetComponent<MeshRenderer>().material = boxMaterial;
        
        newBox.transform.localScale *= Random.Range(1.0f, 1.3f);
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
