using UnityEngine;
using System.Collections;

public class WaterBubbleMove : MonoBehaviour {

    public Transform waterBubble;         //水球
    public Transform particle_waterball;  //水球破碎特效

    public float strength = 1.5f;      //施加力的大小
    public float speed_min = 0.1f;     //水球的最小速度
    public float speed_Burst = 0.2f;   //当手的速度大于此值时水球爆裂
    public float drop_WaitTime = 0.3f; //掉落等待的时间

    Rigidbody bubbleRigibody;

	void Start () {
        bubbleRigibody = this.GetComponent<Rigidbody>();
        AddForceRandom();
    }
	
	void Update () {
        //当运动速度小于指定值时添加力
        if (bubbleRigibody.velocity.magnitude < speed_min) {
            AddForceRandom();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        AddForceNegative(collision.transform.position);
        if (collision.transform.name == "hand") {
            if (collision.transform.GetComponent<Rigidbody>().velocity.magnitude > speed_Burst) {
                WaterBubbleBurst();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        AddForceNegative(collision.transform.position);
    }

    //添加一个随机的力
    void AddForceRandom() {
        bubbleRigibody.AddForce(new Vector3(Random.Range(-strength, strength), Random.Range(-strength, strength), 0));
    }

    //添加反方向的力
    void AddForceNegative(Vector3 giverPos) {
        Vector3 f = this.transform.position - giverPos;
        bubbleRigibody.AddForce(f * strength);
    }

    //水球爆裂
    void WaterBubbleBurst() {
        waterBubble.gameObject.SetActive(false);
        particle_waterball.gameObject.SetActive(true);
        this.GetComponent<SphereCollider>().isTrigger = true;
        StartCoroutine(DropCoroutine());
    }

    IEnumerator DropCoroutine() {
        yield return new WaitForSeconds(drop_WaitTime);
        bubbleRigibody.useGravity = true;
    }
}
