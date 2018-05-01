using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillController: MonoBehaviour {
    public Image skillImg_Main;
    public Image skillImg_Level;

    public float coolingDuration;
    public int maxSkillLevel;

    protected int curSkillLevel;
    protected PlayerSkills playerSkillControl;
    protected bool isCooling = false;

    protected virtual void Awake() {
        playerSkillControl = GameObject.FindWithTag("Player").GetComponent<PlayerSkills>();
    }

    protected virtual void Start() {
        skillImg_Level.color = new Color(skillImg_Level.color.r, skillImg_Level.color.g, skillImg_Level.color.b, 255f / 255f);
    }
    protected abstract void OnFireSkill();
    protected virtual void Cooling() {
        StartCoroutine(StartCoolingProcess());
    }

    protected IEnumerator StartCoolingProcess() {
        isCooling = true;
        float elapsedTime = 0;
        skillImg_Main.fillAmount = 0;

        while (elapsedTime < coolingDuration) {
            elapsedTime += Time.deltaTime;
            skillImg_Main.fillAmount = elapsedTime / coolingDuration;

            yield return null;
        }
        isCooling = false;
    }
}
