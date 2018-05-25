using UnityEngine;

public class AsteroidPoolObject : PoolObject {
    
    public override void OnObjectReuse() {
        ResetMotion();

        base.OnObjectReuse();
    }

    private void ResetMotion() {
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
