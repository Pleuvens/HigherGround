using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public bool IsAiming = false;
    public LineRenderer lRenderer;
    public Vector3 mousePos = Vector3.zero;
    public bool isDead = false;
    public GameObject lvlManager;
    public Camera cam;
    bool hasStarted = false;
    public bool landing = false;
    GameObject curB = null;
    public bool isSwitch = false;
    float prevHeight = 0;
    public bool jumping = false;
    public float timedeath = 0;

    void Start()
    {
        prevHeight = transform.position.y;
        PlayerPrefs.DeleteAll();
    }

    void Update()
    {
        if (timedeath > 0)
            timedeath += Time.deltaTime;
        if (isDead)
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }
        if (isSwitch && Mathf.Abs(prevHeight - this.transform.position.y) < 0.01)
        {
            landing = false;
            jumping = false;
        }
        prevHeight = this.transform.position.y;
        lRenderer.gameObject.SetActive(false);
        if (landing)
            return;
        if (!jumping && cam.gameObject.GetComponent<CameraFollow>().MoveCamera() > 0.1)
        {
            return;
        }
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButton("Fire1") && (IsAiming || (Mathf.Abs(mPos.x - transform.position.x) < 0.23 && Mathf.Abs(mPos.y - transform.position.y) < 0.23)))
        {
            lRenderer.gameObject.SetActive(true);
            IsAiming = true;
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            mousePos = mousePos - transform.position;
            if (mousePos.y > 2f)
                mousePos.y = 2f;
            if (mousePos.y < -2f)
                mousePos.y = -2f;
            mousePos *= -1;
            float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * 200;
            Vector3 lrendpos = new Vector3(Mathf.Cos(angle) * 0.004f, Mathf.Sin(angle) * 0.004f, Mathf.Tan(angle) * 0.004f) + transform.position;
            lRenderer.SetPosition(0,  lrendpos);
            lRenderer.SetPosition(1, mousePos + lrendpos);
        } else if (IsAiming)
        {
            if (mousePos.y > 0)
            {
                this.GetComponent<Rigidbody2D>().AddForce(mousePos * 275f);
                //curB.SetActive(false);
                jumping = true;
            }
            IsAiming = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Basket")
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            isSwitch = true;
            if (curB == null || collider.gameObject != curB)
            {
                curB = collider.gameObject;
                if (!hasStarted)
                {
                    hasStarted = true;
                    return;
                }
                lvlManager.GetComponent<LevelManager>().UpdateMap();
            }
        }
        if (collider.gameObject.tag == "Finish")
        {
            timedeath += Time.deltaTime;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Basket")
        {
            landing = true;
            isSwitch = false;
        }
        if (collider.gameObject.tag == "Finish")
        {
            timedeath = 0;
        }
    }
}
