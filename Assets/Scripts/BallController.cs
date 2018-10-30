using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

    public int force;
    Rigidbody2D rigid;
    public int scoreP1, scoreP2;
    Text scoreUIP1, scoreUIP2;
    GameObject panelSelesai;
    Text textPemenang;

	// Use this for initialization
	void Start () {

        scoreP1 = 0;
        scoreP2 = 0;

        scoreUIP1 = GameObject.Find("Score1").GetComponent<Text>();
        scoreUIP2 = GameObject.Find("Score2").GetComponent<Text>();
        textPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
        panelSelesai = GameObject.Find("Panel Selesai");
        panelSelesai.SetActive(false);

        rigid = GetComponent<Rigidbody2D>();
        Vector2 arah = new Vector2(2, 0).normalized;
        rigid.AddForce(arah * force);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TampilkanScore(){
        Debug.Log("Score P1 : " + scoreP1 + ", Score P2 : " + scoreP2);
        scoreUIP1.text = scoreP1.ToString();
        scoreUIP2.text = scoreP2.ToString();
    }

    void ResetBall(){
        transform.localPosition = new Vector2(0,0);
        rigid.velocity = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Tapi Kiri"){
            ResetBall();
            Vector2 arah = new Vector2(2, 0).normalized;
            rigid.AddForce(arah * force);
            scoreP2 += 1;
            TampilkanScore();
            if(scoreP2 >= 5){
                panelSelesai.SetActive(true);
                textPemenang.text = "Pemain Merah Menang!";
                Destroy(gameObject);
                return;
            }
        }

        if(collision.gameObject.name == "Tapi Kanan"){
            ResetBall();
            Vector2 arah = new Vector2(2, 0).normalized;
            rigid.AddForce(arah * force);
            scoreP1 += 1;
            TampilkanScore();
            if (scoreP1 >= 5)
            {
                panelSelesai.SetActive(true);
                textPemenang.text = "Pemain Biru Menang!";
                Destroy(gameObject);
                return;
            }
        }

        if (collision.gameObject.name == "Paddle Kiri" || collision.gameObject.name == "Paddle Kanan")
        {
            float sudut = (transform.position.y - collision.transform.position.y) * 5f;
            Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(arah * force * 2);
        }
    }
}
