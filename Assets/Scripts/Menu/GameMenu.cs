using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{

    [SerializeField] string _lobbyScene;

    [Header("UI")]
    [SerializeField] GameObject _menu;
    [SerializeField] Button _playButton;



    //ajouter une liste de scene où on est considérés comme "en jeu" pour y griser le bouton play


    private void Awake()
    {
        var currentscene = SceneManager.GetActiveScene().name;
        if (currentscene == _lobbyScene)
        {
            _menu.SetActive(false);
            _playButton.interactable = false;
        }
        else
        {
            _menu.SetActive(true);
            _playButton.interactable = true;
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && SceneManager.GetActiveScene().name == _lobbyScene)
        {
            Debug.Log("escape");
            _menu.SetActive(!_menu.activeSelf);  // la doc au cas où je ne comprend plus comment ça marche : https://docs.unity3d.com/6000.6/Documentation/ScriptReference/GameObject-activeSelf.html
            if(_menu.activeSelf == true)
            {
                Time.timeScale = 0f;
            }
            else 
            { 
            
                Time.timeScale = 1f;
            }
        }

    }


    public void Play()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(_lobbyScene);
        Debug.Log("Play est cliqué");
    }

    public void Quit()
    {
        Debug.Log("click quit");
        Application.Quit();
    }
}
