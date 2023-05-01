using UnityEngine;

public class CraneRopeRenderer : MonoBehaviour
{
    [SerializeField] Transform _CraneController;
    [SerializeField] Transform _Magnet;

    LineRenderer _LineRenderer;

    void Start()
    {
        _LineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        _LineRenderer.SetPositions(new Vector3[]{ _CraneController.position, _Magnet.position });
    }
}
