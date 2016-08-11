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
        _VRInput.OnDown += EnableRotation;
        _VRInput.OnUp += DisableRotation;

        _VRInput.OnPressed += MoveForeward;
    }

    private void OnDisable()
    {
        _VRInput.OnDown -= EnableRotation;
        _VRInput.OnUp -= DisableRotation;

        _VRInput.OnPressed -= MoveForeward;
    }

    private void MoveForeward()
    {
        Vector3 forwardVector = Vector3.ProjectOnPlane(_Cam.forward, Vector3.up).normalized;
        transform.Translate(forwardVector * Time.deltaTime * _Speed);
    }

    private void EnableRotation()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void DisableRotation()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
}
