using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour {

    public Button btn_PowerUp;
    public Button btn_Ulti_Diamond;
    public Button btn_SkillC;

    public event Action Event_PowerUp;
    public event Action Event_Ulti_Diamond;
    public event Action Event_SkillC;

    private void OnEnable() {
        //  add btns onclick listeners
        if (btn_PowerUp != null) btn_PowerUp.onClick.AddListener(FireSkill_PowerUp);
        if (btn_Ulti_Diamond != null) btn_Ulti_Diamond.onClick.AddListener(FireSkill_Ulti_Diamond);
        if (btn_SkillC != null) btn_SkillC.onClick.AddListener(FireSkill_C);
    }

    private void OnDisable() {
        //  remove btns onclick listeners
        if (btn_PowerUp != null) btn_PowerUp.onClick.RemoveListener(FireSkill_PowerUp);
        if (btn_Ulti_Diamond != null) btn_Ulti_Diamond.onClick.RemoveListener(FireSkill_Ulti_Diamond);
        if (btn_SkillC != null) btn_SkillC.onClick.RemoveListener(FireSkill_C);
    }

    private void FireSkill_PowerUp() {
        if (Event_PowerUp != null) {
            Event_PowerUp();
        }
    }

    private void FireSkill_Ulti_Diamond() {
        if (Event_Ulti_Diamond != null) {
            Event_Ulti_Diamond();
        }
    }

    private void FireSkill_C() {
        if (Event_SkillC != null) {
            Event_SkillC();
        }
    }
}
