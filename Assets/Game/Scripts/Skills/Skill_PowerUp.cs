using System.Collections;
using UnityEngine;
using System;

public class Skill_PowerUp : SkillController {

    public event Action<int> OnFireSkillWithLevel_PowerUp;

    protected override void Start() {
        base.Start();
        curSkillLevel = 1;
    }

    private void OnEnable() {
        playerSkillControl.Event_PowerUp += OnFireSkill;
    }

    private void OnDisable() {
        playerSkillControl.Event_PowerUp -= OnFireSkill;
    }
    
    protected override void OnFireSkill() {
        //  if the skill is cooling
        if (this.isCooling) {
            Debug.Log("Skill is Cooling...");
            return;
        }
        
        //  if the level has reached to the top value
        if (++curSkillLevel > maxSkillLevel) {
            Debug.Log("skill level has reached the top value");
            return;
        }

        //  do something on firing skill
        if (OnFireSkillWithLevel_PowerUp != null) {
            OnFireSkillWithLevel_PowerUp(this.curSkillLevel);
        }

        //  scale up the level img
        skillImg_Level.GetComponent<RectTransform>().localScale += new Vector3(0.1f, 0.1f, 0.1f);

        //  cool down the skill
        this.Cooling();
    }

    protected override void Cooling() {
        base.Cooling();
    }
}
