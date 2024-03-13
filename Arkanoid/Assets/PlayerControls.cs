using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public float speed = 10.0f;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
    }


    // Update is called once per frame
    void Update()
    {
    
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    var pos = transform.position;
    pos.x = mousePos.x;      
    transform.position = pos;

    }
        

      
    }


