using UnityEngine;
using System.Collections;

public class ThrusterButton : MonoBehaviour {

    public ThrusterBeta thruster1;
    public ThrusterBeta thruster2;

    public AudioClip click;
    private AudioSource source;

    bool buttonDown = false;

    bool playerTouching = false;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        click = source.clip;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerTouching = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerTouching = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        // handle clicking the buttons with down-arrow
        if (Input.GetKeyDown(KeyCode.DownArrow) && playerTouching)
        {
            toggleButton();
        }
    }

    public void buttonUp()
    {
        buttonDown = false;
        Vector3 scale = transform.localScale;
        Vector3 newScale = new Vector3(scale.x, 10.0f, scale.z);
        transform.localScale = newScale;
        source.PlayOneShot(click);
    }

    void toggleButton()
    {
        Vector3 newScale;
        Vector3 scale = transform.localScale;
        source.PlayOneShot(click);
        if (buttonDown)
        {
            newScale = new Vector3(scale.x, 10.0f, scale.z);
            thruster1.cancel();
            thruster2.cancel();
        }
        else
        {
            // only pressing the button down activates the thrusters
            newScale = new Vector3(scale.x, 0.1f, scale.z);
            thruster1.fire();
            thruster2.fire();
        }
        buttonDown = !buttonDown;
        transform.localScale = newScale;
    }
}
