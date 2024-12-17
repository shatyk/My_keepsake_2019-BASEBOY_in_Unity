using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTrigger : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private BonusManager bonusManager;
    [SerializeField] private CashManager cashManager;

    private BoxCollider2D triggerColl;
    private List<ReactTriggered> reactTriggFlyObjTop;
    private List<ReactTriggered> reactTriggFlyObjRight;
    private List<ReactTriggered> reactTriggFlyObjDown;
    private List<ReactTriggered> reactTriggFlyObjLeft;  

    // Start is called before the first frame update
    void Start()
    {
        triggerColl = GetComponent<BoxCollider2D>();
        reactTriggFlyObjTop = new List<ReactTriggered>();
        reactTriggFlyObjRight = new List<ReactTriggered>();
        reactTriggFlyObjDown = new List<ReactTriggered>();
        reactTriggFlyObjLeft = new List<ReactTriggered>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FlyObjChar>() != null)
        {
            ScreenPart scrPrt = collision.gameObject.GetComponent<FlyObjChar>().GetScreenPart();
            switch (scrPrt)
            {
                case ScreenPart.TOP:
                    reactTriggFlyObjTop.Add(collision.gameObject.GetComponent<ReactTriggered>());
                    break;
                case ScreenPart.RIGHT:
                    reactTriggFlyObjRight.Add(collision.gameObject.GetComponent<ReactTriggered>());
                    break;
                case ScreenPart.DOWN:
                    reactTriggFlyObjDown.Add(collision.gameObject.GetComponent<ReactTriggered>());
                    break;
                case ScreenPart.LEFT:
                    reactTriggFlyObjLeft.Add(collision.gameObject.GetComponent<ReactTriggered>());
                    break;
            }
        }
    }

    //////////////////////////////////public///////////////////////////////

    public void Kick(ScreenPart scrPart)
    {
        List<ReactTriggered> reactTriggFlyObj;

        switch (scrPart)
        {
            case ScreenPart.TOP:
                reactTriggFlyObj = reactTriggFlyObjTop;
                break;
            case ScreenPart.RIGHT:
                reactTriggFlyObj = reactTriggFlyObjRight;
                break;
            case ScreenPart.DOWN:
                reactTriggFlyObj = reactTriggFlyObjDown;
                break;
            case ScreenPart.LEFT:
                reactTriggFlyObj = reactTriggFlyObjLeft;
                break;
            default:
                reactTriggFlyObj = null;
                Debug.Log("reactTriggFlyObj is null");
                break;
        }

        foreach(ReactTriggered reactTrigg in reactTriggFlyObj)
        {
            reactTrigg.KickHit();
            
            if (reactTrigg.gameObject.GetComponent<BonusHandl>() != null)
            {
                BonusesVariants bonVar = reactTrigg.gameObject.GetComponent<BonusHandl>().GetTypeBonus();
                bonusManager.ResetProgr(bonVar);
            } else if (reactTrigg.gameObject.GetComponent<CashHandl>() != null)
            {
                cashManager.IncCash(-1);
            } else
            {
                scoreManager.incGameScore(1);
            }
        }
        reactTriggFlyObj.RemoveRange(0, reactTriggFlyObj.Count);
        
    }

    public void removeFromTriggList(ReactTriggered reactTrigg, bool gameOver)
    {
        switch(reactTrigg.gameObject.GetComponent<FlyObjChar>().GetScreenPart())
        {
            case ScreenPart.TOP:
                reactTriggFlyObjTop.Remove(reactTrigg);
                break;
            case ScreenPart.RIGHT:
                reactTriggFlyObjRight.Remove(reactTrigg);
                break;
            case ScreenPart.DOWN:
                reactTriggFlyObjDown.Remove(reactTrigg);
                break;
            case ScreenPart.LEFT:
                reactTriggFlyObjLeft.Remove(reactTrigg);
                break;
        }
        if (gameOver)
        {
            scoreManager.GameOver();
        }
        
    }

}
