using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    //[Header("Ink")]
    //[SerializeField] private TextAsset _inkJSON;

    [Header("UI")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _speakerText;

    [Header("Choices")]
    [SerializeField] private GameObject[] _choiceButtons;
    [SerializeField] private TextMeshProUGUI[] _choiceTexts;

    private Story story;

    private void Awake()
    {
        Instance = this;
        _dialoguePanel.SetActive(false);
    }

    public void StartDialogue(TextAsset inkFile)
    {
        story = new Story(inkFile.text);

        _dialoguePanel.SetActive(true);

        ContinueStory();
    }

    public void ContinueStory()
    {
        if (story.canContinue)
        {
            string line = story.Continue().Trim();

            HandleTags(story.currentTags);

            _dialogueText.text = line;

            ShowChoices();
        }
        else
        {
            EndDialogue();
        }
    }

    private void HandleTags(System.Collections.Generic.List<string> tags)
    {
        foreach (string tag in tags)
        {
            string[] splitTag = tag.Split(':');

            if (splitTag.Length != 2)
                continue;

            string key = splitTag[0].Trim();
            string value = splitTag[1].Trim();

            if (key == "speaker")
            {
                _speakerText.text = value;
            }

            // Les tags portrait/layout peuvent être ignorés
            // si tu ne veux pas d'animations.
        }
    }

    private void ShowChoices()
    {
        HideChoices();

        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            _choiceButtons[i].SetActive(true);
            _choiceTexts[i].text = story.currentChoices[i].text;
        }
    }

    private void HideChoices()
    {
        foreach (GameObject button in _choiceButtons)
        {
            button.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public void EndDialogue()
    {
        _dialoguePanel.SetActive(false);
    }
}
