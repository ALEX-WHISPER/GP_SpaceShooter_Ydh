using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour {

    public Button btn_PowerUp;
    public Button btn_SkillB;
    public Button btn_SkillC;

    public event Action Event_PowerUp;
    public event Action Event_SkillB;
    public event Action Event_SkillC;

    private void OnEnable() {
        //  add btns onclick listeners
        btn_PowerUp.onClick.AddListener(FireSkill_PowerUp);
        btn_SkillB.onClick.AddListener(FireSkill_B);
        btn_SkillC.onClick.AddListener(FireSkill_C);
    }

    private void OnDisable() {
        //  remove btns onclick listeners
        btn_PowerUp.onClick.RemoveListener(FireSkill_PowerUp);
        btn_SkillB.onClick.RemoveListener(FireSkill_B);
        btn_SkillC.onClick.RemoveListener(FireSkill_C);
    }

    private void FireSkill_PowerUp() {
        if (Event_PowerUp != null) {
            Event_PowerUp();
        }
    }

    private void FireSkill_B() {
        if (Event_SkillB != null) {
            Event_SkillB();
        }
    }

    private void FireSkill_C() {
        if (Event_SkillC != null) {
            Event_SkillC();
        }
    }
}
