using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float m_MovementSmoothing=0.03f;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject[] bullet_Point;
    [Range(0, 0.5f)]
    [SerializeField] private float delayBullet = 0.02f;
    [SerializeField] private float bullet_Force = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created\
    private bool mouseClicked;
    private float m_Position_x = 0;
    private float m_clamp_x = 2.16f;
    private bool bullet_trigger = true;
    private void Awake()
    {
        mouseClicked = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseClicked = true;
        }else if (Input.GetMouseButtonUp(0))
        {
            mouseClicked = false;
        }
    }


    private void FixedUpdate()
    {
        if (mouseClicked)
        {
            Vector3 targetPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 currentPos = transform.position;
            float m_x = Mathf.SmoothDamp(currentPos.x, targetPos.x, ref m_Position_x, m_MovementSmoothing);
            m_x = Mathf.Clamp(m_x, -m_clamp_x, m_clamp_x);
            transform.position = new Vector3(m_x, currentPos.y, currentPos.z);
            //transform.position = Vector3.SmoothDamp(currentPos, targetPos, ref m_Position, m_MovementSmoothing);
            if (bullet_trigger)
            {
                
                bullet_trigger = false;
                for (int i = 0; i < bullet_Point.Length; i++){
                    var preBullet = Instantiate(bullet, bullet_Point[i].transform.position, Quaternion.identity) as GameObject;
                    preBullet.GetComponent<Rigidbody2D>().AddForce(Vector3.up * bullet_Force);
                }
                Invoke("OnFlip", delayBullet);
            }

            
            //StartCoroutine(OnLaunch());
        }
    }

    void  OnFlip()
    {
        bullet_trigger = true;
    }
}
