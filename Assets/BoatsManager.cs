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
    
    [SerializeField] MinMax _StartEnd = new MinMax{ Start = -40.0f, End = 40.0f };

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
                if (t.localPosition.z > _StartEnd.End)
                {
                    t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, _StartEnd.Start);
                    boatController.ResetStatus();
                    StopQueue();
                    StartMission();
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

    static void StartMission()
    {
        foreach (Transform t in _BoatsContainer.transform)
        {
            // is boat in the middle?
            if (-1.0f < t.localPosition.z && t.localPosition.z < 1.0f)
            {
                t.gameObject.GetComponent<BoatController>().StartMission();
            }
        }
    }
}
