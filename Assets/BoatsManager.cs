using System;
using UnityEngine;

public class BoatsManager : MonoBehaviour
{
    [Serializable]
    struct MinMax
    {
        public float Start, End;
    }

    enum State
    {
        Stopped, Running
    }
    
    [SerializeField] MinMax _StartEnd = new MinMax{ Start = -31.0f, End = 40.0f };

    static Transform _BoatsContainer;
    static State _State;

    void Start()
    {
        _BoatsContainer = GameObject.Find("/(C) Boats").transform;
    }

    void Update()
    {
        if (_State == State.Running)
        {
            foreach (Transform t in _BoatsContainer.transform)
            {
                var boatController = t.gameObject.GetComponent<BoatController>();
                boatController.Move();
                if (t.position.z > _StartEnd.End)
                {
                    t.position = new Vector3(t.position.x, t.position.y, _StartEnd.Start);
                    boatController.ClearCargo();
                    StopQueue();
                }
            }
        }
    }

    static public void RunQueue()
    {
        _State = State.Running;
    }

    static void StopQueue()
    {
        _State = State.Stopped;
    }
}
