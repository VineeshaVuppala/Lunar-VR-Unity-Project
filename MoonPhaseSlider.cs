using UnityEngine;
using UnityEngine.UI;

public class MoonPhaseSlider : MonoBehaviour
{
    public Light sunLight;  // Drag your Directional Light (Sun)
    public Transform moon;  // Drag your Moon object
    public Slider phaseSlider; // Drag your UI Slider

    void Start()
    {
        phaseSlider.onValueChanged.AddListener(UpdateMoonPhase);
    }

    void UpdateMoonPhase(float value)
    {
        // Rotate Sun around the Moon to simulate different phases
        float angle = value * 45f; // 8 phases (360° / 8 = 45° per phase)
        sunLight.transform.position = moon.position + Quaternion.Euler(0, angle, 0) * new Vector3(10, 0, 0);
        sunLight.transform.LookAt(moon); // Ensure the Sunlight always points at the Moon
    }
}