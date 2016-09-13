using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

    public float dRotation = 5.0f;
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotation.y += dRotation;
        transform.localRotation = Quaternion.Euler(rotation);
	}
}
