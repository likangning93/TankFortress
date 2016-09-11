using UnityEngine;
using System.Collections;

enum DriveState {FORWARD, BACKWARD, NEUTRAL};

public class DriveControl : MonoBehaviour
{
    public float m_torqueMagnitude = 10.0f;
    public float m_maxAngularVelocity = 500.0f;

    public GameObject wheel1;
    public GameObject wheel2;
    public GameObject wheel3;
    public GameObject wheel4;
    public GameObject wheel5;
    public GameObject wheel6;
    public GameObject wheel7;

    public GameObject forwardButton;
    public GameObject backButton;
    public GameObject currentButton;

    public Rigidbody2D m_wheel1;
    public Rigidbody2D m_wheel2;
    public Rigidbody2D m_wheel3;
    public Rigidbody2D m_wheel4;
    public Rigidbody2D m_wheel5;
    public Rigidbody2D m_wheel6;
    public Rigidbody2D m_wheel7;

    DriveState tankState = DriveState.NEUTRAL;

	// Use this for initialization
	void Start () {
        m_wheel1 = wheel1.GetComponent<Rigidbody2D>();
        m_wheel2 = wheel2.GetComponent<Rigidbody2D>();
        m_wheel3 = wheel3.GetComponent<Rigidbody2D>();
        m_wheel4 = wheel4.GetComponent<Rigidbody2D>();
        m_wheel5 = wheel5.GetComponent<Rigidbody2D>();
        m_wheel6 = wheel6.GetComponent<Rigidbody2D>();
        m_wheel7 = wheel7.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        // handle clicking the buttons with down-arrow
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentButton == forwardButton)
            {
                if (tankState == DriveState.FORWARD)
                {
                    stopTank();
                }
                else {
                    tankState = DriveState.FORWARD;
                    buttonDown(forwardButton);
                    buttonUp(backButton);
                }
            }
            else if (currentButton == backButton)
            {
                if (tankState == DriveState.BACKWARD)
                {
                    stopTank();
                }
                else {
                    tankState = DriveState.BACKWARD;
                    buttonDown(backButton);
                    buttonUp(forwardButton);
                }
            }
        }
	}

    void FixedUpdate()
    {
        // Apply torque to the drive wheels
        if (tankState == DriveState.FORWARD)
        {
            applyTorque(-m_torqueMagnitude);
        }
        if (tankState == DriveState.BACKWARD)
        {
            applyTorque(m_torqueMagnitude);
        }
    }

    public void unclick()
    {
        currentButton = null;
    }

    public void clickForward()
    {
        currentButton = forwardButton;
    }

    public void clickBack()
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

    public void applyTorque(float torque)
    {
        applyTorqueOnce(m_wheel1, torque);
        applyTorqueOnce(m_wheel2, torque);
        applyTorqueOnce(m_wheel3, torque);
        applyTorqueOnce(m_wheel4, torque);
        applyTorqueOnce(m_wheel5, torque);
        applyTorqueOnce(m_wheel6, torque);
        applyTorqueOnce(m_wheel7, torque);
    }

    void applyTorqueOnce(Rigidbody2D wheel, float torque)
    {
        wheel.AddTorque(torque);
        if (wheel.angularVelocity < -m_maxAngularVelocity) wheel.angularVelocity = -m_maxAngularVelocity;
        if (wheel.angularVelocity > m_maxAngularVelocity) wheel.angularVelocity = m_maxAngularVelocity;
    }
}
