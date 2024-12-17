using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Levitation playerLevitation;
    [SerializeField] private bool isPhone;

    private Vector2 touchPos;
    private ScreenPart screenPart;  
    private ManagerTrigger managerTrigger;

    private bool canKick;
    private double aspectRatio;

    // Start is called before the first frame update
    void Start()
    {
        aspectRatio = (double) Screen.height / Screen.width;
        managerTrigger = GetComponent<ManagerTrigger>();
        canKick = true;
        animator.SetInteger("ScreenPartAP", 4);
        //animator.runtimeAnimatorController.animationClips[0].
    }

    // Update is called once per frame
    void Update()
    {
        if (isPhone)
        {

            if (Input.touchCount > 0 && canKick)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.position.y > Screen.height * 0.80f) return;

                if(touch.phase == TouchPhase.Began)
                {
                    touchPos = touch.position;
                    int aspectWidth = (int) (touchPos.x * aspectRatio);

                    if (aspectWidth > touchPos.y)
                    {
                        aspectWidth = (int)((Screen.width - touchPos.x) * aspectRatio);
                        if (aspectWidth > touchPos.y)
                        {
                            screenPart = ScreenPart.DOWN;
                        }
                        else
                        {
                            screenPart = ScreenPart.RIGHT;
                        }
                    }
                    else
                    {
                        aspectWidth = (int)((Screen.width - touchPos.x) * aspectRatio);               
                        if (aspectWidth > touchPos.y)
                        {
                            screenPart = ScreenPart.LEFT;
                        }
                        else
                        {
                            screenPart = ScreenPart.TOP;
                        }
                    }

                    StartCoroutine(KickCoroutine());
                    playerLevitation.Kick();
                }
            } 

        } else
        {

            if (canKick)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow)) //в зависимости от нажатой кнопки, запускаем функцию
                {
                    screenPart = ScreenPart.TOP;
                    StartCoroutine(KickCoroutine());
                    playerLevitation.Kick();
                }
                else
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    screenPart = ScreenPart.DOWN;
                    StartCoroutine(KickCoroutine());
                    playerLevitation.Kick();
                }
                else
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    screenPart = ScreenPart.LEFT;
                    StartCoroutine(KickCoroutine());
                    playerLevitation.Kick();
                }
                else
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    screenPart = ScreenPart.RIGHT;
                    StartCoroutine(KickCoroutine());
                    playerLevitation.Kick();
                }

            } 

        }

    }

    private IEnumerator KickCoroutine()
    {
        canKick = false;
        animator.SetInteger("ScreenPartAP", (int)screenPart);
        yield return new WaitForSeconds(0.15f / animator.GetFloat("AnimSpeed"));
        managerTrigger.Kick(screenPart);
        yield return new WaitForSeconds(0.15f / animator.GetFloat("AnimSpeed"));
        animator.SetInteger("ScreenPartAP", 4);
        yield return new WaitForSeconds(0.033334f / animator.GetFloat("AnimSpeed"));
        canKick = true;
    }

}
