using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 5.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Target")
        {

            other.GetComponent<Target>().number--;
            Destroy(gameObject);
        }
    }
}
