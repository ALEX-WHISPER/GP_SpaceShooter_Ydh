﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum SkillType {
    POWER_UP,
    ULTI_DIAMOND,
}

public abstract class SkillController: MonoBehaviour {
    public SkillType m_SkillType;
    public Image skillImg_Main;
    public float coolingDuration;
    public KeyCode skillFireKey;
    public Animator m_Animator;

    private bool skillEnabled = false;

    public void EnableSkill(SkillType skillType) {
        if (skillType == m_SkillType) {
            skillEnabled = true;

            skillImg_Main.fillAmount = 1f;
            m_Animator.SetBool(m_HashConsuming, false);
        }
    }

    public void DisableSkill(SkillType skillType) {
        if (skillType == m_SkillType) {
            skillEnabled = false;

            skillImg_Main.fillAmount = 0f;
            m_Animator.SetBool(m_HashConsuming, true);
        }
    }

    public bool SkillEnabled {
        get { return this.skillEnabled; }
    }

    protected int curSkillLevel;
    protected PlayerSkills playerSkillControl;
    protected bool isCooling = false;
    protected readonly int m_HashConsuming = Animator.StringToHash("consume");

    protected virtual void Awake() {
        playerSkillControl = GameObject.FindWithTag("Player").GetComponent<PlayerSkills>();
    }

    protected virtual void OnEnable() {
        Bonus.OnGetSkill += EnableSkill;
    }

    protected virtual void OnDisable() {
        Bonus.OnGetSkill -= EnableSkill;
    }

    protected virtual void Start() {
        DisableSkill(this.m_SkillType);
    }

#if UNITY_EDITOR
    protected virtual void Update() {
        if (Input.GetKeyUp(skillFireKey)) {
            OnFireSkill();
        }
    }
#endif

    protected abstract void OnFireSkill();

    protected virtual void Cooling() {
        StartCoroutine(StartCoolingProcess());
    }

    protected IEnumerator StartCoolingProcess() {
        isCooling = true;
        float elapsedTime = 0;
        skillImg_Main.fillAmount = 0;

        m_Animator.SetBool(m_HashConsuming, true);
        while (elapsedTime < coolingDuration) {
            elapsedTime += Time.deltaTime;
            skillImg_Main.fillAmount = elapsedTime / coolingDuration;

            yield return null;
        }
        m_Animator.SetBool(m_HashConsuming, false);
        isCooling = false;
    }
}
