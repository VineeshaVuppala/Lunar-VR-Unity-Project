using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoonPhaseManager : MonoBehaviour
{
    public GameObject moonPrefab;
    public Light sunLight;
    public float radius = 12f;
    public float height = 6f;
    public float spawnDelay = 0.3f;

    [System.Serializable]
    public class MoonPhase
    {
        public int day;
        public float phaseAngle;
        public string phaseName;
    }

    public List<MoonPhase> moonPhases = new List<MoonPhase>();

    private void Start()
    {
        GenerateMoonPhases();
        StartCoroutine(SpawnMoons());
    }

    void GenerateMoonPhases()
    {
        moonPhases.Clear();
        for (int i = 0; i < 28; i++)
        {
            float phaseAngle = GetPhaseAngleForDay(i + 1);
            moonPhases.Add(new MoonPhase
            {
                day = i + 1,
                phaseAngle = phaseAngle,
                phaseName = "Day " + (i + 1)
            });
        }
    }

    float GetPhaseAngleForDay(int day)
    {
        // Replace with your accurate phase angle table if needed
        return (360f / 28f) * (day - 1);
    }

    IEnumerator SpawnMoons()
    {
        for (int i = 0; i < moonPhases.Count; i++)
        {
            Vector3 position = GetArcPosition(i);
            GameObject moon = Instantiate(moonPrefab, position, Quaternion.identity, this.transform);

            moon.transform.LookAt(Camera.main.transform); // Always faces the camera

            float phaseAngle = moonPhases[i].phaseAngle;
            sunLight.transform.rotation = Quaternion.Euler(0, phaseAngle, 0);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    Vector3 GetArcPosition(int index)
    {
        float angle = Mathf.Lerp(-90, 90, index / 27f);
        float rad = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(rad) * radius;
        float y = Mathf.Sin(rad) * height;
        return new Vector3(x, y, 0);
    }
}

