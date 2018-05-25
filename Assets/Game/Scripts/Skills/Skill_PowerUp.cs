using System.Collections;
using UnityEngine;
using System;

public class Skill_PowerUp : SkillController {

    public event Action<int> OnFireSkillWithLevel_PowerUp;
    public int maxSkillLevel;

    protected override void Start() {
        base.Start();
        curSkillLevel = 1;
    }

    protected override void OnEnable() {
        base.OnEnable();
        playerSkillControl.Event_PowerUp += OnFireSkill;
    }

    protected override void OnDisable() {
        base.OnDisable();
        playerSkillControl.Event_PowerUp -= OnFireSkill;
    }
    
    protected override void OnFireSkill() {
        if (!SkillEnabled || this.isCooling || ++curSkillLevel > maxSkillLevel) { return; }
        
        //  do something on firing skill
        if (OnFireSkillWithLevel_PowerUp != null) {
            OnFireSkillWithLevel_PowerUp(this.curSkillLevel);
        }

        //  cool down the skill
        Cooling();
    }
}
