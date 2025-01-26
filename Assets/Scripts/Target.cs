using NUnit.Framework.Constraints;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    //public UnityEvent NumberAnim;
    public int number;
    public int target_size;
    private SpriteRenderer m_sprite;
    private TextMesh numText;
    private bool m_attated;
    public Animator animator;
    private float m_time;
    private int ori_number;

    private void Awake()
    {
        numText = GetComponentInChildren<TextMesh>();
        m_sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        ori_number = number;
        switch (target_size)
        {
            case 1: transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                break;
            case 2: transform.localScale = new Vector3(1, 1, 1);
                break;
            case 3: transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                break;
            case 4: transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (numText != null)
        {
            if (number < 1000)
            {
                numText.text = number.ToString();
            }
            else
            {
                numText.text = (Mathf.Floor(number / 100) / 10).ToString() + "K";
            }
        }

        if (m_sprite != null)
        { 
            if (number <= 582)
            {
                m_sprite.color = new Color(48 / 255f, 217 / 255f, Mathf.FloorToInt(120 + number / 6) / 255f, 1);
            }
            else if (number > 582 && number < 1000)
            {
                m_sprite.color = new Color(48 / 255f, Mathf.FloorToInt(217 - (number - 582) * 169 / 418) / 255f, 217 / 255f, 1);
            }
            else if (number >= 1000 && number < 5000)
            {
                m_sprite.color = new Color(Mathf.FloorToInt(48 + (number - 1000) * 110 / 4000) / 255f, 48 / 255f, 217 / 255f, 1);
            }
            else
            {
                m_sprite.color = new Color(140 / 255f, 48 / 255f, 217 / 255f, 1);
            }
        }


        if (number <= 0)
        {
            FindObjectOfType<GameManager>().OnCreatLess(target_size, transform.position, ori_number);
            //particle
            Destroy(gameObject);
        }

        animator.SetBool("attacked", m_attated);
        
        if (m_attated)
        {
            m_time += Time.deltaTime;
            if (m_time > 0.2f)
            {
                m_attated = false;
            }
        }

 //      if(GetComponent<Rigidbody2D>().velocityY < 0)
 //      {
 //          GetComponent<Rigidbody2D>().gravityScale = 0.4f;
 //      }else if (GetComponent<Rigidbody2D>().velocityY > 0)
 //      {
 //          GetComponent<Rigidbody2D>().gravityScale = 0.15f;
 //      }
 //
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            m_attated = true;
            m_time = 0f;
        }
        if (collision.transform.tag == "Wall")
        {
            Vector2 theVec = gameObject.GetComponent<Rigidbody2D>().velocity;
            theVec.x *= -1;
            gameObject.GetComponent<Rigidbody2D>().velocity = theVec;
        }
        if (collision.transform.tag == "Floor")
        {
            float coef=1;
            Vector2 theVec = gameObject.GetComponent<Rigidbody2D>().velocity;
            switch (target_size)
            {
                case 1:
                    coef = Mathf.Abs(3.513938f / theVec.y);
                    break;
                case 2:
                    coef = Mathf.Abs(4.021506f / theVec.y);
                    break;
                case 3:
                    coef = Mathf.Abs(4.49003f / theVec.y);
                    break;
                case 4:
                    coef = Mathf.Abs(5.153772f / theVec.y);
                    break;
            }
            theVec.y *= -1;
            gameObject.GetComponent<Rigidbody2D>().velocity = theVec*coef;
        }

    }

// private void OnCollisionEnter2D (Collision2D collision)
// {
//     if (collision.gameObject.tag == "Floor")
//     {
//         switch (target_size)
//         {
//             case 1:
//                 Debug.Log("size 1 =" + GetComponent<Rigidbody2D>().velocity.y);
//                 break;
//             case 2:
//                 Debug.Log("size 2 =" + GetComponent<Rigidbody2D>().velocity.y);
//                 break;
//             case 3:
//                 Debug.Log("size 3 =" + GetComponent<Rigidbody2D>().velocity.y);
//                 break;
//             case 4:
//                 Debug.Log("size 4 =" + GetComponent<Rigidbody2D>().velocity.y);
//                 break;
//         }
//     }
// }
}
