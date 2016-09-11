using UnityEngine;
using System.Collections;

public class backButton : MonoBehaviour
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
