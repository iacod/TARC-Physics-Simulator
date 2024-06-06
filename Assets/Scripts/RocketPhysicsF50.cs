using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class RocketPhysicsF50 : MonoBehaviour
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
        
        rocket.AddForce(0, (float) calculateThrust(timeElapsed), 0, ForceMode.Force);

        if (timeElapsed == 6)
        {
            Debug.Log("Parachute Deployed");
        }

        maxHeight = Mathf.Max(maxHeight, rocket.position.y);
        maxVelocity = Mathf.Max(maxVelocity, rocket.velocity.y);
        maxAcceleration = Mathf.Max(maxAcceleration, (rocket.velocity.y - previousVelocity)/Time.deltaTime);

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
        if (time >= 0.0f && time < 0.1f)
        {
            thrust = 180f * time;
        }
        else if (time >= 0.1f && time < 0.2f)
        {
            thrust = -30f * time + 21f;
        }
        else if (time >= 0.2f && time < 0.6f)
        {
            thrust = -2.5f * time + 15.5f;
        }
        else if (time >= 0.6f && time < 0.8f)
        {
            thrust = -8.75f * time + 19.25f;
        }
        else if (time >= 0.8f && time < 1.0f)
        {
            thrust = -10f * time + 20.25f;
        }
        else if (time >= 1.0f && time < 1.3f)
        {
            thrust = -28.3f * time + 38.58f;
        }
        else if (time >= 1.3f && time < 1.5f)
        {
            thrust = -8.75f * time + 13.125f;
        }
        else
        {
            thrust = 0f;
        }

        return thrust * 4.44822F;
    }
}
