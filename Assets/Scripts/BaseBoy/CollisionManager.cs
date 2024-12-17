using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private ManagerTrigger managerTrigg;
    [SerializeField] private BonusManager managerBonus;
    [SerializeField] private CashManager managerCash;
    [SerializeField] private ScoreManager managerScore;

    private Animator animator;
    private Coroutine coroutine = null;

    private bool isShieldBonus = false;
    private double time = 0;
    private int howManyItemsShieldProtect;
    private int howManyCoinsInc;

    // Start is called before the first frame update
    void Start()
    {
        howManyCoinsInc = 1;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("On collision, time: " + (time + Time.deltaTime));
        time = 0;

        GameObject gameObjDestroy = collision.gameObject;

        if (collision.gameObject.GetComponent<BonusHandl>() != null)
        {
            BonusesVariants bonusVar = collision.gameObject.GetComponent<BonusHandl>().GetTypeBonus();
            managerBonus.IncProgr(bonusVar);
            managerTrigg.removeFromTriggList(collision.gameObject.GetComponent<ReactTriggered>(), false);
            Destroy(gameObjDestroy);
        } else if (collision.gameObject.GetComponent<CashHandl>() != null)
        {
            managerCash.IncCash(howManyCoinsInc);
            managerTrigg.removeFromTriggList(collision.gameObject.GetComponent<ReactTriggered>(), false);
            Destroy(gameObjDestroy);
        } else
        {
            if (isShieldBonus)
            {
                if(--howManyItemsShieldProtect == 0)
                {
                    StopShieldBonus();
                }
                Debug.Log("protect " + (howManyItemsShieldProtect) + " items");
                managerTrigg.removeFromTriggList(collision.gameObject.GetComponent<ReactTriggered>(), false);
                Destroy(gameObjDestroy);

            } else
            {
                MoveFlyObj moveFlyObj = collision.gameObject.GetComponent<MoveFlyObj>();
                moveFlyObj.IsEnterSetTrue();
                managerTrigg.removeFromTriggList(collision.gameObject.GetComponent<ReactTriggered>(), true);
                moveFlyObj.DisableCollision();
                collision.gameObject.GetComponent<Animator>().enabled = false;
                StartCoroutine(CoroutineDie(gameObjDestroy));
            }
        }      
    } */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("On collision, time: " + (time + Time.deltaTime));
        time = 0;

        GameObject gameObjDestroy = collision.gameObject;

        if (collision.gameObject.GetComponent<TouchController>() != null)
        {
            return;
        }
        if (collision.gameObject.GetComponent<BonusHandl>() != null)
        {
            BonusesVariants bonusVar = collision.gameObject.GetComponent<BonusHandl>().GetTypeBonus();
            managerBonus.IncProgr(bonusVar);
            managerTrigg.removeFromTriggList(collision.gameObject.GetComponent<ReactTriggered>(), false);
            Destroy(gameObjDestroy);
        }
        else if (collision.gameObject.GetComponent<CashHandl>() != null)
        {
            managerCash.IncCash(howManyCoinsInc);
            managerTrigg.removeFromTriggList(collision.gameObject.GetComponent<ReactTriggered>(), false);
            Destroy(gameObjDestroy);
        }
        else
        {
            if (isShieldBonus)
            {
                if (--howManyItemsShieldProtect == 0)
                {
                    StopShieldBonus();
                }
                Debug.Log("protect " + (howManyItemsShieldProtect) + " items");
                managerTrigg.removeFromTriggList(collision.gameObject.GetComponent<ReactTriggered>(), false);
                Destroy(gameObjDestroy);

            }
            else
            {
                MoveFlyObj moveFlyObj = collision.gameObject.GetComponent<MoveFlyObj>();
                moveFlyObj.IsEnterSetTrue();
                managerTrigg.removeFromTriggList(collision.gameObject.GetComponent<ReactTriggered>(), false); //true
                moveFlyObj.DisableCollision();
                collision.gameObject.GetComponent<Animator>().enabled = false;
                StartCoroutine(CoroutineDie(gameObjDestroy));
                managerScore.incGameScoreFail(1);
            }
        }
    }

    public void StartShieldBonus(float durationShieldBonus, int _howManyItemsShieldProtect)
    {
        if (coroutine != null) StopShieldBonus();
        howManyItemsShieldProtect = _howManyItemsShieldProtect;
        isShieldBonus = true;
        Debug.Log("Start Shield Bonus, " + durationShieldBonus + "sec, protect " + howManyItemsShieldProtect + " items");
        coroutine = StartCoroutine(CouroutineShield(durationShieldBonus));      
    }

    private void StopShieldBonus()
    {
        StopCoroutine(coroutine);
        coroutine = null;
        Debug.Log("Stop Shield Bonus (StopCoroutine)");
        isShieldBonus = false;
    }

    private IEnumerator CoroutineDie(GameObject gObj)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gObj);
    }

    private IEnumerator CouroutineShield(float durationShieldBonus)
    {
        /*for (int i = durationShieldBonus; i!=0; i--)
        {
            //Debug.Log("Shield " + i + " sec...");
            yield return new WaitForSeconds(1f);
        }*/

        yield return new WaitForSeconds(durationShieldBonus);
        Debug.Log("Stop Shield Bonus");
        howManyItemsShieldProtect = 0;
        isShieldBonus = false;
    }

    public void StartCoinBonus(float durCoinBonus, int howManyCoinsInc)
    {
        StartCoroutine(CoroutineCoin(durCoinBonus, howManyCoinsInc));
    }

    private IEnumerator CoroutineCoin(float _durCoinBonus, int _howManyCoinsInc)
    {
        int oldHowManyCoinsInc = howManyCoinsInc;
        howManyCoinsInc = _howManyCoinsInc;
        Debug.Log("Start Bonus Coin, dur: " + _durCoinBonus + ", new Cash bonus: " + _howManyCoinsInc);
        yield return new WaitForSeconds(_durCoinBonus);
        Debug.Log("Stop Bonus Coin");
        howManyCoinsInc = oldHowManyCoinsInc;
    }
}
