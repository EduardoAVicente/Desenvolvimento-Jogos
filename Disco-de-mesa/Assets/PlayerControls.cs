using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public float speed = 10.0f;
    public float boundY = 2.25f;
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

	if(mousePos.x < -0.30){
        pos.x = mousePos.x;
	}

    if (mousePos.x < 0){
        pos.y = mousePos.y;
    }

            
    transform.position = pos;

    }
        

      
    }



