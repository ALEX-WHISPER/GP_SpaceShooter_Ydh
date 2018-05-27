using System.Collections;
using UnityEngine;

public class BossBehaviour : MonoBehaviour {
    public float moveToDuration = 3f;
    public Vector3 moveFrom;
    public Vector3 moveTo;

    private float m_TotalDistance;
    private float m_CurDistance;
    private float m_MoveToSpeed;
    private bool m_Invincible = false;

    private void Start() {
        transform.position = moveFrom;
        m_TotalDistance = Vector2.Distance(transform.position, moveTo);
        m_CurDistance = m_TotalDistance;
        m_MoveToSpeed = m_TotalDistance / moveToDuration;

        Debug.Log(string.Format("totalDistance: {0}, curDistance: {1}, moveSpeed: {2}", m_TotalDistance, m_CurDistance, m_MoveToSpeed));

        StartCoroutine(MoveTo(moveTo));
    }

    IEnumerator MoveTo(Vector3 moveToPos) {
        EnableInvicibility();

        while (m_CurDistance > 0.01f) {
            transform.position = Vector2.MoveTowards(transform.position, moveToPos, m_MoveToSpeed * Time.deltaTime);
            m_CurDistance = Vector2.Distance(transform.position, moveTo);

            yield return null;
        }

        DisableInvicibility();
    }

    public bool Invicible { get { return this.m_Invincible; } }

    public void EnableInvicibility() {
        m_Invincible = true;
        Debug.Log("Enable invicibility");
    }

    public void DisableInvicibility() {
        m_Invincible = false;
        Debug.Log("Disable invicibility");
    }

    public void BossDefeated() {
        StartCoroutine(MoveTo(moveFrom));
    }
}
