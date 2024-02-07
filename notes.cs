using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ** FOR UI MANAGER USE **
 
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

public class OurPlayer: MonoBehaviour{
    public float PlayerSpeed;
    public float PlayerJump;
    public int NoOfJumps;
    public int JumpsCheck;
    public Transform target;
    public GameObject _explosion;
    private Rigidbody2D _rb;
    public bool IsAlive = true;
    void Start(){
        _rb = GetComponent<Rigidbody2D>();
        transform.position = target.position;
    }
    void Update(){
        float MoveX = input.GetAxis("Horizontal") * PlayerSpeed;
        if(IsAlive){
            _rb.velocity = new Vector2(MoveX, _rb.velocity.y);
            if (Input.GetKeyDown(KeyCode.Space) && JumpsCheck < NoOfJumps){
                _rb.velocity = new Vector2(_rb.velocity.x, PlayerJump);
                JumpsCheck++;
            }
        }
    }
    private void onCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Ground"){
            JumpsCheck = 0;
        }
        if(collision.gameObject.tag == "Trap"){
            SpriteRenderer psr = GetComponent<SpriteRenderer>();
            BoxCollider2D pbc = GetComponent<BoxCollider2D>();
            _rb.simulated = false;
            psr.enabled = false;
            pbc.enabled = false;
            IsAlive = false;
            StartCoroutine (Dead());
        }
    }
    private void onTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "DJ"){
            NoOfJumps = 2;
            SpriteRenderer sr = collision.GetComponent<SpriteRenderer>();
            BoxCollider2D cl = collision.GetComponent<BoxCollider2D>();
            sr.enabled = false;
            cl.enabled = false;
        }

    }
    IEnumerator Dead(){
        Instantiate(_explosion, transform.position, transform.rotation);
        yield return new WaitForSecondsRealtime(2);
        SpriteRenderer psr = GetComponent<SpriteRenderer>();
        BoxCollider2D pbc = GetComponent<BoxCollider2D>();
        _rb.simulated = true;
        psr.enabled = true;
        pbc.enabled = true;
        IsAlive = true;
        transform.position = target.position;
    }
}

public class Autodestruct : MonoBehaviour{

    void Start(){
        Destroy(gameObject, 1,5f);
    }
}
