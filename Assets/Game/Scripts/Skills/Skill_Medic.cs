using System.Collections;
using UnityEngine;
using System;

public class Skill_Medic : SkillController {
    public int recoveringValue = 1;
    public event Action<int> FireSkill_UltiMedic;

    protected override void OnEnable() {
        base.OnEnable();
        playerSkillControl.Event_Ulti_Medic += OnFireSkill;
    }

    protected override void OnDisable() {
        base.OnDisable();
        playerSkillControl.Event_Ulti_Medic -= OnFireSkill;
    }

    protected override void OnFireSkill() {
        if (!SkillEnabled || this.isCooling) { return; }

        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null) {
            playerHealth.GainHealth(this.recoveringValue);
        }

        Cooling();
    }
}
