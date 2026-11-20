using System;
using UnityEngine;

public class Heal : MonoBehaviour
{

    [SerializeField] int _healhGain = 10;
    [SerializeField] GameObject _healFx;
    [SerializeField] bool Destructible;

    public void Use(LivingEntity target)
    {

        target.Heal(_healhGain);
        if (_healFx)
            GameObject.Instantiate(_healFx, target.gameObject.transform);
        if (Destructible)
            Destroy(gameObject);
    }

}

