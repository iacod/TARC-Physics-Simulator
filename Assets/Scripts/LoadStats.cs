using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI velocityText;
    [SerializeField] private TextMeshProUGUI accelarationText;
    [SerializeField] private TextMeshProUGUI apogeeText;
    [SerializeField] private TextMeshProUGUI timeText;

    void Start()
    {
        velocityText.text = $"Max Velocity: {PlayerPrefs.GetFloat("max_velocity")} m/s";
        accelarationText.text = $"Max Accelaration: {PlayerPrefs.GetFloat("max_accelaration")} m/s^2";
        apogeeText.text = $"Apogee: {PlayerPrefs.GetFloat("max_height")} m";
        timeText.text = $"Time elapsed: {PlayerPrefs.GetFloat("time_elapsed")} s";
    }
}
