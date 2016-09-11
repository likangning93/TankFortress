using UnityEngine;
using System.Collections;

public class terrainGen : MonoBehaviour {

    public int platformCount = 50;
    public float startX = -500.0f;
    public float endX = 500.0f;
    public float baseY = -10.0f;
    public float scaleY = 5.0f;
    public Transform platformPrefab;
    public int seed = 8;

    void Awake()
    {
        // compute x and y coordinates of each "point" in the terrain squiggle
        Vector2[] terrainPoints = new Vector2[platformCount + 1];
        Random.InitState(seed);

        float stepWidth = (endX - startX) / (platformCount);
        for (int i = 0; i < platformCount + 1; i++) {
            terrainPoints[i].x = startX + i * stepWidth;
            terrainPoints[i].y = baseY + Random.value * scaleY;
        }

        // generate platformCount platforms

        for (int i = 0; i < platformCount; i++)
        {
            generatePlatform(terrainPoints[i], terrainPoints[i + 1]);
        }
    }

    void generatePlatform(Vector2 position1, Vector2 position2)
    {
        Transform platformClone = Instantiate(platformPrefab);

        // compute the midpoint of the platform
        Vector2 midpoint = (position1 + position2) * 0.5f;
        
        // compute scale
        float xScale = (position2 - position1).magnitude;

        // compute the orientation
        float rotation = Mathf.Asin((position2.y - position1.y) / xScale) * Mathf.Rad2Deg;

        platformClone.transform.Translate(new Vector3(midpoint.x, midpoint.y, 0.0f));
        platformClone.transform.Rotate(Vector3.forward, rotation, Space.Self);
        platformClone.transform.localScale = new Vector3(xScale, 1.0f, 10.0f);
    }
}
