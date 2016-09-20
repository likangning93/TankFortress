using UnityEngine;
using System.Collections;

public class SelfRighting : MonoBehaviour {
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update () {
        // Keep this from tipping over
        Quaternion rotation = rb2d.transform.rotation;
        if (rotation.z > 0.30f)
        {
            rb2d.transform.rotation = new Quaternion(rotation.x, rotation.y, 0.30f, rotation.w);
        }
        else if (rotation.z < -0.30f)
        {
            rb2d.transform.rotation = new Quaternion(rotation.x, rotation.y, -0.30f, rotation.w);
        }
	}
}
