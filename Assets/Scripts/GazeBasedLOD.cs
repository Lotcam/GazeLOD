using UnityEngine;

public class GazeBasedLOD : MonoBehaviour
{
    public MeshRenderer[] lodRenderers; // 0 = high, 1 = medium, 2 = low (unintuitive but thats the standard :/ )
    public Transform gazeOrigin;
    public Vector3 gazeDirection;     

    public float angleThreshold1 = 5f;
    public float angleThreshold2 = 15f;

    void Update()
    {
        gazeDirection = gazeOrigin.forward; 
        Vector3 toObject = (transform.position - gazeOrigin.position).normalized;
        float angle = Vector3.Angle(toObject, gazeDirection);

        Debug.DrawRay(gazeOrigin.position, gazeOrigin.forward * 5, Color.red);
        if (angle < angleThreshold1)
        {
            SetLOD(0); // H
        }
        else if (angle < angleThreshold2)
        {
            SetLOD(1); // M
        }
        else
        {
            SetLOD(2); // L
        }
    }

    void SetLOD(int index)
    {
        for (int i = 0; i < lodRenderers.Length; i++)
        {
            lodRenderers[i].enabled = (i == index);
        }
    }
}
