using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour {

    private Rigidbody2D RG;
    public float speed;
    public bool move = false;
    private void Start()
    {
        EventHandler.onStartGame += onStartGame;
        EventHandler.onShipDieEvent += onGameOver;

        RG = GetComponent<Rigidbody2D>();
    }

    public void onGameOver()
    {
        move = false;
    }
    public void onStartGame()
    {
        move = true;
    }
    void Update () {
		if(move)
        {
            RG.velocity = new Vector2(speed * Time.deltaTime, RG.velocity.y);
        }
        else
        {
            RG.velocity = Vector2.zero;
        }
	}
}
