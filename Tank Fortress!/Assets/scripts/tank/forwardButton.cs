using UnityEngine;
using System.Collections;

public class forwardButton : MonoBehaviour
{

    public GameObject driveControlObject;
    DriveControl driveControl;

    // Use this for initialization
    void Start()
    {
        driveControl = driveControlObject.GetComponent<DriveControl>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            driveControl.clickForward();
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            driveControl.unclick();
        }
    }
}
