using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

    public int lifeSpan = 30;
    public float maxScale = 10.0f;

    int lifeRemaining;
    float scaleStep;
    float currentScale;

	// Use this for initialization
	void Start () {
        lifeRemaining = lifeSpan;
        scaleStep = maxScale / lifeSpan;
        currentScale = scaleStep;
	}

    Vector3 scaleScratch = new Vector3();
    void FixedUpdate()
    {
        currentScale += scaleStep;
        lifeRemaining--;

        scaleScratch.x = currentScale;
        scaleScratch.y = currentScale;
        scaleScratch.z = currentScale;
        transform.localScale = scaleScratch;
    }
	
	// Update is called once per frame
	void Update () {
        if (lifeRemaining < 0)
        {
            Destroy(gameObject);
        }
	}
}
