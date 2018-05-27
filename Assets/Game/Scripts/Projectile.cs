using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the damage and defines whether the projectile belongs to the ‘Enemy’ or to the ‘Player’, whether the projectile is destroyed in the collision, or not and amount of damage.
/// </summary>

public class Projectile : MonoBehaviour {

    [Tooltip("Damage which a projectile deals to another object. Integer")]
    public int damage;

    [Tooltip("Whether the projectile belongs to the ‘Enemy’ or to the ‘Player’")]
    public bool enemyBullet;

    [Tooltip("Whether the projectile is destroyed in the collision, or not")]
    public bool destroyedByCollision;

    public GameObject m_HitBossEffect;

    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        //  该子弹为敌人子弹
        if (enemyBullet)
        {
            //  击中玩家
            if (collision.tag == "Player") {
                //  玩家受到伤害
                Player.instance.GetDamage(damage);

                //  子弹自毁
                if (destroyedByCollision)
                    Destruction();
            }
            //  接触到大招物体
            else if (collision.tag == "Ultimate") {
                //  直接销毁
                Destruction();
            }
            
        }

        //  该子弹为玩家子弹
        else if (!enemyBullet)
        {
            //  击中敌人
            if (collision.tag == "Enemy") {
                //  敌人受到伤害
                if (collision.GetComponent<Enemy>() != null) {
                    collision.GetComponent<Enemy>().GetDamage(damage);
                }
            }
            
            //  击中boss
            else if (collision.tag == "Boss") {
                //   boss受到伤害
                if (collision.GetComponent<BossHealth>() != null) {
                    collision.GetComponent<BossHealth>().TakeDamage(damage);
                    PoolManager.GetInstance.ReuseObject(m_HitBossEffect, transform.position, Quaternion.identity);
                    Debug.Log("reuse HitBossEffect");
                    Destruction();
                }
            }
        }
    }

    void Destruction() 
    {
        if (enemyBullet) {
            Destroy(gameObject);
        } else {
            if (GetComponent<PlayerProjectilePoolObject>() != null) {
                GetComponent<PlayerProjectilePoolObject>().Destroy();
            }
        }
    }
}