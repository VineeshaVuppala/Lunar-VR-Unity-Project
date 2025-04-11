using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EclipseSimulation : MonoBehaviour
{
    public Light sunLight;  // Directional Light acts as the Sun
    public Transform moon;
    public Transform earth;
    public TMP_Dropdown modeDropdown;
    public float orbitRadius = 5f; // Radius of the orbit
    public float orbitSpeed = 50f; // Speed of movement along the orbit
    
    private bool isEclipseMode = false;
    private float orbitAngle = 180f; // Start with the Moon in the middle (Solar Eclipse)

    void Start()
    {
        modeDropdown.onValueChanged.AddListener(ChangeMode);
        ChangeMode(1); // Default to Eclipse Mode
        CheckEclipse(); // Check initial eclipse state
    }

    void ChangeMode(int mode)
    {
        if (mode == 0) // Moon Phases Mode
        {
            isEclipseMode = false;
            earth.gameObject.SetActive(false); // Hide Earth
        }
        else if (mode == 1) // Eclipse Mode
        {
            isEclipseMode = true;
            earth.gameObject.SetActive(true); // Show Earth
        }
    }

    void Update()
    {
        if (isEclipseMode)
        {
            HandleOrbitalMovement();
            CheckEclipse();
        }
    }

    void HandleOrbitalMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            orbitAngle += orbitSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            orbitAngle -= orbitSpeed * Time.deltaTime;
        }
        
        float radians = orbitAngle * Mathf.Deg2Rad;
        earth.position = sunLight.transform.position + new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * orbitRadius;
        moon.position = sunLight.transform.position + new Vector3(Mathf.Cos(radians + Mathf.PI), 0, Mathf.Sin(radians + Mathf.PI)) * orbitRadius;
    }

    void CheckEclipse()
    {
        if (IsSolarEclipse())
        {
            Debug.Log("🌞 Solar Eclipse Occurring!");
        }
        else if (IsLunarEclipse())
        {
            Debug.Log("🌑 Lunar Eclipse Occurring!");
        }
        else
        {
            Debug.Log("No Eclipse");
        }
    }

    bool IsLunarEclipse()
    {
        return Mathf.Abs(Mathf.Repeat(orbitAngle, 360f) - 0f) < 5f; // Earth in the middle
    }

    bool IsSolarEclipse()
    {
        return Mathf.Abs(Mathf.Repeat(orbitAngle, 360f) - 180f) < 5f; // Moon in the middle
    }
}