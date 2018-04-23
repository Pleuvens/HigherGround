using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject target;
    public int threshold = 0;
    public float floor = -5.5f;

    void Start()
    {
        floor = transform.position.y - 5.5f;
    }

    // Update is called once per frame
    void LateUpdate () {
        IsPlayerDead();
    }

    public float MoveCamera()
    {
        //Debug.Log(Mathf.Abs(transform.position.y - target.transform.position.y));
        //if (Mathf.Abs(transform.position.y - target.transform.position.y) > threshold)
        float dif = this.transform.position.y;
        IsPlayerDead();
        if (target.transform.position.y - threshold > floor && Mathf.Abs(transform.position.y - (target.transform.position.y + threshold)) > 1)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, target.transform.position.y + threshold - 1, -10), Time.deltaTime);
            floor = transform.position.y - 5.5f;
        }
        return Mathf.Abs(dif - this.transform.position.y);
    }

    public bool IsPlayerDead()
    {
        target.GetComponent<PlayerScript>().isDead = target.transform.position.y < floor || target.GetComponent<PlayerScript>().timedeath >= 3;
        return target.transform.position.y < floor;
    }
}