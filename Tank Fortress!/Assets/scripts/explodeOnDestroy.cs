using UnityEngine;
using System.Collections;

public class explodeOnDestroy : MonoBehaviour {

    public Transform explosionPrefab;
    public float radius = 5.0f;
    public int explosionCount = 10;

    // http://answers.unity3d.com/questions/169656/instantiate-ondestroy.html
    bool isQuitting = false;
    void OnApplicationQuit()
    {
        isQuitting = true;
    }
    void OnDestroy()
    {
        if (!isQuitting)
        {
            spawnExplosions();
        }
    }

	// Update is called once per frame
    Vector3 offset = new Vector3();
    void spawnExplosions()
    {
        for (int i = 0; i < explosionCount; i++)
        {
            offset.x = 0.5f - Random.value * radius;
            offset.y = 0.5f - Random.value * radius;
            Transform explosionClone = Instantiate(explosionPrefab);
            explosionClone.transform.position = transform.position + offset;
            explosion explode = explosionClone.GetComponent<explosion>();
            explode.delay = i * 3;
        }
	}
}
