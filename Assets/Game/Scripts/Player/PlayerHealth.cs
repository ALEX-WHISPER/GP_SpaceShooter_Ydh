using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerHealth : MonoBehaviour {
    [Serializable]
    public class HealthEvent: UnityEvent<int> { }

    [Serializable]
    public class DamageEvent: UnityEvent { }

    [Serializable]
    public class DieEvent: UnityEvent { }
    
    //  初始生命值
    public int m_InitHealthAmount = 5;

    //  最大生命值
    public int m_MaxHealthAmount = 5;

    //  生命值刷新触发事件
    public HealthEvent OnHealthSet;
    public DamageEvent OnDamaged;
    public DieEvent OnDie;

    //  当前生命值
    private int m_CurHealthAmount;
    
    private void Start() {
        SetHealth(m_InitHealthAmount);
    }

    public int GetCurHealthAmount {
        get;
        private set;
    }

    //  更新生命值
    public void SetHealth(int healthPoint) {
        //  若大于最大值则返回
        if (healthPoint > m_MaxHealthAmount) {
            return;
        }

        //  更新当前生命值
        this.m_CurHealthAmount = healthPoint;

        //  调用事件
        OnHealthSet.Invoke(this.m_CurHealthAmount);
    }

    //  受到伤害
    public void TakeDamage(int damageAmount) {
        this.m_CurHealthAmount -= damageAmount;

        if (m_CurHealthAmount <= 0) {
            PlayerDie();
        } else {
            OnDamaged.Invoke();
        }
    }

    //  死亡
    public void PlayerDie() {
        OnDie.Invoke();
    }

    //  增加生命值
    public void GainHealth(int gainedAmount) {
        SetHealth(this.m_CurHealthAmount + gainedAmount);
    }
}
