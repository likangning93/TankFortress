using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

    public int lifeSpan = 30;
    public float maxScale = 10.0f;
    public int delay = 0;

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
        if (delay > 0)
        {
            delay--;
            scaleScratch.x = 0.0f;
            scaleScratch.y = 0.0f;
            scaleScratch.z = 0.0f;
            transform.localScale = scaleScratch;
        }
        else
        {
            currentScale += scaleStep;
            lifeRemaining--;

            scaleScratch.x = currentScale;
            scaleScratch.y = currentScale;
            scaleScratch.z = currentScale;
            transform.localScale = scaleScratch;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (lifeRemaining < 0)
        {
            Destroy(gameObject);
        }
	}
}
