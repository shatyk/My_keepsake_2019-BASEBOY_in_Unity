using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFlyObj : MonoBehaviour
{
    private Vector2 targetPoint;
    private ScreenPart screenPart;
    private Rigidbody2D rb2D;
    private Vector2 targetForward;

    private float speedFlyObj;
    private bool isEnter = false;

    // Start is called before the first frame update
    void Start()
    {
        screenPart = GetComponent<FlyObjChar>().GetScreenPart();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!isEnter)
        {
            Vector2 v2 = rb2D.position + targetForward * speedFlyObj * Time.fixedDeltaTime;
            rb2D.MovePosition(v2);         
        }
    }

    ////////////////////////////////////////public////////////////////////////////

    public void SetTargPointAndCalcForw(Vector2 _targPoint)
    {
        targetPoint = _targPoint;
        targetForward = transform.InverseTransformPoint(targetPoint);
        targetForward = transform.TransformDirection(targetForward);
        targetForward = targetForward.normalized;
    }

    public void IsEnterSetTrue()
    {
        isEnter = true;
    }

    public void DisableCollision()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 1.5f;
    }

    public void SetSpeedObj(float spd)
    {
        speedFlyObj = spd;
    }
}
