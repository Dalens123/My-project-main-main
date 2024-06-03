using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] 
    private int _maxHp = 100;
    private int _hp;
    
    [SerializeField]
    private Animator _animator;

    [SerializeField] 
    private GameObject _gameOverPanel;

    public int MaxHp => _maxHp;

    public int Hp
    {
        get => _hp;
        private set
        {
            var isDamage = value < _hp;
            _hp = Mathf.Clamp(value, 0, _maxHp);
            if (isDamage)
            {
                Damaged?.Invoke(_hp);
                _animator.SetTrigger("Damaged");
            }
            else
            {
                Healed?.Invoke(_hp);
            }

            if (_hp <= 0)
            {
                Died?.Invoke();
                ShowGameOverScreen();
            }
        }
    }

    public UnityEvent<int> Healed;
    public UnityEvent<int> Damaged;
    public UnityEvent Died;

    private void Awake()
    {
        _hp = _maxHp;
        _gameOverPanel.SetActive(false);
    }

    public void Damage(int amount) => Hp -= amount;

    public void Heal(int amount) => Hp += amount;
    
    public void HealFull() => Hp = _maxHp;
    
    public void Kill() => Hp = 0;
    
    public void Adjust(int value) => Hp = value;

    private void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Damaged"))
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                _animator.SetTrigger("Idle");
            }
        }
    }

    private void ShowGameOverScreen()
    {
        _gameOverPanel.SetActive(true);
    }
}
