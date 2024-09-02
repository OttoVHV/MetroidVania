using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    //Pontos patrulha
    public Vector2 PontoPerseguir;

    [SerializeField]
    private Vector2 ponto1, ponto2;
    [SerializeField]
    private Texture textD, textE;
    Texture text;
    
    //componentes inimigo
    [SerializeField]
    private float speed;
    private Rigidbody2D rig;
    private BoxCollider2D bx;

    
    RaycastHit2D raio;
    GameObject player;
    bool perseguindo;
    bool isGrounded;
    bool Buraco = false;

    void Start()
    {
        PontoPerseguir = ponto1;
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        bx = gameObject.AddComponent<BoxCollider2D>();
        bx.isTrigger = true;
    }

    void Update()
    {
        //esquerda ou direita
        if(transform.position.x < PontoPerseguir.x && !Buraco)
        {
            rig.velocity = new Vector2 (speed, rig.velocity.y);
            bx.offset = new Vector2(1, 0.05f);
            raio = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y), new Vector2(0,-2));
            Debug.DrawRay(new Vector2(transform.position.x + 1, transform.position.y), new Vector2(0,-2));
        } else if(transform.position.x > PontoPerseguir.x && !Buraco){
            rig.velocity = new Vector2 (-speed, rig.velocity.y);
            bx.offset = new Vector2(-1, 0.05f);
            raio = Physics2D.Raycast(new Vector2(transform.position.x - 1, transform.position.y), new Vector2(0,-2));
            Debug.DrawRay(new Vector2(transform.position.x -1, transform.position.y), new Vector2(0,-2));
        }

        //mudar de ponto da patrulha
        if(Vector2.Distance(transform.position, PontoPerseguir) < 0.6f && PontoPerseguir == ponto1)
        {
            PontoPerseguir = ponto2;
        } else if(Vector2.Distance(transform.position, PontoPerseguir) < 0.6f && PontoPerseguir == ponto2)
        {
            PontoPerseguir = ponto1;
        }

        //perseguir o player
       if(Vector2.Distance(transform.position, player.transform.position) < 5f)
       {
        perseguindo = true;
       }

       if(perseguindo)
       {
        PontoPerseguir = player.transform.position;
       }

       if(raio.collider == null)
       {
        Buraco = true;
        rig.velocity = new Vector2(0, rig.velocity.y);
       } else {
        Buraco = false;
       }
       Debug.Log(raio.collider);

       isGrounded = Physics2D.OverlapBox(transform.position, new Vector3(0, 1, 0), 0f, 7);
    }

    //mostrar as setas
    private void OnDrawGizmos()
    {
        if(PontoPerseguir == ponto1)
        {
            text = textD;
        } else if(PontoPerseguir == ponto2)
        {
            text = textE;
        } 

        if(text != null)
        for(float i = ponto2.x;i < ponto1.x ;i++)
        {
            Gizmos.DrawGUITexture(new Rect(i, 0, 1, 1), text);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Ground" && isGrounded)
        {
            rig.AddForce(new Vector2(rig.velocity.x, 300f), ForceMode2D.Force);
            Debug.Log("Pula");
        }
    }
}
