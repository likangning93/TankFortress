using UnityEngine;
using System.Collections;

public class BackButtonBeta : MonoBehaviour
{
    public GameObject driveControlObject;
    DriveControlBeta driveControl;

    // Use this for initialization
    void Start()
    {
        driveControl = driveControlObject.GetComponent<DriveControlBeta>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            driveControl.activeButtonBackward();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            driveControl.resetActiveButton();
        }
    }
}
