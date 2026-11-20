using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset _inkJSON;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(_inkJSON);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<J1>() || other.gameObject.GetComponent<J2>())
        {
            TriggerDialogue();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<J1>() || other.gameObject.GetComponent<J2>())
        {
            DialogueManager.Instance.EndDialogue();
        }
    }
}