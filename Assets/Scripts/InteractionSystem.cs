using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] LivingEntity entity;
    [SerializeField] PlayerBrain brain;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject && other.gameObject.GetComponent("Heal"))
        {
            brain.objectFocus = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject && other.gameObject.GetComponent("Heal"))
        {
            brain.objectFocus = null;
        }
    }
}
