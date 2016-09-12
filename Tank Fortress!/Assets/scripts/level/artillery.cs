using UnityEngine;
using System.Collections;

public class artillery : MonoBehaviour {

    public GameObject turretBarrel;
    public GameObject turretHousing;

    public int rotateSpeed = 1;
    public int turretCooldown = 0;
    public int turretCooldownRate = 80;

    public Transform cannonballPrefab;

    Transform m_target = null;

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
            Vector3 launchDirection = checkAndCorrectOrientation(targetPositionX);

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
        if (turretCooldown > 0) turretCooldown--;
	}

    Vector3 launchDirectionScratch = new Vector3(1.0f, 1.0f, 0.0f);
    Vector3 checkAndCorrectOrientation(float targetPositionX)
    {
        float positionX = transform.position.x;
        float goalAngle = -45f;
        launchDirectionScratch.x = 1.0f;
        launchDirectionScratch.y = 1.0f;
        if (positionX > targetPositionX)
        {
            launchDirectionScratch.x = -1.0f;
            goalAngle = 45f;
        }
        turretHousing.transform.rotation = Quaternion.AngleAxis(goalAngle, Vector3.forward);
        return launchDirectionScratch;
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
