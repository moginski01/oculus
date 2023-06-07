using UnityEngine;

public class ArmCircling : MonoBehaviour
{
    public float rotationSpeed = 50f; // Pr�dko�� obrotu ramion
    public float circleRadius = 1.5f; // Promie� kr��enia ramion

    private float angle = 0f; // Aktualny k�t

    public Transform leftArm;
    public Transform leftForearm;
    public Transform leftHand;

    private void Update()
    {
        // Zwi�kszanie k�ta na podstawie pr�dko�ci obrotu i czasu
        angle += rotationSpeed * Time.deltaTime;

        // Obliczanie pozycji na okr�gu
        float x = Mathf.Sin(angle) * circleRadius;
        float y = Mathf.Cos(angle) * circleRadius;
        float z = 0f;

        // Ustawianie pozycji ramienia na okr�gu
        transform.localPosition = new Vector3(x, y, z);

        // Obr�t pozosta�ych element�w r�ki wzgl�dem barku
        leftArm.localRotation = Quaternion.Euler(-angle, 0f, 0f);
        //leftForearm.localRotation = Quaternion.Euler(-angle, 0f, 0f);
        //leftHand.localRotation = Quaternion.Euler(-angle, 0f, 0f);
    }
}
