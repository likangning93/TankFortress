using UnityEngine;
using System.Collections;

public class artillery : MonoBehaviour {

    public GameObject turretBarrel;
    public GameObject turretHousing;

    public int turretCooldown = 0;
    public int turretCooldownRate = 80;

    public Transform cannonballPrefab;

    Transform m_target = null;
    int goalAngle = -45;
    int currentAngle = -45;

    Vector3 launchDirection = new Vector3(-1.0f, 1.0f, 0.0f);

    float const_g = 0.0f;

    void Start()
    {
        const_g = Physics2D.gravity.y * 0.5f;
    }

	void FixedUpdate () {
        if (m_target != null && turretCooldown == 0)
        {
            float targetPositionX = m_target.transform.position.x;
            float targetPositionY = m_target.transform.position.y;

            if (checkAndCorrectOrientation(targetPositionX))
            {
                // Compute launch velocity based on 45 degree angle.
                Vector3 launchPosition = turretBarrel.transform.position;
                launchPosition += launchDirection * 2.0f;

                float xMinusX0 = Mathf.Abs(targetPositionX - launchPosition.x);
                float launchSpeed = (const_g * xMinusX0 * xMinusX0) / (targetPositionY - launchPosition.y - xMinusX0);
                if (launchSpeed > 0.0f)
                {
                    launchSpeed = Mathf.Sqrt(launchSpeed);

                    Transform cannonballClone = Instantiate(cannonballPrefab);
                    cannonballClone.transform.position = launchPosition;
                    Rigidbody2D rb2d = cannonballClone.GetComponent<Rigidbody2D>();
                    rb2d.velocity = launchDirection * launchSpeed;

                    turretCooldown = turretCooldownRate;
                }
            }
        }
        if (turretCooldown > 0) turretCooldown--;
	}

    bool checkAndCorrectOrientation(float targetPositionX)
    {
        float positionX = transform.position.x;
        launchDirection.x = 1.0f;
        launchDirection.y = 1.0f;
        goalAngle = -45;
        if (positionX > targetPositionX)
        {
            launchDirection.x = -1.0f;
            goalAngle = 45;
        }
        if (currentAngle != goalAngle)
        {
            if (goalAngle > 0)
            {
                currentAngle++;
            }
            else
            {
                currentAngle--;
            }
        }

        turretHousing.transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
        return currentAngle == goalAngle;
    }

    public void setTarget(Transform target)
    {
        if (m_target == null)
        {
            m_target = target;
        }
    }

    public void setNoTarget()
    {
        m_target = null;
    }
}
