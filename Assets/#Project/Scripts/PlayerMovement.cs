using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    public float _Speed = 2f;

    [SerializeField] private VRInput _VRInput;
    [SerializeField] private Transform _Cam;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    private void OnEnable()
    {
        _VRInput.OnPressed += MoveForeward;
    }

    private void OnDisable()
    {
        _VRInput.OnPressed -= MoveForeward;
    }

    private void MoveForeward()
    {
        Vector3 forwardVector = Vector3.ProjectOnPlane(_Cam.forward, Vector3.up);
        Debug.DrawRay(_Cam.position, forwardVector);
        transform.Translate(_Cam.forward * Time.deltaTime * _Speed, Space.World);
    }
}
