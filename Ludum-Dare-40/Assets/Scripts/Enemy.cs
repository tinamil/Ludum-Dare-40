using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{

    public int HP = 1;
    public int Damage = 1;
    public GameObject Explosion;
    public AudioClip ExplosionSound;
    public AudioClip hurtClip;
    public AudioClip dropClip;
    public GameObject[] dropItems;
    public float dropChance;
    public float explodeAfter;
    public bool explodeOnCollision = false;

    // Use this for initialization
    void Start() {
        if (explodeAfter > 0)
        {
            StartCoroutine(DelayedExplosion());
        }
    }

    IEnumerator DelayedExplosion() {
        yield return new WaitForSeconds(explodeAfter);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (explodeOnCollision && collision.gameObject.tag != "Player")
        {
            DestroyMe(false);
        }
    }

    public void DestroyMe(bool playerKilled = true) {
        if (Random.value <= dropChance)
        {
            MusicController.PlayClip(dropClip);
            Instantiate(dropItems[Random.Range(0, dropItems.Length)], transform.position, Quaternion.identity);
        }
        if (Explosion != null)
        {
            Instantiate(Explosion, transform.position, transform.rotation);
        }
        if (ExplosionSound != null)
        {
            MusicController.PlayClip(ExplosionSound);
        }
        if (playerKilled)
            GameController.DestroyEnemy(this);
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damage) {
        HP -= damage;
        if (HP <= 0)
        {
            DestroyMe();
        } else if (hurtClip != null)
        {
            GetComponent<AudioSource>().PlayOneShot(hurtClip);
        }
    }
}
