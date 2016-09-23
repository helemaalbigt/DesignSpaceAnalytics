using UnityEngine;
using System.Collections;

public class LookAtPointRecorder : MonoBehaviour {



    [SerializeField]
    private float _frameRecordInterval;
    [SerializeField]
    private Transform _rootReference;
    private LayerMask _layerAllowToCollideWithLook;
    public bool _autoStartRecord;

    [Header("Debug Viewer")]
    [SerializeField]
    private bool _recording;
    [SerializeField]
    private float _timeSinceStartRecording;

    public LookPath _lookPathAffected;
    public Transform _trackedTransform;
    public LookPathToJSON _lookPathConverter;
    public LookPathToMySQLAsJson _lookPathToDB;
    public KeyCode _keycodeToStopRecording;

    void Start() {
        InvokeRepeating("RecordFrame", 0, _frameRecordInterval);
        if (_autoStartRecord) StartRecord();
    }

    void RecordFrame() {
        if (_trackedTransform == null) return;
        if (!_recording) return;
        Vector3 rootPoint = _rootReference.InverseTransformPoint(_trackedTransform.position);

        RaycastHit hit;
        Vector3 lookAtPoint = Vector3.zero;
        if (Physics.Raycast(_trackedTransform.position, _trackedTransform.forward, out hit))
            lookAtPoint = hit.point;
        lookAtPoint = _rootReference.InverseTransformPoint(lookAtPoint);

        _lookPathAffected.AddLookState(_timeSinceStartRecording, rootPoint, lookAtPoint);

    }

    void Update() {
        if (_recording) {
            _timeSinceStartRecording += Time.deltaTime;
        }
        if (Input.GetKeyDown(_keycodeToStopRecording))
            if (_recording)
                StopRecord();
    }



    void StartRecord() {
        _recording = true;
        _lookPathAffected = new LookPath(SystemInfo.deviceUniqueIdentifier);
        _lookPathAffected.Reset();

    }

    void StopRecord()
    {
        _recording = false;
        if(_lookPathConverter)
        _lookPathConverter.RecordPath(_lookPathAffected);
        if(_lookPathToDB)
        _lookPathToDB.PostLookPathData(_lookPathAffected);
    }
}
