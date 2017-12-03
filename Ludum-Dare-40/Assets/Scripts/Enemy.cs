using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour {

    public int HP = 1;
    public int Damage = 1;
    public GameObject Explosion;
    public AudioClip hurtClip;
    public AudioClip dropClip;
    public GameObject[] dropItems;
    public float dropChance;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DestroyMe() {
        if(Random.value <= dropChance)
        {
            MusicController.PlayClip(dropClip);
            Instantiate(dropItems[Random.Range(0, dropItems.Length)], transform.position, Quaternion.identity);
        }
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damage) {
        HP -= damage;
        if (HP <= 0)
        {
            DestroyMe();
        } else if(hurtClip != null)
        {
            GetComponent<AudioSource>().PlayOneShot(hurtClip);
        }
    }
}
