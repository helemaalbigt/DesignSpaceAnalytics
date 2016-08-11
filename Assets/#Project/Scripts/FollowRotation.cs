using UnityEngine;
using System.Collections;

public class FollowRotation : MonoBehaviour {

    public bool lockX, lockY, lockZ;
    public Transform _Target;

	// Update is called once per frame
	void Update () {
        Vector3 eulerRot = new Vector3(
            lockX ? transform.rotation.eulerAngles.x : _Target.rotation.eulerAngles.x,
            lockY ? transform.rotation.eulerAngles.y : _Target.rotation.eulerAngles.y,
            lockZ ? transform.rotation.eulerAngles.z : _Target.rotation.eulerAngles.z
        );
        transform.rotation = Quaternion.Euler(eulerRot);
	}
}
