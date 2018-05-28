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

    public bool invincibleAfterDamage = true;
    public float invincibleDuration = 3f;

    //  初始生命值
    public int m_InitHealthAmount = 5;

    //  最大生命值
    public int m_MaxHealthAmount = 5;

    //  玩家受到伤害时的爆炸效果
    public GameObject m_DamageEffect;

    //  生命值刷新触发事件
    public HealthEvent OnHealthSet;
    public DamageEvent OnDamaged;
    public DieEvent OnDie;

    protected Animator m_Animator;
    protected readonly int m_HashDamaged = Animator.StringToHash("damaged");

    //  当前生命值
    private int m_CurHealthAmount;
    private bool m_Invincible = false;
    
    private PlayerSkills playerSkillControl;

    private void Awake() {
        playerSkillControl = GetComponent<PlayerSkills>();
        m_Animator = GetComponent<Animator>();
    }

    private void Start() {
        SetHealth(m_InitHealthAmount);
    }

    private void OnEnable() {
        if (playerSkillControl.medic.isSupported)
            playerSkillControl.medic.btn.GetComponent<Skill_Medic>().FireSkill_UltiMedic += GainHealth;
    }

    private void OnDisable() {
        if (playerSkillControl.medic.isSupported && playerSkillControl.medic.btn != null)
            playerSkillControl.medic.btn.GetComponent<Skill_Medic>().FireSkill_UltiMedic -= GainHealth;
    }

    public int GetCurHealthAmount {
        get;
        private set;
    }

    public void EnableInvincibility() {
        m_Invincible = true;
        m_Animator.SetBool(m_HashDamaged, true);
        GetComponent<PlayerShooting>().DisableShooting();
    }

    public void DisableInvincibility() {
        m_Invincible = false;
        m_Animator.SetBool(m_HashDamaged, false);
        GetComponent<PlayerShooting>().EnableShooting();
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

        if (m_CurHealthAmount <= 0) {
            PlayerDie();
        } else {
            OnDamaged.Invoke();
        }
    }

    //  受到伤害
    public void TakeDamage(int damageAmount) {
        if (m_Invincible) {
            return;
        }
        SetHealth(this.m_CurHealthAmount - damageAmount);

        if (m_DamageEffect != null) {
            Instantiate(m_DamageEffect, transform.position, Quaternion.identity);
        }

        EnableInvincibility();
        Invoke("DisableInvincibility", invincibleDuration);
    }

    //  死亡
    public void PlayerDie() {
        OnDie.Invoke();
        GetComponent<PlayerShooting>().enabled = false;
    }

    //  增加生命值
    public void GainHealth(int gainedAmount) {
        SetHealth(this.m_CurHealthAmount + gainedAmount);
    }
}
