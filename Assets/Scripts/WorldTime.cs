using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public struct WorldTime
{
    [Range(0, 24)]
    public int Hours;

    [Range(0, 60)]
    public int Minutes;


    public void setTime(int hours, int minutes)
    {
        // Clamp = oblige la valeur donnée à être entre 0 et 24 ou 60 pour les minutes
        this.Hours = Mathf.Clamp(hours, 0, 24);
        this.Minutes = Mathf.Clamp(minutes, 0, 60);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}