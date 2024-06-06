using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class RocketPhysicsF20 : MonoBehaviour
{
    private Rigidbody rocket;
    [SerializeField] private float volume;
    [SerializeField] private float area;
    [SerializeField] private TextMeshProUGUI velocityText;
    [SerializeField] private TextMeshProUGUI altitudeText;
    [SerializeField] private TextMeshProUGUI thrustText;
    [SerializeField] private TextMeshProUGUI accelarationText;
    private float timeElapsed = 0;
    private float maxHeight = 0;
    private float maxVelocity = 0;
    private float maxAcceleration = 0;
    private float previousVelocity = 0;

    void Start()
    {
        rocket = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;

        rocket.AddForce(0, (float)calculateThrust(timeElapsed), 0, ForceMode.Force);

        if (timeElapsed == 6)
        {
            Debug.Log("Parachute Deployed");
        }

        maxHeight = Mathf.Max(maxHeight, rocket.position.y);
        maxVelocity = Mathf.Max(maxVelocity, rocket.velocity.y);
        maxAcceleration = Mathf.Max(maxAcceleration, (rocket.velocity.y - previousVelocity) / Time.deltaTime);

        velocityText.text = $"Velocity: {(int)Mathf.Round(rocket.velocity.y)} m/s";
        altitudeText.text = $"Altitude: {(int)Mathf.Round(rocket.position.y)} m";
        thrustText.text = $"Thrust: {(int)Mathf.Round(calculateThrust(timeElapsed))} N";
        accelarationText.text = $"Accelaration: {(int)Mathf.Round((rocket.velocity.y - previousVelocity) / Time.deltaTime)} m/s^2";

        previousVelocity = rocket.velocity.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (timeElapsed > 6)
        {
            PlayerPrefs.SetFloat("max_height", maxHeight);
            PlayerPrefs.SetFloat("max_velocity", maxVelocity);
            PlayerPrefs.SetFloat("max_accelaration", maxAcceleration);
            PlayerPrefs.SetFloat("time_elapsed", timeElapsed);
            Debug.Log("Max height: " + maxHeight);
            Debug.Log("Max velocity: " + maxVelocity);
            Debug.Log("Max accelaration: " + maxAcceleration);
            SceneManager.LoadScene(4);
        }
    }

    public float calculateThrust(float time)
    {
        float thrust;
        if (time >= 0.0f && time <= 0.08f)
        {
            thrust = 112.5f * time;
        }
        else if (time >= 0.08f && time <= 0.12f)
        {
            thrust = -12.5f * time + 10.0f;
        }
        else if (time >= 0.12f && time <= 0.25f)
        {
            thrust = -0.556f * time + 8.567f;
        }
        else if (time >= 0.25f && time <= 0.3f)
        {
            thrust = 1.44f * time + 8.068f;
        }
        else if (time >= 0.3f && time <= 0.7f)
        {
            thrust = -1.25f * time + 8.875f;
        }
        else if (time >= 0.7f && time <= 1.0f)
        {
            thrust = -3.333f * time + 10.333f;
        }
        else if (time >= 1.0f && time <= 1.5f)
        {
            thrust = -6.0f * time + 13.0f;
        }
        else if (time >= 1.5f && time <= 2.3f)
        {
            thrust = -4.063f * time + 10.095f;
        }
        else if (time >= 2.3f && time <= 3.0f)
        {
            thrust = -1.071f * time + 3.213f;
        }
        else
        {
            thrust = 0f;
        }
        
        return thrust * 4.44822F;
    }
}
