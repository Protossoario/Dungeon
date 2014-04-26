using UnityEngine;
using System.Collections;

public class SpiderScript : MonoBehaviour {
	int damage;
	/* Directions:
	 * 0 = none
	 * 1 = up
	 * 2 = down
	 * 3 = left
	 * 4 = right
	 */
	int direction;
	int time; // tiempo que falta de ejecutar de la animacion actual
	int timeMax = 9; // tiempo maximo de una animacion de movimiento
	Animator anim;
	float maxSpeed = 2f;
	bool turn; // variable que checa si es el turno de la arania de hacer un movimiento
	bool moving; // checa si la arania se esta moviendo
	bool aggressive; // sin utilizar aun
	// Use this for initialization
	void Start () {
		damage = 1;
		aggressive = false;
		time = 0;
		direction = 2;
		anim = GetComponent<Animator>();
		turn = false;
		moving = false;
	}

	public void startTurn() {
		turn = true;
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
		if (direction == 0 && turn) { // Al inicio del turno no tiene direccion
			Collider2D colUp = Physics2D.OverlapPoint(new Vector2(this.transform.position.x, this.transform.position.y + 0.32f));
			Collider2D colDown = Physics2D.OverlapPoint(new Vector2(this.transform.position.x, this.transform.position.y - 0.32f));
			Collider2D colLeft = Physics2D.OverlapPoint(new Vector2(this.transform.position.x - 0.32f, this.transform.position.y));
			Collider2D colRight = Physics2D.OverlapPoint(new Vector2(this.transform.position.x + 0.32f, this.transform.position.y));
			// Checar en las 4 direcciones si esta el jugador a un lado
			if (colUp != null && colUp.CompareTag("Player")) {
			}
			else if (colDown != null && colDown.CompareTag("Player")) {
			}
			else if (colUp != null && colUp.CompareTag("Player")) {
			}
			else if (colUp != null && colUp.CompareTag("Player")) {
			}
			// Si no se ataca al jugador, checar el movimiento
			else if (anim.GetBool("SpiderUp")) {
				if (colUp != null && colUp.CompareTag("Wall")) {
					anim.SetBool("SpiderUp", false);
					anim.SetBool("SpiderDown", true);
					direction = 2;
				} else {
					direction = 1;
					time = timeMax;
				}
			}
			else if (anim.GetBool("SpiderDown")) {
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
		// El turno se esta ejecutando y la arania se esta desplazando de un tile a otro
		else if (moving) {
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
				direction = 0;
				turn = false;
				rigidbody2D.velocity = new Vector2 (0f, 0f);
				GameObject dm = GameObject.Find("Dungeon Master");
				DungeonMaster dmScript = dm.GetComponent<DungeonMaster>();
				dmScript.notifyTurnFinish();
			}
		}
	}
}
