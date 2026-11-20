using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class AimSystem : MonoBehaviour
{
    [SerializeField] Transform pointer;
    [SerializeField] Transform entity;
    [SerializeField] float distance = 1.5f;

    Vector3 direction = Vector3.forward;


    public void SetDirection(Vector3 dir)
    {
        direction = dir;

    }

    void Update()
    {
        

        pointer.position = entity.position + direction * distance;
        pointer.rotation = Quaternion.LookRotation(direction);
    }
}
