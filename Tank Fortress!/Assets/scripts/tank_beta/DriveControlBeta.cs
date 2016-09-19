using UnityEngine;
using System.Collections;

public class DriveControlBeta : MonoBehaviour
{
    public float m_torqueMagnitude = 10.0f;

    public GameObject forwardButton;
    public GameObject backButton;
    public GameObject currentButton;

    public DriveState tankState = DriveState.NEUTRAL;

    // Update is called once per frame
    void Update()
    {

        // handle clicking the buttons with down-arrow
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            clickForward();
            clickBackward();
        }
    }

    public void clickForward()
    {
        if (currentButton == forwardButton)
        {
            if (tankState == DriveState.FORWARD)
            {
                stopTank();
            }
            else
            {
                tankState = DriveState.FORWARD;
                buttonDown(forwardButton);
                buttonUp(backButton);
            }
        }
    }

    public void clickBackward()
    {
        if (currentButton == backButton)
        {
            if (tankState == DriveState.BACKWARD)
            {
                stopTank();
            }
            else
            {
                tankState = DriveState.BACKWARD;
                buttonDown(backButton);
                buttonUp(forwardButton);
            }
        }
    }

    public void resetActiveButton()
    {
        currentButton = null;
    }

    public void activeButtonForward()
    {
        currentButton = forwardButton;
    }

    public void activeButtonBackward()
    {
        currentButton = backButton;
    }

    void stopTank()
    {
        tankState = DriveState.NEUTRAL;
        buttonUp(forwardButton);
        buttonUp(backButton);
    }

    void buttonUp(GameObject button)
    {
        Vector3 scale = button.transform.localScale;
        Vector3 newScale = new Vector3(scale.x, 10.0f, scale.z);
        button.transform.localScale = newScale;
    }

    void buttonDown(GameObject button)
    {
        Vector3 scale = button.transform.localScale;
        Vector3 newScale = new Vector3(scale.x, 0.1f, scale.z);
        button.transform.localScale = newScale;
    }
}
