using UnityEngine;

public class ArmCircling : MonoBehaviour
{
    public float rotationSpeed = 50f; // Prêdkoœæ obrotu ramion
    public float circleRadius = 1.5f; // Promieñ kr¹¿enia ramion

    private float angle = 0f; // Aktualny k¹t

    public Transform leftArm;
    public Transform leftForearm;
    public Transform leftHand;

    private void Update()
    {
        // Zwiêkszanie k¹ta na podstawie prêdkoœci obrotu i czasu
        angle += rotationSpeed * Time.deltaTime;

        // Obliczanie pozycji na okrêgu
        float x = Mathf.Sin(angle) * circleRadius;
        float y = Mathf.Cos(angle) * circleRadius;
        float z = 0f;

        // Ustawianie pozycji ramienia na okrêgu
        transform.localPosition = new Vector3(x, y, z);

        // Obrót pozosta³ych elementów rêki wzglêdem barku
        leftArm.localRotation = Quaternion.Euler(-angle, 0f, 0f);
        //leftForearm.localRotation = Quaternion.Euler(-angle, 0f, 0f);
        //leftHand.localRotation = Quaternion.Euler(-angle, 0f, 0f);
    }
}
