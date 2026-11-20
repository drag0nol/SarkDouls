using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health_Stam_bar : MonoBehaviour
{


    [SerializeField] LivingEntity _lifeSystemJ1;
    [SerializeField] LivingEntity _lifeSystemJ2;//ça va être à récupérer depuis le player pour le multijoueur
    [SerializeField] Slider _sliderJ1;
    [SerializeField] Slider _sliderJ2;
    [SerializeField] MyPlayerInputManager _playerInputManager;
    //[SerializeField] GameObject _player;
    //[SerializeField] GameObject _player2;

    void LinkingSlider(GameObject player) 
    {
        //_player = player;
        if (!_lifeSystemJ1)
        {
            _lifeSystemJ1 = player.GetComponent<LivingEntity>();
            _sliderJ1.gameObject.SetActive(true);
            _sliderJ1.maxValue = _lifeSystemJ1.MaxHP;
            _sliderJ1.value = _lifeSystemJ1.HP;
            _lifeSystemJ1.onDamaged += UpdateSlider;
            _lifeSystemJ1.onHealed += UpdateSlider;
        }
        else
        {
            _lifeSystemJ2 = player.GetComponent<LivingEntity>();
            _sliderJ2.gameObject.SetActive(true);
            _sliderJ2.maxValue = _lifeSystemJ2.MaxHP;
            _sliderJ2.value = _lifeSystemJ2.HP;
            _lifeSystemJ2.onDamaged += UpdateSlider;
            _lifeSystemJ2.onHealed += UpdateSlider;
        }

        


        //Debug.Log("PLAYER : "+_player  +" LIFE SYSTEM : "+ _lifeSystem );
    }

    void UpdateSlider(LivingEntity entity)
    {
        var currentHP = entity.HP;
        Debug.Log(entity.HP);
        if (entity.gameObject.GetComponent<J1>())
        {
            _sliderJ1.value = currentHP;
        } else if (entity.gameObject.GetComponent<J2>())
        {
            _sliderJ2.value = currentHP;
        }
    }


    private void Reset()
    {
        _sliderJ1 = GameObject.Find("Joueur1").GetComponentInChildren<Slider>();
        _sliderJ2 = GameObject.Find("Joueur2").GetComponentInChildren<Slider>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Je start dans le UI");
        _sliderJ1.gameObject.SetActive(false);
        _sliderJ2.gameObject.SetActive(false);
        _playerInputManager.joined += LinkingSlider;
        Debug.Log("Je suis abonné dans le UI au playerinput");
    }

    // Update is called once per frame
    void Update()   
    {
        
    }

    private void OnDisabled()
    {
        _lifeSystemJ1.onDamaged -= UpdateSlider;
        _lifeSystemJ1.onHealed -= UpdateSlider;
        _lifeSystemJ2.onDamaged -= UpdateSlider;
        _lifeSystemJ2.onHealed -= UpdateSlider;
        _playerInputManager.joined -= LinkingSlider;
    }

    private void OnDestroy()
    {
        if (_lifeSystemJ1)
        {
            _lifeSystemJ1.onDamaged -= UpdateSlider;
            _lifeSystemJ1.onHealed -= UpdateSlider;
        }
        if (_lifeSystemJ2)
        {
        _lifeSystemJ2.onDamaged -= UpdateSlider;
        _lifeSystemJ2.onHealed -= UpdateSlider;
        }
        if (_playerInputManager)
            _playerInputManager.joined -= LinkingSlider;
    }

}
