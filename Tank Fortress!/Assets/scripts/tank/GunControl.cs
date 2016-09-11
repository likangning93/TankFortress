using UnityEngine;
using System.Collections;

enum TurretState { CLOCKWISE, CCLOCKWISE, NEUTRAL };

public class GunControl : MonoBehaviour {
    public GameObject clockwiseButton;
    public GameObject cClockwiseButton;
    public GameObject fireButton;
    public GameObject gun;
    public GameObject currentButton = null;

    TurretState turretState = TurretState.NEUTRAL;

    float m_orientationChange = 0.5f;
    float m_totalOrientationChange = 0.0f;
    bool m_firing = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        // handle clicking the buttons with down-arrow
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentButton == clockwiseButton)
            {
                if (turretState == TurretState.CLOCKWISE)
                {
                    stopTurret();
                }
                else
                {
                    turretState = TurretState.CLOCKWISE;
                    buttonDown(clockwiseButton);
                    buttonUp(cClockwiseButton);
                }
            }
            else if (currentButton == cClockwiseButton)
            {
                if (turretState == TurretState.CCLOCKWISE)
                {
                    stopTurret();
                }
                else
                {
                    turretState = TurretState.CCLOCKWISE;
                    buttonUp(clockwiseButton);
                    buttonDown(cClockwiseButton);
                }
            }
            else if (currentButton == fireButton)
            {
                if (m_firing)
                {
                    m_firing = false;
                    buttonUp(fireButton);
                }
                else
                {
                    m_firing = true;
                    buttonDown(fireButton);
                }
            }
        }
	}

    void FixedUpdate()
    {
        if (turretState == TurretState.CCLOCKWISE)
        {
            changeOrientation(m_orientationChange);
        }
        if (turretState == TurretState.CLOCKWISE)
        {
            changeOrientation(-m_orientationChange);
        }
    }

    void stopTurret()
    {
        turretState = TurretState.NEUTRAL;
        buttonUp(clockwiseButton);
        buttonUp(cClockwiseButton);
    }

    public void clickFire()
    {
        currentButton = fireButton;
    }

    public void unclick()
    {
        currentButton = null;
    }

    public void clickClockwise()
    {
        currentButton = clockwiseButton;
    }

    public void clickCclockwise()
    {
        currentButton = cClockwiseButton;
    }

    void buttonUp(GameObject button)
    {
        Vector3 scale = button.transform.localScale;
        Vector3 newScale = new Vector3(scale.x, 7.0f, scale.z);
        button.transform.localScale = newScale;
    }

    void buttonDown(GameObject button)
    {
        Vector3 scale = button.transform.localScale;
        Vector3 newScale = new Vector3(scale.x, 2.0f, scale.z);
        button.transform.localScale = newScale;
    }

    void changeOrientation(float dRotation)
    {
        m_totalOrientationChange += dRotation;
        if (m_totalOrientationChange > 90.0f)
        {
            dRotation = 0.0f;
            m_totalOrientationChange = 90.0f;
        }
        if (m_totalOrientationChange < -90.0f)
        {
            dRotation = 0.0f;
            m_totalOrientationChange = -90.0f;
        }

        Vector3 rotation = gun.transform.rotation.eulerAngles;
        rotation.z += dRotation;
        gun.transform.rotation = Quaternion.Euler(rotation);
    }
}
