using UnityEngine;
using System.Collections;

public class Jamminger : MonoBehaviour
{
    MegaMan mMegaMan;

    [SerializeField]
    GameObject mExplosionPrefab;
    void Start()
    {
        mMegaMan = GameObject.Find("Mega Man").GetComponent<MegaMan>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Bullet(Clone)")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
            Instantiate(mExplosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag=="Player")
        {
            mMegaMan.TakeDamage(3);
        }
    }
}
