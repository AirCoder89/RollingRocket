using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {

    public static ShipController Instance;
    [SerializeField] GameObject ExploseFX;
    public Text DistanceTxt;
    public Text CoinsTxt;
    [Range(40, 300)]  public float LevelSpeed;
    private float gravity = 0f;
    private float thrust;
    private float RotationSpeed;
    private float FuelConsume;
    public float XSpeed;
    private float YSpeed = 0f;
    private bool Engine = false;
   // [Range(100, 5000)] public float Fuel = 1000f;
    private bool FuelIsEmpty = false;
    
    private float _distance;
    private Quaternion rotation;
    [HideInInspector] public Rigidbody2D RG;
    [HideInInspector] public string GameControl;

    [HideInInspector] public bool isOnMagnet = true;
    [HideInInspector] public bool isGameStarted = false;

    [SerializeField] private TrailRenderer Trail1;
    [SerializeField] private TrailRenderer Trail2;
    [SerializeField] private TrailRenderer SpeedTrailFX;
    [SerializeField] private GameObject ShipMagnetFX;
    [SerializeField] private GameObject SmokeFX;
    
    private Vector3 ShipScale;
    private string scaleMode;
    [HideInInspector] public bool isGamePaused;
    private bool isFirstClick;
    private bool isSecondClick;
    private bool isReversed = false; //reverse the gamer play control (reverse gravity)
    [HideInInspector] public bool isLevelWin;
    [HideInInspector] public bool touchBegan;
    [HideInInspector] public bool touchEnd;
    [HideInInspector] public bool IsAlive;
    private uint SizeIndex; //1- normal //2- big //3- small
    private float angle;
    

    // Use this for initialization

    private void OnLevelWasLoaded(int level)
    {
        //reInit Ship
        Instance = this;
        this.Start();
        //Init Ship Depending The level
        switch (level)
        {
            //case 3: print("level XXXXXXX"); break;
        }
    }
    public void StartSmoke()
    {
        SmokeFX.SetActive(true);
    }
    void Start () {
        
        Instance = this;
        RG = GetComponent<Rigidbody2D>();
        RG.gravityScale = 0f;

        //---------- Init Ship
        UpdateShipData();

        //--------------------------

        scaleMode = "Normal";
        SizeIndex = 1;
        ShipScale = this.gameObject.transform.localScale;

        SpeedTrailFX.enabled = false;
        isGamePaused = false;
         FuelIsEmpty = false;
        isReversed = false;
        touchBegan = false;
        touchEnd = false;
        isFirstClick = true;
        isSecondClick = false;
        isLevelWin = false;
        IsAlive = true;
        _distance = 0f;
       
        DistanceTxt.text = "0.00";
    }
   
   public void UpdateShipData()
    {
        ShipData shipData = new ShipData();
        //--Fuel Conusme
        int FuelConsumeLevel = shipData.GetLevelFuelConsume();
        FuelConsume = shipData.GetFuelConsume(FuelConsumeLevel);

        //--thrust
        int thrustLevel = shipData.GetLevelThrust();
        thrust = shipData.GetThrust(thrustLevel);

        //--RotationSpeed
        int RotationSpeedLevel = shipData.GetLevelSpeedRotation();
        RotationSpeed = shipData.GetSpeedRotation(RotationSpeedLevel);

        //--Magnet
        CircleCollider2D MagnetCollider = GetComponent<CircleCollider2D>();
        int MagnetLevel = shipData.GetLevelMagnet();
        MagnetCollider.radius = shipData.GetMagnet(MagnetLevel);

    }
    public void UpdateCoinsText(int coins)
    {
        if (coins < 10) CoinsTxt.text = "00" + coins.ToString();
        else if(coins >= 10 && coins < 100) CoinsTxt.text = "0" + coins.ToString();
        else CoinsTxt.text = coins.ToString();
    }
    public void onLevelWin()
    {
        isLevelWin = true;
        isGameStarted = false;
      
        SceneHandler.GetInstance().SetFinalDistance(_distance);
        if (gameObject != null) gameObject.SetActive(false);
        //if(gameObject != null)
        //GetComponentInChildren<SpriteRenderer>().enabled = false;
        //RG.velocity = new Vector2(5f * Time.deltaTime, RG.velocity.y);
    }
   
    public void SetTrailSpeedFX(bool SpStatus,float t)
    {
        if (SpeedTrailFX) SpeedTrailFX.enabled = SpStatus;
        SpeedTrailFX.time = t;
    }
    public void UpdateTrailSpeedAlpha(float alpha)
    {
       if(SpeedTrailFX) SpeedTrailFX.startColor = new Vector4(0, 0, 0, alpha);
    }
    public float getTrailSpeedAlpha()
    {
        if(SpeedTrailFX)
        return SpeedTrailFX.startColor.a;
        else return 0f;
    }
    public void SetFuelStatus(bool fuelStatus)
    {
        FuelIsEmpty = fuelStatus;
    }
   
  
    IEnumerator onstartGame()
    {
        Trail1.enabled = true;
        Trail2.enabled = true;
        SpeedTrailFX.startColor = new Vector4(0, 0, 0, 00);

        if (SceneHandler.GetInstance().GetisLateContinue()) {
            SceneHandler.GetInstance().SetIsLateContinue(false);

           RG.AddForce(new Vector2(120f, 0), ForceMode2D.Impulse);
		} else {
			RG.AddForce(new Vector2(210f, 0), ForceMode2D.Impulse);
		}
       
        yield return new WaitForSeconds(0.65f);
        isGameStarted = true;
        GoAnimation.Instance.StartAnimation();
        LevelManager.Instance.RemoveStartStation();
        EventHandler.GameStarted_TR();
    }
   
    public void ReverseHandler(bool isreversed)
    {
        this.isReversed = isreversed;
    }

    public void ResetPosition()
    {
        /* print("######################################");
         print("PARENT POS : " + transform.position);
         print("PARENT ROT : " + transform.rotation);
         print("ANGLE : " + this.angle);
         print("XSpeed : " + this.XSpeed);
         print("YSpeed : " + this.YSpeed);
         print("######################################");*/
        
    }
    IEnumerator LerpSomething()
    {
        float startTime = Time.time;
        float duration = 0.2f; //SetHald Duration (1/2 for FXin and 1/2 for FXout)
       
        //VFX in
        while (Time.time < startTime + duration)
        {
            float smoothing = (Time.time - startTime) / duration;
            //lerp
           
            yield return null;
        }

        //Lerp complete
       
       // XSpeed = 8.5f;
        yield return null;
    }

    // Update is called once per frame
    void Update () {

        //----------------------
        if(GameControl == "tap")
        {
#if UNITY_EDITOR
       /* if (Input.GetButtonDown("Fire1"))
        {
            touchBegan = true;
            touchEnd = false;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            touchBegan = false;
            touchEnd = true;
        }*/
        if(Input.GetKeyDown(KeyCode.Space))
            {
                touchBegan = true;
                touchEnd = false;
            }
        if (Input.GetKeyUp(KeyCode.Space))
            {
                touchBegan = false;
                touchEnd = true;
            }
         if (Input.GetKeyUp(KeyCode.R))
            {
                ResetPosition();
            }
#elif UNITY_ANDROID
         if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchBegan = true;
            touchEnd = false;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchBegan = false;
            touchEnd = true;
        }
#endif
        }
       
        //trigger start game
        //if (Input.GetButtonDown("Fire1") && !isGameStarted && isFirstClick && !isSecondClick)
        if (touchBegan && !isGameStarted && isFirstClick && !isSecondClick)
        {
            if (isFirstClick)
            {
                gravity = 0f;
                isFirstClick = false;
                isSecondClick = true;
                StartCoroutine("onstartGame");
            }
        }
        //else if (Input.GetButtonDown("Fire1") && isGameStarted && !isFirstClick && isSecondClick)
        else if (touchBegan && isGameStarted && !isFirstClick && isSecondClick)
        {
            if(isSecondClick)
            {
                isSecondClick = false;
                if(isReversed)
                {
                    gravity = -0.1f;
                }
                else
                {
                    gravity = 0.1f;
                }
                
            }
        }


         if (isGameStarted)
         {

            // if (Input.GetButtonDown("Fire1"))
            if(touchBegan)
            {
                Engine = true;
            }
            // if (Input.GetButtonUp("Fire1"))
            if (touchEnd)
            {
                Engine = false;
            }



            if (!isGamePaused)
            {
                _distance += ((XSpeed / 10) / (Time.timeScale * 2));
                DistanceTxt.text =(_distance / 1000).ToString("F");
            }

            //Scale Handler (Normal/Big/Small)

            if (scaleMode == "Normal" && SizeIndex != 1)
            {
                
                ShipScale = Vector3.Lerp(ShipScale, new Vector3(0.65f,0.65f,1f), 5f * Time.deltaTime);
                this.gameObject.transform.localScale = ShipScale;
                Trail1.startWidth = 0.5f;
                Trail2.startWidth = 0.5f;
                if(SpeedTrailFX.enabled) SpeedTrailFX.startWidth = 0.8f;
                RG.mass = 10f;
                if(ShipScale == new Vector3(0.65f, 0.65f, 1f)) SizeIndex = 1;
            }
            else if (scaleMode == "Big" && SizeIndex != 2)
            {
                
                ShipScale = Vector3.Lerp(ShipScale, new Vector3(0.85f, 0.85f, 1f), 5f * Time.deltaTime);
                this.gameObject.transform.localScale = ShipScale;
                Trail1.startWidth = 0.6f;
                Trail2.startWidth = 0.6f;
                if (SpeedTrailFX.enabled) SpeedTrailFX.startWidth = 1.2f;
                RG.mass = 20f;
                if (ShipScale == new Vector3(0.85f, 0.85f, 1f)) SizeIndex = 2;

            }
            else if (scaleMode == "Small" && SizeIndex != 3)
            {
                
                ShipScale = Vector3.Lerp(ShipScale, new Vector3(0.45f, 0.45f, 1f), 5f * Time.deltaTime);
                this.gameObject.transform.localScale = ShipScale;
                Trail1.startWidth = 0.3f;
                Trail2.startWidth = 0.3f;
                if (SpeedTrailFX.enabled) SpeedTrailFX.startWidth = 0.5f;
                RG.mass = 0.1f;
                if (ShipScale == new Vector3(0.45f, 0.45f, 1f)) SizeIndex = 3;
            }

            //-Magnet FX
            if (isOnMagnet) ShipMagnetFX.SetActive(true);
            else ShipMagnetFX.SetActive(false);
            
        } //>Game Started

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("PlatformLayer"))
        {
            print("HIT PLATFORM");
            SceneHandler.GetInstance().GAME_OVER();
            EventHandler.ShipDie_TR();
            LevelManager.Instance.ResetTime();
            LevelManager.Instance.StopBonusCounting();
            IsAlive = false;
            if (SpeedTrailFX.enabled) SpeedTrailFX.enabled = false;
            SpeedTrailFX.startColor = new Vector4(00, 00, 00, 00);
            if (!isLevelWin) Instantiate(ExploseFX, transform.position, transform.rotation);
           // if (!isLevelWin) ObjectPooler.Instance.SpawnFromPool("ExploseFX", transform.position, Quaternion.identity);
            Destroy(gameObject);
            //SetPosition(new Vector3(-700f, -700f, 0));
        }
        else
        {
            if (isSecondClick)
            {
                if (isReversed)
                {
                    gravity = -0.1f;
                }
                else
                {
                    gravity = 0.1f;
                }
            }
        } 
    }


  

    private void OnTriggerStay2D(Collider2D other)
    {
        //magnet
        if (isOnMagnet)
        {
            if(other.gameObject.tag == "Coin")
            {
                coinsScript coin = other.gameObject.GetComponent<coinsScript>();
                coin.tweenTo(transform.position); 
            }
        }
        
    } //on Magnet
    private void FixedUpdate()
    {
        if(!isGameStarted)
        {
            return;
        }
        else
        {
            if (Engine && !FuelIsEmpty)
            {
                YSpeed += thrust;
                LevelManager.Instance.UpdateFuelProgressBar(this.FuelConsume);
            }

            
            ///FuelTxt.text = "Fuel: " + Fuel.ToString();
            RG.velocity = new Vector2(LevelSpeed * XSpeed * Time.deltaTime, RG.velocity.y);
            

            YSpeed -= gravity;
            Vector2 OldPos = transform.position;

            if (isReversed)
            {
                OldPos.y -= (YSpeed * Time.deltaTime);
                transform.position = OldPos;
                angle = Mathf.Atan2(YSpeed, XSpeed) * Mathf.Rad2Deg;
                rotation = Quaternion.Inverse(Quaternion.AngleAxis(angle, Vector3.forward));
            }
            else
            {
                //apply gravity
                OldPos.y += (YSpeed * Time.deltaTime);
                transform.position = OldPos;
                //set ship angle
                angle = Mathf.Atan2(YSpeed, XSpeed) * Mathf.Rad2Deg;
                rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);

        }

    }


    //########################## GET & SET ################################
    //--Get
    public float GetAngle()
    {
        return angle;
    }
    public Quaternion getRotation()
    {
        return rotation;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        if (gameObject.GetComponentInChildren<SpriteRenderer>() != null) return gameObject.GetComponentInChildren<SpriteRenderer>();
        else return new SpriteRenderer();
    }
    public Vector3 GetLocalScale()
    {
        return gameObject.transform.localScale;
    }
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
    public Transform GetTransform()
    {
        return gameObject.transform;
    }

    //--Set
    public void SetScale(string scalemode)
    {
        scaleMode = scalemode;
    }
    public void SetPosition(Vector3 pos)
    {
       gameObject.transform.position = pos;
    }
   
}
