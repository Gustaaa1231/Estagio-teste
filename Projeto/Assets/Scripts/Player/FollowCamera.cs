using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0.03f, -1.57f, -7.88f);
    public float rotationSpeed = 2f;

    private float pitch = 0f;
    private float yaw = 0f;
    private void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, -35, 60); // Limitar o ângulo de rotação vertical

        // Calcular a rotação da câmera
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Aplicar a rotação e a posição da câmera
        transform.position = target.position + rotation * offset;
        transform.LookAt(target);
    }
}
