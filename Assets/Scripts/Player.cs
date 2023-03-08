using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    public CameraShake cameraShake;
    public UIManager uimanager;



    public GameObject cam;
    public GameObject VectorBack;
    public GameObject VectorForward;

    private Rigidbody rb;

    private Touch touch;


    //scrol sað sol çekebilmesi için
    [Range(20,40)]
    [SerializeField] private int speedModifier;
    public int forwardSpeed;

    private bool speedballforward = false;
    private bool firstTouchControl = false;

    private void Start()
    {
       GetComponent();
    }

    public void Update()
    {
       PlayerMove();
       Firsttouch();
    }


    public GameObject[] FractureItems;

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag("Obstacles"))
        {
            cameraShake.CameraShakesCall();
            uimanager.StartCoroutine("WhiteEffect");
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            foreach (GameObject item in FractureItems)
            {
                item.GetComponent<SphereCollider>().enabled = true;
                item.GetComponent<Rigidbody>().isKinematic = false;
            }
            StartCoroutine("TimeScaleControl");
        }
    }

    public IEnumerator TimeScaleControl()
    {
        speedballforward = true;
        yield return new WaitForSecondsRealtime(0.4f);
        Time.timeScale = 0.4f;
        yield return new WaitForSecondsRealtime(0.6f);
        uimanager.RestartButtonActive();
        rb.velocity = Vector3.zero;
    }



    private void Firsttouch()
    {
        if (Variables.firsttouch==1&& speedballforward==false)
        {
            transform.position += new Vector3(0,0,forwardSpeed * Time.deltaTime);
            VectorBack.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            VectorForward.transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);
            
        }
    }
    private void PlayerMove()
    {
        if (Input.touchCount>0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase==TouchPhase.Began)
            {

                //butona basýnca oyun baþlýyordu bu kod sayesinde açýlmýyor
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    if (firstTouchControl==false)
                    {
                        uimanager.FirstTouch();
                        Variables.firsttouch = 1;
                        firstTouchControl = true;
                    }  
                }   
            }
           else if (touch.phase==TouchPhase.Moved)
            {
                //butona basýnca oyun baþlýyordu bu kod sayesinde açýlmýyor
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    rb.velocity = new Vector3(touch.deltaPosition.x * speedModifier * Time.deltaTime,
                          transform.position.y,
                          touch.deltaPosition.y * speedModifier * Time.deltaTime);
                    if (firstTouchControl == false)
                    {
                        uimanager.FirstTouch();
                        Variables.firsttouch = 1;
                        firstTouchControl = true;
                    }
                }
            }
            else if (touch.phase==TouchPhase.Ended)
            {
                // rb.velocity = new Vector3(0, 0, 0);  ikiside ayný iþlev
                rb.velocity = Vector3.zero;
            }

        }
    }

    private void GetComponent()
    {
        rb = GetComponent<Rigidbody>();
    }

}
