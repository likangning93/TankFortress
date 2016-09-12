using UnityEngine;
using System.Collections;

public class artilleryRangefinder : MonoBehaviour {

    public artillery artilleryControl;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            artilleryControl.setTarget(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            artilleryControl.setNoTarget();
        }
    }
}
