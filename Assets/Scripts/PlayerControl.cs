using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControl : MonoBehaviour
{
    delegate void MoveFunc();
    MoveFunc moveFuncList;
    public float speed;
    public float jump;

    public LayerMask whatIsGround;

    private bool ground;
    private bool achivments = false;

    Rigidbody2D rb;
    Collider2D col;
    Animator anim;
    MeshRenderer mesh;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        mesh = GetComponent<MeshRenderer>();
        moveFuncList = new MoveFunc(MoveJump);
        gameObject.layer = 8;
        ChoosePlayerColor();

        if (SoundManager.instance.musicIsActive)
        {
            SoundManager.instance.StartMusic();
        }
    }
    void ChoosePlayerColor()
    {
        switch (GameManagerScript.Instance.playerColor)
        {
            case "PlayerBlack":
                mesh.material = GameManagerScript.Instance.arrColor[0];
                break;
            case "PlayerViolet":
                mesh.material = GameManagerScript.Instance.arrColor[1];
                break;
            case "PlayerBrown":
                mesh.material = GameManagerScript.Instance.arrColor[2];
                break;
            case "PlayerBlue":
                mesh.material = GameManagerScript.Instance.arrColor[3];
                break;
            case "PlayerYellow":
                mesh.material = GameManagerScript.Instance.arrColor[4];
                gameObject.layer = 0;
                achivments = true;
                break;
            case "PlayerRed":
                mesh.material = GameManagerScript.Instance.arrColor[5];
                break;
            default: 
                Debug.Log("Некорректно выбранный цвет!");
                break;
        }
    }
	
	void Update ()
    {
        if (NextLevel.Instanse.curPlatform >= 10)
        {
            moveFuncList = MoveGravity;
        }
        moveFuncList.Invoke();

        if (NextLevel.Instanse.curPlatform >= 6 && achivments)
        {
            gameObject.layer = 8;
        }
    }
    void MoveGravity()
    {
        ground = Physics2D.IsTouchingLayers(col, whatIsGround);

        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
        {
            if (ground)
            {
                rb.gravityScale = rb.gravityScale > 0 ? -3.5f : 3.5f;
            }
        }
        anim.SetBool("Ground", ground);
    }
    void MoveJump()
    {
        ground = Physics2D.IsTouchingLayers(col, whatIsGround);

        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
        {
            if (ground)
            {
                rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            }
        }
        anim.SetBool("Ground", ground);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemie")
        {
            GameManagerScript.Instance.YouLose();
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Point")
        {
            GameManagerScript.Instance.AddPoint();
        }
    }

}
