using UnityEngine;

public class GazeBasedLOD_EccentricityModel : MonoBehaviour
{
    public MeshRenderer[] lodRenderers; // 0 = high, 1 = medium, 2 = low (unintuitive but thats the standard :/ )
    public Transform gazeOrigin;
    public Transform headOrigin;
    public Vector3 gazeDirection;
    public bool simulatedGaze;

    // Parameters from Watson’s eccentricity model
    [Range(0.01f, 1.0f)]
    public float k = 0.1f; // Tuning constant (0.02–0.2 works well, more = stricter)

    public float highDetailThreshold = 0.8f;
    public float mediumDetailThreshold = 0.4f;

    void Update()
    {
        Vector3 toObject;
        if (!simulatedGaze)
        {
            gazeDirection = gazeOrigin.forward;
            toObject = (transform.position - gazeOrigin.position).normalized;
        }
        else
        {
            gazeDirection = headOrigin.forward;
            toObject = (transform.position - headOrigin.position).normalized;
        }

        float eccentricity = Vector3.Angle(gazeDirection, toObject); 

        // Apply Watson-style acuity falloff aka fancy formula from the paper: 
        // A(e) = 1 / (1 + k * e)
        float acuity = 1f / (1f + k * eccentricity);

        if (acuity > highDetailThreshold)
        {
            SetLOD(0); // High detail
        }
        else if (acuity > mediumDetailThreshold)
        {
            SetLOD(1); // Medium detail
        }
        else
        {
            SetLOD(2); // Low detail
        }
    }

    void SetLOD(int index)
    {
        for (int i = 0; i < lodRenderers.Length; i++)
        {
            //   lodRenderers[i].enabled = (i == index); NOTE do not uncomment
            // var blat = lodRenderers[i].gameObject;
            // blat.SetActive((i == index)); 
            lodRenderers[0].gameObject.SetActive(true);
        }
    }
}
