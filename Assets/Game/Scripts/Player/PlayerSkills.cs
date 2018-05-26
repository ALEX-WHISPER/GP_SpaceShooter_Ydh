using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour {

    public Button btn_PowerUp;
    public Button btn_Ulti_Diamond;
    public Button btn_Ulti_Medic;
    public Button btn_RateUp;

    public event Action Event_PowerUp;
    public event Action Event_Ulti_Diamond;
    public event Action Event_Ulti_Medic;
    public event Action Event_RateUp;

    private void OnEnable() {
        //  add btns onclick listeners
        if (btn_PowerUp != null) btn_PowerUp.onClick.AddListener(FireSkill_PowerUp);
        if (btn_Ulti_Diamond != null) btn_Ulti_Diamond.onClick.AddListener(FireSkill_Ulti_Diamond);
        if (btn_Ulti_Medic != null) btn_Ulti_Medic.onClick.AddListener(FireSkill_Ulti_Medic);
        if (btn_RateUp != null) btn_RateUp.onClick.AddListener(FireSkill_RateUp);
    }

    private void OnDisable() {
        //  remove btns onclick listeners
        if (btn_PowerUp != null) btn_PowerUp.onClick.RemoveListener(FireSkill_PowerUp);
        if (btn_Ulti_Diamond != null) btn_Ulti_Diamond.onClick.RemoveListener(FireSkill_Ulti_Diamond);
        if (btn_Ulti_Medic != null) btn_Ulti_Medic.onClick.RemoveListener(FireSkill_Ulti_Medic);
        if (btn_RateUp != null) btn_RateUp.onClick.RemoveListener(FireSkill_RateUp);
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

    private void FireSkill_Ulti_Medic() {
        if (Event_Ulti_Medic != null) {
            Event_Ulti_Medic();
        }
    }

    private void FireSkill_RateUp() {
        if (Event_RateUp != null) {
            Event_RateUp();
        }
    }
}
