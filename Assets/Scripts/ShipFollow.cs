using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
public class ShipFollow : MonoBehaviour {
    public Transform target;
    public Transform CenterLine;
    public Vector3 offset ;
    public bool IsEndlessLevel;

    [SerializeField] private Animator ZoomAnim;
    public float Xoffset; //Ship x on the screen
    public float YScale; //lock at scale
    private bool IsSlowMotion;
    public bool ZoomWithotAnimator;
    
    private Camera camera;
    private float Smoothing = 120f;
    //private Vector2 SmoothVelocity; 

    private float YOffset;
    public Vector3 CenterLineOffset;
    private bool IsMoveOffset;

    private bool IsNitroON;
    
    private void Start()
    {
        camera = GetComponent<Camera>();
        IsSlowMotion = false;
        IsMoveOffset = false;
        IsNitroON = false;
        offset = new Vector3(0f, 0f, -15f);
        Xoffset = 10f;//8f;
        YScale = 4f;
        CenterLineOffset = new Vector3(17.9f, -38.76f, 130) ;
    }
  
    public void StartSlowMotion(bool movingOffset = false)
    {
        IsSlowMotion = true;
        IsMoveOffset = movingOffset;
    }
    public void StopSlowMotion()
    {
        IsSlowMotion = false;
        IsMoveOffset = false;
    }

    public void StartNitro()
    {
        IsNitroON = true;
        if(!ZoomWithotAnimator) ZoomAnim.SetTrigger("NitroOn");

    }

    public void StopNitro()
    {
        IsNitroON = false;
        if (!ZoomWithotAnimator) ZoomAnim.SetTrigger("NitroOff");
    }

    public void StartReverseCameraFX()
    {
        StartCoroutine(ReverseVFX());
    }

   IEnumerator ReverseVFX()
    {
        GetComponent<Fisheye>().enabled = true;
        GetComponent<Fisheye>().strengthX = 0f;
        GetComponent<Fisheye>().strengthY = 0f;
        PauseMenuScript.CanOpen = false;
        float startTime = Time.time;
        float duration = 0.2f; //SetHald Duration (1/2 for FXin and 1/2 for FXout)
        float StrengthX = 0.2f;
        float StrengthY = 0.7f;

        //VFX in
        while (Time.time < startTime + duration)
        {
            float smoothing = (Time.time - startTime) / duration;
            //lerp
            GetComponent<Fisheye>().strengthX = Mathf.Lerp(GetComponent<Fisheye>().strengthX, StrengthX, smoothing);
            GetComponent<Fisheye>().strengthY = Mathf.Lerp(GetComponent<Fisheye>().strengthY, StrengthY, smoothing);
            yield return null;
        }

        //VFX out
        startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            float smoothing = (Time.time - startTime) / duration;
            //lerp
            GetComponent<Fisheye>().strengthX = Mathf.Lerp(GetComponent<Fisheye>().strengthX, 0, smoothing);
            GetComponent<Fisheye>().strengthY = Mathf.Lerp(GetComponent<Fisheye>().strengthY, 0, smoothing);
            yield return null;
        }

        //VFX complete
        PauseMenuScript.CanOpen = true;
        GetComponent<Fisheye>().enabled = false;
        yield return null;
    }
    private void LateUpdate() //FixedUpdate
    {
        if (!ShipController.Instance.IsAlive)
        {
            return;
        }
        else
        {

            //---try 1
            //transform.position = new Vector3(target.position.x + Xoffset, 0, 0) + offset;
            //---

            //--- try 2
            /*Vector3 _pos = new Vector3(target.position.x + Xoffset, 0, 0) + offset;
             Vector3 smoothedPos = Vector3.Lerp(transform.position, _pos, Smoothing * Time.deltaTime );
             transform.position = smoothedPos;*/
            //---

            //----- try 3
            /*Vector3 v3 = transform.position;
                v3.x = Mathf.Lerp(v3.x, target.position.x + Xoffset, Smoothing * Time.deltaTime);
             transform.position = v3;*/
            //-----

            //--- try 4
            Vector3 _pos = new Vector3(target.position.x + Xoffset, 0, 0) + offset;
            Vector3 smoothedPos = Vector3.Slerp(transform.position, _pos, 120f * Time.deltaTime);
            transform.position = smoothedPos;
            //---

            //----- try 5
            // transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, target.position.x + Xoffset, ref SmoothVelocity.x, Smoothing * Time.deltaTime), transform.position.y, transform.position.z);
            //-----



            YOffset = target.position.y - (target.position.y / YScale);
            Vector3 lockatPos = new Vector3(target.position.x + Xoffset, target.position.y - YOffset, 0);
            transform.LookAt(lockatPos);
            
            if (CenterLine != null) CenterLine.position = transform.position + CenterLineOffset - new Vector3(0f, 0, 0);

            if(IsEndlessLevel)
            {
                GetComponent<EndlessCamera>().SetPosition(target.position.x + 40);
            }


            //----------
            float zoom = 55;
            float speedZomm = 2f;
            float speedOffsetX = 0.5f;
            if (IsSlowMotion && !IsNitroON)
            {
                //Enter slow motion 
                Xoffset = Mathf.Lerp(Xoffset, 7.5f, Time.fixedDeltaTime * speedOffsetX);
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, zoom, Time.deltaTime * speedZomm);
            }
            else if (!IsNitroON && !IsSlowMotion)
            {
                //Exit slow motion 
                Xoffset = Mathf.Lerp(Xoffset, 10f, Time.fixedDeltaTime * speedOffsetX);
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 70f, Time.fixedDeltaTime * speedZomm);
            }


            if(IsMoveOffset)
            {
                //Enter slow motion 
                offset = Vector3.Lerp(offset, new Vector3(-1f, 0f, -15f), Time.fixedDeltaTime * speedZomm);
            }
            else
            {
                //Exit slow motion 
                offset = Vector3.Lerp(offset, new Vector3(0f, 0f, -15f), Time.fixedDeltaTime * speedZomm);
            }



            //---- Nitro Zone
            float zoomOut = 90f;
            float speedZommOut = 2f;
            if (IsNitroON && !IsSlowMotion)
            {
                //Enter Nitro
                if(!GetComponent<MotionBlur>().enabled) GetComponent<MotionBlur>().enabled = true;
                GetComponent<MotionBlur>().blurAmount = Mathf.Lerp(GetComponent<MotionBlur>().blurAmount, 0.65f, Time.fixedDeltaTime * speedZommOut);
                Xoffset = Mathf.Lerp(Xoffset, 6.5f, Time.fixedDeltaTime * speedOffsetX);
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, zoomOut, Time.fixedDeltaTime * speedZommOut);
            }
            else if(!IsNitroON && !IsSlowMotion)
            {
                //Exit Nitro
               
                if (GetComponent<MotionBlur>().enabled) GetComponent<MotionBlur>().blurAmount = Mathf.Lerp(GetComponent<MotionBlur>().blurAmount, 0.05f, Time.fixedDeltaTime * speedZommOut);
                Xoffset = Mathf.Lerp(Xoffset, 10f, Time.fixedDeltaTime * speedOffsetX);
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 70f, Time.fixedDeltaTime * speedZomm);
                if(GetComponent<MotionBlur>().blurAmount <= 0.07f)
                {
                    GetComponent<MotionBlur>().enabled = false;
                }
            }
        }

    }
    
}
