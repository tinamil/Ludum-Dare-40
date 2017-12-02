using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int HP = 1;
    public GameObject Explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DestroyMe() {
        Instantiate(Explosion);
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damage) {
        HP -= damage;
        if (HP <= 0)
        {
            DestroyMe();
        }
    }
}
