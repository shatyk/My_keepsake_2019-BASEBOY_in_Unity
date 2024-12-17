using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnFlyingObject : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private BoxCollider2D playerBoxColider;
    [SerializeField] private CdSlider sliderCd;
    [SerializeField] private SpeedSlider sliderSpd;
    [SerializeField] private RandSlider sliderRand;

    [SerializeField] private GameObject[] flyingItemPrefabs;      
    [SerializeField] private GameObject[] flyingBonusPrefabs;  //порядок строго как в BonusesVariants
    [SerializeField] private GameObject flyingCashPrefab;
    [SerializeField] private Vector2[] batPointTop; //0 - start, 1 - end 
    [SerializeField] private Vector2[] batPointDown;
    [SerializeField] private Vector2[] batPointLeft;
    [SerializeField] private Vector2[] batPointRight;
    [SerializeField] private float cdItemToPlayerStart;
    [SerializeField] private float speedFlyObjStart;
    [SerializeField] private float boundsRandCdStart;

    [SerializeField] private int percentSpawnBonuses; //в процентах спавн бонусов
    [SerializeField] private int percentSpawnCash; //в процентах спавн кеша

    private GameObject flyingItemObj;
    private ScreenPart screenPart;
    private TimeScalerCamera timeScaler;
    private Vector2 targetPoint;

    private List<int> enabledItemsNumbers;
    private float timeCounterSpawn;
    private float scaleSpd;
    private float delayBetwSpawn;
    private float incrInFlyTime;
    private float imaginaryDist = 0;
    private bool isChengeSpecif = false;
    private float cdItemToPlayer;
    private float speedFlyObj;
    private float boundsRandCd;
    private bool isChange;

    private float oldCdItemToPlayer;
    private float oldSpeedFlyObj;
    private int countUpdate;

    // Start is called before the first frame update
    void Start()
    {
        
        countUpdate = 0;
        timeScaler = gameCamera.GetComponent<TimeScalerCamera>();
        scaleSpd = timeScaler.retScaleSpeed();      
        timeCounterSpawn = 2;

        batPointDown[0].y = -(batPointTop[0].y = playerBoxColider.size.y / 2f);
        batPointLeft[0].x = -(batPointRight[0].x = playerBoxColider.size.x / 2f);
    
        sliderSpd.SetValue(speedFlyObjStart);
        sliderCd.SetValue(cdItemToPlayerStart);
        sliderRand.SetValue(boundsRandCdStart);

        enabledItemsNumbers = new List<int>();
        StartCoroutine(CouroutineFindEnabledItems(0));
        delayBetwSpawn = 0;
        isChange = false;
        sliderSpd.Enabled(true);
        sliderCd.Enabled(true);
    }

    // Update is called once per frame
    void Update()
    {        
        timeCounterSpawn -= Time.deltaTime;

        if (timeCounterSpawn <= 0)
        {
            int randSpawnItemVal = Random.Range(0, 100);

            if (randSpawnItemVal < percentSpawnBonuses)
            {
                int randValBonus = Random.Range(0, flyingBonusPrefabs.Length);
                flyingItemObj = Instantiate(flyingBonusPrefabs[randValBonus]) as GameObject;
                flyingItemObj.GetComponent<BonusHandl>().SetTypeBonus((BonusesVariants)randValBonus);
            }
            else if (randSpawnItemVal < (percentSpawnBonuses + percentSpawnCash))
            {
                flyingItemObj = Instantiate(flyingCashPrefab) as GameObject;
            }
            else
            {
                int numberOfItem = enabledItemsNumbers[Random.Range(0, enabledItemsNumbers.Count)];
                flyingItemObj = Instantiate(flyingItemPrefabs[numberOfItem]) as GameObject;
                //Debug.Log("Started Coroutine");
                StartCoroutine(CouroutineFindEnabledItems(delayBetwSpawn));
            }

            if (imaginaryDist == 0)
            {
                CalcFlyObjSpecif();
            }

            float spawnX, spawnY;

            screenPart = (ScreenPart) Random.Range(0,4);
            
            DetermineSpawnPlaceAndTargPoint(out spawnX, out spawnY);


            imaginaryDist += (scaleSpd * delayBetwSpawn);
            float distToPlayerCollision = CalcDistToPlayerCollision(spawnX, spawnY);

            float randVal = Random.Range(-boundsRandCd, boundsRandCd);

            //float realSpeed = (speedFlyObj * distToPlayerCollision) / imaginaryDist;
            float realSpeed = ((isChange ? oldSpeedFlyObj : speedFlyObj) * distToPlayerCollision) / imaginaryDist;
            float oldSpd = realSpeed;
            float newFlyTime = (distToPlayerCollision / realSpeed) + (isChange ? 0 : randVal);
            //Debug.Log("newFlyTime: " + newFlyTime);
            realSpeed = distToPlayerCollision / newFlyTime;

            //Debug.Log("newFlyTime: " + newFlyTime + "; distToPlayerCollision: " + distToPlayerCollision);
            //Debug.Log("randVal: " + randVal + "; realSpd: " + realSpeed);

            flyingItemObj.transform.position = new Vector3(spawnX, spawnY, 1);
            flyingItemObj.GetComponent<FlyObjChar>().SetScreenPart(screenPart);
            flyingItemObj.GetComponent<MoveFlyObj>().SetTargPointAndCalcForw(targetPoint);            
            flyingItemObj.GetComponent<MoveFlyObj>().SetSpeedObj(realSpeed);

            /*Debug.Log("speed: " + (speedFlyObj * distToPlayerCollision) / imaginaryDist +
                "; real dist: " + distToPlayerCollision + "; imaginary dist: " + imaginaryDist +
                "; incrInFlyTime: " + incrInFlyTime); */

            timeCounterSpawn += delayBetwSpawn;
            countUpdate = -1;

            if (isChange)
            {
                //Debug.Log("TimeCounter: " + timeCounterSpawn);
                float futureRealSpeed = (speedFlyObj * distToPlayerCollision) / imaginaryDist;
                float futureFlyTime = (distToPlayerCollision / futureRealSpeed);
                float timeDiff = newFlyTime - futureFlyTime;
                timeCounterSpawn += timeDiff < -timeCounterSpawn+0.1f ? -timeCounterSpawn+0.1f : timeDiff;
                CalcFlyObjSpecif();
                //Debug.Log("TimeCounter: " + timeCounterSpawn + " (futureFlyTime: " + futureFlyTime + ", delayBetwSpawn: " + delayBetwSpawn + ")");
                isChange = false;
                sliderSpd.Enabled(true);
                sliderCd.Enabled(true);
                //Debug.Log("True - Update");
            }
        }
        countUpdate++;
    }

    private float CalcDistToPlayerCollision(float spawnX, float spawnY)
    {
        float distToTargPoint, distToCollider, radius;
        radius = flyingItemObj.GetComponent<CircleCollider2D>().radius;
        if (screenPart == ScreenPart.TOP || screenPart == ScreenPart.DOWN)
        {
            distToCollider = Mathf.Abs(spawnY - targetPoint.y);
        } else
        {
            distToCollider = Mathf.Abs(spawnX - targetPoint.x);
        }
        distToTargPoint = Mathf.Sqrt( Mathf.Pow(spawnX - targetPoint.x, 2) + Mathf.Pow(spawnY - targetPoint.y, 2) );
        return distToTargPoint - ((distToTargPoint * radius) / distToCollider);
    }

    private void DetermineSpawnPlaceAndTargPoint(out float spawnX, out float spawnY)
    {
        Vector2 batStart, batEnd;
        float radiusOfCollider = flyingItemObj.GetComponent<CircleCollider2D>().radius;
        float maxSpBord;
        Vector2 maxMinSpawnBord, batStartBord;
        int randVal = Random.Range(0, 21);

        switch (screenPart)
        {
            case ScreenPart.TOP:
                {
                    Vector2 bound = new Vector2(0, gameCamera.pixelHeight);
                    spawnX = 0;
                    spawnY = gameCamera.ScreenToWorldPoint(bound).y + radiusOfCollider + scaleSpd * timeCounterSpawn;
                    batStart = batPointTop[0];
                    batEnd = batPointTop[1];
                    break;
                }
            case ScreenPart.DOWN:
                {
                    Vector2 bound = new Vector2(0, 0);
                    spawnX = 0;
                    spawnY = gameCamera.ScreenToWorldPoint(bound).y - radiusOfCollider - scaleSpd * timeCounterSpawn;
                    batStart = batPointDown[0];
                    batEnd = batPointDown[1];
                    break;
                }
            case ScreenPart.RIGHT:
                {
                    Vector2 bound = new Vector2(gameCamera.pixelWidth, 0);
                    spawnX = gameCamera.ScreenToWorldPoint(bound).x + radiusOfCollider + scaleSpd * timeCounterSpawn;
                    spawnY = 0;
                    batStart = batPointRight[0];
                    batEnd = batPointRight[1];
                    break;
                }
            case ScreenPart.LEFT:
                {
                    Vector2 bound = new Vector2(0, 0);
                    spawnX = gameCamera.ScreenToWorldPoint(bound).x - radiusOfCollider - scaleSpd * timeCounterSpawn;
                    spawnY = 0;
                    batStart = batPointLeft[0];
                    batEnd = batPointLeft[1];
                    break;
                }
            default:
                batStart = new Vector2(0, 0);
                batEnd = new Vector2(0, 0);
                spawnY = 0;
                spawnX = 0;
                break;
        }      

        if (screenPart == ScreenPart.TOP || screenPart == ScreenPart.DOWN)
        {          
            maxSpBord = 20f / 60f * gameCamera.orthographicSize;
            maxMinSpawnBord = new Vector2(maxSpBord, -maxSpBord);

            batStartBord = new Vector2((batStart.x + radiusOfCollider > playerBoxColider.size.x / 2f) ? (playerBoxColider.size.x / 2f) : (batStart.x + radiusOfCollider),
                    (batStart.x - radiusOfCollider < -playerBoxColider.size.x / 2f) ? (-playerBoxColider.size.x / 2f) : (batStart.x - radiusOfCollider));
            targetPoint = new Vector2(batStartBord.y + ((batStartBord.x - batStartBord.y) / 20f * randVal), batStart.y);

            float x = FindPointOnLine(targetPoint, new Vector2(batEnd.x + radiusOfCollider / 2f, batEnd.y), spawnY, false);
            maxMinSpawnBord.x = (x > maxMinSpawnBord.x) ? maxMinSpawnBord.x : x;
            x = FindPointOnLine(targetPoint, new Vector2(batEnd.x - radiusOfCollider / 2f, batEnd.y), spawnY, false);
            maxMinSpawnBord.y = (x < maxMinSpawnBord.y) ? maxMinSpawnBord.y : x;

            randVal = Random.Range(0, 21);
            spawnX = maxMinSpawnBord.y + (maxMinSpawnBord.x - maxMinSpawnBord.y) / 20 * randVal;
        }
        else
        {
            maxSpBord = 20f / 30f * gameCamera.orthographicSize;
            maxMinSpawnBord = new Vector2(maxSpBord, -maxSpBord);

            batStartBord = new Vector2((batStart.y + radiusOfCollider > playerBoxColider.size.y / 2f) ? (playerBoxColider.size.y / 2f) : (batStart.y + radiusOfCollider),
                (batStart.y - radiusOfCollider < -playerBoxColider.size.y / 2f) ? (-playerBoxColider.size.y / 2f) : (batStart.y - radiusOfCollider));
            targetPoint = new Vector2(batStart.x, batStartBord.y + ((batStartBord.x - batStartBord.y) / 20f * randVal));

            float y = FindPointOnLine(targetPoint, new Vector2(batEnd.x, batEnd.y + (radiusOfCollider - 0.3f)), spawnX, true); //radiusOfCollider / 2f
            maxMinSpawnBord.x = (y > maxMinSpawnBord.x) ? maxMinSpawnBord.x : y;
            y = FindPointOnLine(targetPoint, new Vector2(batEnd.x, batEnd.y - (radiusOfCollider - 0.3f)), spawnX, true); //radiusOfCollider / 2f
            maxMinSpawnBord.y = (y < maxMinSpawnBord.y) ? maxMinSpawnBord.y : y;

            randVal = Random.Range(0, 41);
            spawnY = maxMinSpawnBord.y + (maxMinSpawnBord.x - maxMinSpawnBord.y) / 40 * randVal;
        }
    }

    private float FindPointOnLine(Vector2 point1, Vector2 point2, float xOrYOfPoint, bool isFindX)
    {
        return isFindX ?
            (point2.y - point1.y) / (point2.x - point1.x) * (xOrYOfPoint - point2.x) + point2.y :
            (point2.x - point1.x) / (point2.y - point1.y) * (xOrYOfPoint - point2.y) + point2.x;
    }

    private IEnumerator CouroutineShield(float _durSlowDownBonus, float _percentSlowDown)
    {
        float oldSpeedFlyObj = speedFlyObj;
        float oldCdItemToPlayer = cdItemToPlayer;

        float percentSpeedCoef = (100 - _percentSlowDown) / 100;
        float newSpeedFlyObj = speedFlyObj * percentSpeedCoef;
        float newCdItemToPlayer = cdItemToPlayer / percentSpeedCoef;

        SetNewFlyObjSpecif(newCdItemToPlayer, newSpeedFlyObj);
        Debug.Log("Start SlowDown Bonus, dur: " + _durSlowDownBonus + ", percent slow: " + _percentSlowDown);
        yield return new WaitForSeconds(_durSlowDownBonus);
        SetNewFlyObjSpecif(oldCdItemToPlayer, oldSpeedFlyObj);
        Debug.Log("Stop SlowDown Bonus");
    }

    private IEnumerator CouroutineFindEnabledItems(float time)
    {
        float cameraOrtSize = gameCamera.orthographicSize + scaleSpd * delayBetwSpawn;
        enabledItemsNumbers = new List<int>();
        if (time != 0)
        {
            //Debug.Log("time = 0");
            yield return new WaitForEndOfFrame();
        }

        //Debug.Log("deley: " + time);
        
        int mod = (int)(flyingItemPrefabs.Length / (time * 15))+1;
        if (mod < 7) mod = 7;

        for (int i=0; i < flyingItemPrefabs.Length; i++)
        {
          
            FlyObjChar flyObjChar = flyingItemPrefabs[i].GetComponent<FlyObjChar>();
            if (flyObjChar.GetDownBoundSpawn() < cameraOrtSize && flyObjChar.GetUpBoundSpawn() > cameraOrtSize)
            {
                enabledItemsNumbers.Add(i);
            }
            if (i % mod == 0 && time != 0)
            {
                //Debug.Log("Wait iter: " + i);
                yield return new WaitForEndOfFrame();
            }
            
        }       
    }
    /////////////////////////////////////////////public///////////////////////////////////////////
    public void CalcFlyObjSpecif()
    {
        Vector2 bound = new Vector2(0, gameCamera.pixelHeight);
        imaginaryDist = (gameCamera.ScreenToWorldPoint(bound).y) - playerBoxColider.size.y / 2f;

        incrInFlyTime = cdItemToPlayer / (1 + (1 / (scaleSpd / speedFlyObj)));
        delayBetwSpawn = cdItemToPlayer - incrInFlyTime;

        imaginaryDist -= (scaleSpd * delayBetwSpawn);        
    }
    
    public void SetNewFlyObjSpecif(float _cdItemToPlayer, float _speedFlyObj)
    {
        //imaginaryDist = 0;
        if (!isChange)
        {
            oldCdItemToPlayer = cdItemToPlayer;
            oldSpeedFlyObj = speedFlyObj;
            cdItemToPlayer = _cdItemToPlayer;
            speedFlyObj = _speedFlyObj;
            isChange = true;
            sliderSpd.Enabled(false);
            sliderCd.Enabled(false);
            //Debug.Log("false, isChange - " + isChange);
        }
    }

    public void SetBoundsRandCd(float val)
    {
        boundsRandCd = val;
    }

    public void StartSlowDownBonus(float durSlowDownBonus, float percentSlowDown)
    {
        StartCoroutine(CouroutineShield(durSlowDownBonus, percentSlowDown));
    }


}
