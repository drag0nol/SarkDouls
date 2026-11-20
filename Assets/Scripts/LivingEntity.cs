using NaughtyAttributes;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour
{

    [Header("Dependance")]
    

    [Header("Configuration")]
    
    [ShowNonSerializedField] int _hp;
    [SerializeField] int _maxHP;
    [SerializeField] GameObject _itemDrop;


    public event Action<LivingEntity> onDamaged;
    public event Action<LivingEntity> onHealed;
    public int MaxHP
    {
        get { return _maxHP; }
    }
    public int HP
    {
        get { return _hp; }
        set { _hp = Mathf.Min(_maxHP, Mathf.Max(0, value)); }
    }

    void Awake()
    {
        
        HP = _maxHP;
        Debug.Log("hp = " + HP);

    }


    public void TakeDamage(int damage)
    {
        HP -= damage;
        onDamaged?.Invoke(this);
    }

    public void Heal(int lifeGain)
    {
        HP += lifeGain;
        onHealed?.Invoke(this);
    }

    private void OnDestroy()
    {
        
    }

    void Update()
    {
        if (HP == 0)
        {
            if (_itemDrop)
                GameObject.Instantiate(_itemDrop, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

    }
}
