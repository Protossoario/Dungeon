using UnityEngine;
using System.Collections;

public class SpiderScript : EnemyScript {
	int damage;
	Animator anim;
	public float maxSpeed = 2f;
	int time;
	bool down;
	public int timeMax = 9;
	// Use this for initialization
	void Start () {
		damage = 1;
		agressive = false;
		time = 0;
		vision = 1;
		direction = 2;
		anim = GetComponent<Animator>();
		turn = false;
		down = false;
	}

	public void startTurn() {
		turn = true;
		Debug.Log("Spaidah turn: " + turn);
	}
	/*
	void checkPlayer()
	{ float monsterPositionX = this.transform.position.x;
	  float monsterPositionY = this.transform.position.y;
		if(GameObject.FindGameObjectWithTag("Player").transform.position.x<=monsterPositionX+tilesize*vision
		   ||GameObject.FindGameObjectWithTag("Player").transform.position.x>=monsterPositionX-tilesize*vision)
			agressive = true;
		if(GameObject.FindGameObjectWithTag("Player").transform.position.y<=monsterPositionY+tilesize*vision
		   ||GameObject.FindGameObjectWithTag("Player").transform.position.y>=monsterPositionY-tilesize*vision)
			agressive = true;


	} */

	// Update is called once per frame
	void FixedUpdate () {
		if (direction == 0 && turn) {
			Debug.Log("Spaidah determining path to take");
			Collider2D colUp = Physics2D.OverlapPoint(new Vector2(this.transform.position.x, this.transform.position.y + 0.32f));
			Collider2D colDown = Physics2D.OverlapPoint(new Vector2(this.transform.position.x, this.transform.position.y - 0.32f));
			if (anim.GetBool("SpiderUp")) {
				if (colUp != null && colUp.CompareTag("Wall")) {
					anim.SetBool("SpiderUp", false);
					anim.SetBool("SpiderDown", true);
					direction = 2;
				} else {
					direction = 1;
					time = timeMax;
				}
			} else if (anim.GetBool("SpiderDown")) {
				if (colDown != null && colDown.CompareTag("Wall")) {
					anim.SetBool("SpiderUp", true);
					anim.SetBool("SpiderDown", false);
					direction = 1;
				} else {
					direction = 2;
					time = timeMax;
				}
			}
		}
		else if (turn) {
			Debug.Log("Spaidah moving to intercept");
			switch (direction) {
			case 1:
				rigidbody2D.velocity = new Vector2 (0f, maxSpeed);
				break;
			case 2:
				rigidbody2D.velocity = new Vector2 (0f, -maxSpeed);
				break;
			}

			time--;
			if (time <= 0) {
				Debug.Log("Spaidah is done, for now");
				direction = 0;
				turn = false;
				rigidbody2D.velocity = new Vector2 (0f, 0f);
				GameObject dm = GameObject.Find("Dungeon Master");
				DungeonMaster dmScript = (DungeonMaster) dm.GetComponent(typeof(DungeonMaster));
				dmScript.notifyTurnFinish();
			}
		}
	}
}
