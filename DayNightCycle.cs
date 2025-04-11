using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light dayNightSun;       // The Sun (Directional Light)
    public Transform dayNightMoon;  // The Moon
    public Transform eclipseMoon;   // Object used for solar eclipse
    public Transform earthShadow;   // Object used for lunar eclipse

    public float dayDuration = 60f; // One full cycle = 60 seconds
    private float timePassed = 0f;

    void Update()
    {
        timePassed += Time.deltaTime;
        float cycleProgress = (timePassed / dayDuration) * 360f; 

        // ☀ Rotate Sun & Moon to simulate Day-Night Cycle
        dayNightSun.transform.rotation = Quaternion.Euler(new Vector3(cycleProgress - 90, 0, 0));
        dayNightMoon.transform.rotation = Quaternion.Euler(new Vector3(cycleProgress + 90, 0, 0));

        // 🔆 Adjust Sun Intensity for smooth day-night transition
        float intensity = Mathf.Clamp01(Mathf.Sin(Mathf.Deg2Rad * cycleProgress));
        dayNightSun.intensity = intensity;

        // 🌑 Solar Eclipse: Happens when Moon moves in front of the Sun (Daytime)
        float solarEclipseTime = 180f; // Noon (Sun at highest point)
        if (Mathf.Abs(cycleProgress - solarEclipseTime) < 10f) // Eclipse lasts ~20 degrees
        {
            eclipseMoon.position = dayNightSun.transform.position + new Vector3(0, 0, -5); // Move Moon in front of Sun
            dayNightSun.intensity = 0.3f; // Dim the Sun
        }
        else
        {
            eclipseMoon.position = new Vector3(1000, 1000, 1000); // Move it far away when not in use
        }

        // 🌕 Lunar Eclipse: Happens when Earth casts a shadow on the Moon (Nighttime)
        float lunarEclipseTime = 360f; // Midnight (Moon at highest point)
        if (Mathf.Abs(cycleProgress - lunarEclipseTime) < 10f)
        {
            dayNightMoon.GetComponent<Renderer>().material.color = Color.red; // Moon turns red
            earthShadow.position = dayNightMoon.position; // Shadow covers the Moon
        }
        else
        {
            dayNightMoon.GetComponent<Renderer>().material.color = Color.white; // Normal Moon color
            earthShadow.position = new Vector3(1000, 1000, 1000); // Move shadow away when not in use
        }
    }
}