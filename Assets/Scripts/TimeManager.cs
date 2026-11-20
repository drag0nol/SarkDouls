using UnityEngine;

public class TimeManager : MonoBehaviour
{


    public float _currentTime;
    //l'heure actuelle dans le jeu
    public WorldTime LocalTime;
    [SerializeField] float _timeMultiplier = 1f;
    [SerializeField] int _secondsInAFullDay = 120;


    private float WorldTimeToInt(WorldTime time)
    {
        return (time.Hours + (time.Minutes / 60f)) / 24f;
    }


    private void updateWorldTime()
    {
        var fHours = 24 * _currentTime;
        var hours = (int)fHours;
        var minutes = (int)(60 * (fHours - Mathf.Floor(fHours)));

        LocalTime.setTime(hours, minutes);

    }


    private void Reset()
    {
        LocalTime = new WorldTime { Hours = 22, Minutes = 0 };
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //LocalTime = new WorldTime { Hours = 22, Minutes = 0 };
        _currentTime = WorldTimeToInt(LocalTime);
    }

    // Update is called once per frame
    void Update()
    {
        updateWorldTime();

        _currentTime += (Time.deltaTime / _secondsInAFullDay) * _timeMultiplier;

        // 1 = une journée écoulée on repart à 0
        if (_currentTime >= 1)
        {
            _currentTime = 0;
        }
    }
}
