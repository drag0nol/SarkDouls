using Unity.VisualScripting;
using UnityEngine;

public class ContinueStory : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick()
    {
        DialogueManager.Instance.ContinueStory();
        Debug.Log("click!");
    }
}
