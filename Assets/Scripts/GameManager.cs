using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_target;
    private float m_creatForce;
    private int targetDepth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCreatLess(int size, Vector3 pos, int num)
    {
        if (size > 1 && m_target != null)
        {
            targetDepth++;
            var lessTarget_right = Instantiate(m_target, new Vector3(pos.x, pos.y, -targetDepth * 0.3f), Quaternion.identity);
            targetDepth++;
            var lessTarget_left = Instantiate(m_target, new Vector3(pos.x, pos.y, -targetDepth * 0.3f), Quaternion.identity);
            m_creatForce = Random.Range(15, 20)/10f;
            lessTarget_right.GetComponent<Rigidbody2D>().AddForce(new Vector3(1f, 1.7f, 0) * m_creatForce);
            m_creatForce = Random.Range(15, 20)/10f;
            lessTarget_left.GetComponent<Rigidbody2D>().AddForce(new Vector3(-1f, 1.7f, 0) * m_creatForce);
            lessTarget_right.GetComponent<Target>().target_size = size - 1;
            lessTarget_left.GetComponent<Target>().target_size = size - 1;
            lessTarget_right.GetComponent<Target>().number = num;
            lessTarget_left.GetComponent<Target>().number = num;
        }
        else
        {
            //complete
        }
    }
}
