using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitation : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float levitationBound; 
    [SerializeField] private float timeToReturnToKickPos; 
    [SerializeField] private float timeStopAfterReturn;

    private Rigidbody2D rbPlayer;

    private bool isUp = true;
    private bool isKick = false;
    private float kickMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float spd = (0.4f - Mathf.Abs(rbPlayer.position.y)) * moveSpeed;
        if (!isKick) {
            Vector2 v2 = rbPlayer.position + (isUp ? Vector2.up : Vector2.down) * spd * Time.fixedDeltaTime;
            rbPlayer.MovePosition(v2);
            if (rbPlayer.position.y > levitationBound)
            {
                isUp = !isUp;
                rbPlayer.MovePosition(new Vector2(0, levitationBound));
            } else if (rbPlayer.position.y < -levitationBound)
            {
                isUp = !isUp;
                rbPlayer.MovePosition(new Vector2(0, -levitationBound));
            }
        } else
        {
            Vector2 v2 = rbPlayer.position + (isUp ? Vector2.up : Vector2.down) * kickMoveSpeed * Time.fixedDeltaTime;
            rbPlayer.MovePosition(v2);
        }
    }

    private IEnumerator KickTimer()
    {
        yield return new WaitForSeconds(timeToReturnToKickPos);
        kickMoveSpeed = 0;
        yield return new WaitForSeconds(timeStopAfterReturn);
        isKick = false;
    }

    /////////////////////////////////////////////////////public///////////////////////////////////

    public void Kick()
    {
        kickMoveSpeed = Mathf.Abs(rbPlayer.position.y / timeToReturnToKickPos);
        isKick = true;
        isUp = rbPlayer.position.y < 0 ? true : false;
        StartCoroutine(KickTimer());
    }
}
