using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaControl : MonoBehaviour
{

    int speed = 3;
    public float jumpForce;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public LayerMask groundlayer;
    public bool isJumping = true;
    public GameObject mainCamera;
    public GameObject Goaltextobject;
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Animator anim = GetComponent<Animator>();
        if (x != 0)
        {
            Vector2 movement = new Vector2(x, 0);
            rigidbody.AddForce(movement * speed);
            anim.SetBool("Dash", true);
        }
        else
        {
            anim.SetBool("Dash", false);
        }


        if (Input.GetKeyDown("space") && isJumping)
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            // 一度ジャンプしたらisJumpingをfalseにする。
            isJumping = false;
        }
        if (transform.position.x > mainCamera.transform.position.x - 4)
        {
            Vector3 cameraPos = mainCamera.transform.position;
            cameraPos.x = transform.position.x + 4;
            mainCamera.transform.position = cameraPos;
        }
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, min.x + 0.5f, max.x);
        transform.position = pos;


    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Groundタグを地面に付け、地面に衝突したらisJumpingをtrueにする。
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            Goaltextobject.SetActive(true);
        }
    }
}
