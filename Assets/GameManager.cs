using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _BoxPrefab;
    [SerializeField] float _DistanceFromGround = 5.0f;

    Transform _DeliveryBoxesContainer;
    Rigidbody _SelectedBox = null;

    static public GameManager instance;

    static public int Score = 0;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            _DeliveryBoxesContainer = GameObject.Find("(C) Delivery Boxes").transform;
        }
        else
        {
            Debug.LogWarning("Instance already exists, destroying 'this'!");
            Destroy(this);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
        
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Ground")
                {
                    var newBox = Instantiate(_BoxPrefab, _DeliveryBoxesContainer);

                    var boxNewPos = new Vector3(hit.point.x, _DistanceFromGround, hit.point.z);
                    newBox.transform.localPosition = boxNewPos;
                }
                else if (hit.transform.gameObject.tag == "DeliveryBox")
                {
                    _SelectedBox = hit.transform.gameObject.GetComponent<Rigidbody>();

                    _SelectedBox.velocity = Vector3.zero;
                    _SelectedBox.position = new Vector3(_SelectedBox.position.x, _DistanceFromGround, _SelectedBox.position.z);
                }
            }
        }
        if (Input.GetMouseButton(0) && _SelectedBox)
        {
            var mousePos = Input.mousePosition;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
        
            if (Physics.Raycast(ray, out hit))
            {
                var boxNewPos = new Vector3(hit.point.x, _DistanceFromGround, hit.point.z);
                _SelectedBox.velocity = Vector3.zero;
                _SelectedBox.position = boxNewPos;
            }
        }
    }
}
