using UnityEngine;
using System.Collections;

public class fireButton : MonoBehaviour {

    public GameObject gunControlObject;
    GunControl gunControl;

    // Use this for initialization
    void Start()
    {
        gunControl = gunControlObject.GetComponent<GunControl>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gunControl.clickFire();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gunControl.unclick();
        }
    }
}
