using UnityEngine;
using Mathf = UnityEngine.Mathf;
using UnityEngine.InputSystem;

public class player_movement : MonoBehaviour
{
    //variables
    private bool hasMoved;
    // make editable from Unity
    [SerializeField] private float speed;
    // instantiate a Rigidbody2D object called body
    private Rigidbody2D body;
    private Animator anim;
    public GameObject child;
    PlayerControls controls;

    Vector3 direction;
    Vector2 move;
    Vector2 aim; //mousepad and right joystick vector
    public InputControl control { get; } //tried detecting if gamepad...
    public string currentControlScheme { get; }
    public InputAction action { get; }

    private float x;
    private float y;
    private float z;
    private float angleofjump;

    private GameObject joystick;
    private GameObject joystick2; //right joystick probably
    private GameObject jump_button;
    private Animator jump_button_anim;
    private float joystickX;
    private float joystickY;
    private float joystick2X; //right joystick probably
    private float joystick2Y; //right joystick probably

    //player sprite pixels per unit
/*     private int pixelsPerUnit = 24; */
    private player_stats player_Stats;
    // Setup get object
    private void Awake(){
        controls = new PlayerControls();

        //different phases, started, performed, cancelled.

        //take the value of Movement and put it into the move object.
        controls.Player.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => move = Vector2.zero;

/*         controls.Player.Aim.performed += ctx => aim = ctx.ReadValue<Vector2>();
        controls.Player.Aim.canceled += ctx => aim = Vector2.zero; */

        child = transform.GetChild(0).gameObject;
        
        // Grabbing references from objects
        body = GetComponent<Rigidbody2D>();
        anim = child.GetComponent<Animator>();

/*         joystick = GameObject.FindGameObjectsWithTag("joystick")[0];
        jump_button = GameObject.FindGameObjectsWithTag("jump_button")[0];
        jump_button_anim = jump_button.GetComponent<Animator>();
        joystickX = joystick.transform.localPosition.x;
        joystickY = joystick.transform.localPosition.y; */

/*         player_Stats = gameObject.GetComponent<player_stats>(); */
    }

    private void Update(){
        Vector3 temp = joystick.transform.localPosition;
        temp.x = joystickX + move.x * 12;
        temp.y = joystickY + move.y * 12;
        joystick.transform.localPosition = temp;


        SetDirection(move.x);
        body.velocity = new Vector2(move.x * speed, body.velocity.y);

/*         // rotate if jumping
        angleofjump = Mathf.Atan(body.velocity.y/body.velocity.x)*180/Mathf.PI;
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("jump") & Mathf.Abs(body.velocity.x)>=2 & body.velocity.y>=0){
            child.transform.rotation = Quaternion.identity * Quaternion.Euler(0,0,angleofjump);
        } else {
            child.transform.rotation = Quaternion.identity;
        } */

        if(move.x == 0)
        {
            hasMoved = false;
        }
        else if (move.x != 0 && !hasMoved)
        {
            hasMoved = true;

        GetMovementDirection();
        }

        // set animations last
        anim.SetBool("walk", move.x != 0);
/*         anim.SetBool("grounded", grounded); */

    }

    public void GetMovementDirection()
    {
        if (move.x < 0)
        {
            if (move.y > 0)
            {
                direction = new Vector3(-0.5f, 0.5f);
            }
            else if (move.y < 0)
            {
                direction = new Vector3(-0.5f, -0.5f);
            }
            else
            {
                direction = new Vector3(-1, 0, 0);
            }
            transform.position += direction;
        }
        else if (move.x > 0)
        {
            if (move.y > 0)
            {
                direction = new Vector3(0.5f, 0.5f);
            }
            else if (move.y < 0)
            {
                direction = new Vector3(0.5f, -0.5f);
            }
            else
            {
                direction = new Vector3(1, 0, 0);
            }
            transform.position += direction;
        }
    }

    private void SetDirection(float horizontalinput){
        if (horizontalinput > 0) transform.localScale = Vector3.one;
        else if (horizontalinput < 0) transform.localScale = new Vector3(-1,1,1);
    }

/*     private void LateUpdate()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        transform.position = new Vector3(Mathf.Round(x * pixelsPerUnit) / pixelsPerUnit, Mathf.Round(y * pixelsPerUnit) / pixelsPerUnit, z);
    } */

/*     private void Jump(){
        jump_button_anim.SetBool("Button", true);
        if (grounded == true){
            body.velocity = new Vector2(body.velocity.x, speed);
        }
    }

    private void NoJump()
    {
        jump_button_anim.SetBool("Button", false);
    }  */

    void OnEnable() {
        if (controls?.Player != null){
            controls.Player.Enable();
        }
    }
    void OnDisable() {
        if (controls?.Player != null){
            controls.Player.Disable();       
        }
    } 

/*     private void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.tag == "ground"){
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag == "ground"){
            grounded = false;
        }
    } */
}
