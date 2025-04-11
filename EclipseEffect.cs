using UnityEngine;
using UnityEngine.UI;

public class LunarEclipseController : MonoBehaviour
{
    public Transform moon; // Assign the Moon object
    public Light directionalLight; // Assign the directional light
    public Slider eclipseSlider; // UI slider to control eclipse manually

    private Renderer moonRenderer;

    void Start()
    {
        moonRenderer = moon.GetComponent<Renderer>(); // Get the Moon's material renderer
    }

    void Update()
    {
        float value = eclipseSlider.value; // Get slider value (0 to 1)

        // Smoothly transition the Moon's color from white to red
        Color moonColor = Color.Lerp(Color.white, new Color(0.5f, 0, 0), value);
        moonRenderer.material.color = moonColor;

        // Adjust directional light intensity (from bright to dark)
        directionalLight.intensity = Mathf.Lerp(10, 0, value);

        // Change directional light color to a reddish hue as the eclipse progresses
        directionalLight.color = Color.Lerp(Color.white, new Color(0.8f, 0.2f, 0.2f), value);
    }
}
