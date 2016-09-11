using UnityEngine;
using System.Collections;

enum TurretState { CLOCKWISE, CCLOCKWISE, NEUTRAL };

public class GunControl : MonoBehaviour {
    public GameObject clockwiseButton;
    public GameObject cClockwiseButton;
    public GameObject fireButton;
    public GameObject gun;
    public GameObject currentButton = null;

    public GameObject turretMuzzle;
    public Transform cannonballPrefab;
    public int turretCooldown = 0;
    public int turretCooldownRate = 80;
    float turretCooldownStep = 0.0f;
    public float launchPower = 2000.0f;

    TurretState turretState = TurretState.NEUTRAL;

    float m_orientationChange = 0.5f;
    float m_totalOrientationChange = 0.0f;
    bool m_firing = false;

	// Use this for initialization
	void Start () {
        turretCooldownStep = -1.0f / turretCooldownRate;
	}

	// Update is called once per frame
	void Update () {
        // handle clicking the buttons with down-arrow
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
          clickClockwise();
          clickCClockwise();
          clickFireButton();
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
        
        animateTurret();
        
        if (m_firing)
        {
            fireCannonball();
        }
    }

    void animateTurret()
    {
        if (turretCooldown > 0) {
            turretCooldown--;
            turretMuzzle.transform.Translate(0.0f, turretCooldownStep, 0.0f);
        }
    }

    Vector3 cannonballDirectionScratch;
    void fireCannonball()
    {
        if (turretCooldown == 0)
        {
            // fire!
            Transform cannonballClone = Instantiate(cannonballPrefab);
            Vector3 position = turretMuzzle.transform.position;
            cannonballDirectionScratch = position - gun.transform.position;
            cannonballDirectionScratch.Normalize();

            cannonballClone.transform.position = position + cannonballDirectionScratch * 2.0f;

            Rigidbody2D rb2d = cannonballClone.GetComponent<Rigidbody2D>();
            rb2d.AddForce(cannonballDirectionScratch * launchPower);

            // recoil animation
            turretCooldown = turretCooldownRate;
            turretMuzzle.transform.Translate(0.0f, 1.0f, 0.0f);
        }
    }

    void clickClockwise() {
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
    }

    void clickCClockwise() {
      if (currentButton == cClockwiseButton)
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
    }

    void clickFireButton() {
      if (currentButton == fireButton)
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

    void stopTurret()
    {
        turretState = TurretState.NEUTRAL;
        buttonUp(clockwiseButton);
        buttonUp(cClockwiseButton);
    }

    public void fireButtonActive()
    {
        currentButton = fireButton;
    }

    public void resetActiveButton()
    {
        currentButton = null;
    }

    public void clockwiseButtonActive()
    {
        currentButton = clockwiseButton;
    }

    public void cClockwiseButtonActive()
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
