using System.Collections;
using UnityEngine;
using System;

public class AsteroidBehaviour : MonoBehaviour {

    #region Modifiable Public Variables
    //  事件：当陨石被销毁（被玩家子弹击中、与玩家发生碰撞，运动至边界）时触发
    public static event Action AsteroidReuse;

    //  旋转速度
	public float tumble;

    //  下落速度
    public float fallingSpeed;
    
    //  被子弹击中后的爆炸效果
    public GameObject explosion;

    //  与玩家碰撞到的爆炸效果
    public GameObject playerExplosion;
    #endregion

    #region Private
    //  pool object, for destroy
    private AsteroidPoolObject m_AsteroidPoolObject;

    //  static instance
    private static AsteroidBehaviour _instance;
    #endregion

    //  static instacne getter
    public static AsteroidBehaviour GetInstace {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<AsteroidBehaviour>();
            }
            return _instance;
        }
    }

    #region Unity life cycle
    private void Awake() {
        m_AsteroidPoolObject = GetComponent<AsteroidPoolObject>();
    }

    //  陨石一被激活即开始运动
    private void OnEnable() {
        RandomRotate(); //  旋转
        FallingDown();  //  下落
    }
    #endregion

    #region Contact Destroy

    //  被玩家子弹击中
    public void GetDamageFromPlayerDamager() {
        if (explosion != null) {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        DestructableHitting();
    }

    //  与玩家发生碰撞
    public void GetContactHitWithPlayer() {
        if (playerExplosion != null) {
            Instantiate(playerExplosion, transform.position, transform.rotation);
        }
        FindObjectOfType<PlayerHealth>().TakeDamage(1);
        DestructableHitting();
    }

    //  位置超出边界
    public void GetDestroyOnExitBoundary() {
        DestructableHitting();
    }

    private void DestructableHitting() {
        m_AsteroidPoolObject.Destroy(); //  使其不可见
        CallOnDestroy();    //  触发事件
    }
    
    private void CallOnDestroy() {
        if (AsteroidReuse != null) {
            AsteroidReuse();
        }
    }
    #endregion

    #region Movement
    private void RandomRotate() {
        GetComponent<Rigidbody>().angularVelocity = UnityEngine.Random.insideUnitSphere * tumble;
    }

    private void FallingDown() {
        GetComponent<Rigidbody>().velocity = transform.up * fallingSpeed;
    }
    #endregion
}
