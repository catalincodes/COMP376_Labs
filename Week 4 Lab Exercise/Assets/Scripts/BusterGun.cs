using UnityEngine;
using System.Collections;

public class BusterGun : MonoBehaviour
{
    Animator mAnimator;
    bool mShooting;

    float kShootDuration = 0.25f;
    float mTime;

    private readonly Vector2 EMPTY_VECTOR = new Vector2(0.0f, 0.0f);
    private readonly Vector2 DEFAULT_DIRECTION = new Vector2(1.0f, 0.0f);

    [SerializeField]
    GameObject mBulletPrefab;
    MegaMan mMegaManRef;

    AudioSource mBusterSound;

    void Start ()
    {
        mAnimator = transform.parent.GetComponent<Animator>();



        // TODO: Get a reference to the following items and store them:
        //          - MegaMan component in the "Mega Man" game object (store in "mMegaManRef")
        //          - AudioSource component in "BusterGun" game object (store in "mBusterSound")

        mMegaManRef = GetComponentInParent<MegaMan>();
        mBusterSound = GetComponent<AudioSource>();
    }

    void Update ()
    {
        if (Input.GetButtonDown("Fire"))
        {
            // TODO: Shoot a bullet!
            //       Instantiate it and get a reference of its Bullet Component.
            //       You're going to need it ;)
            GameObject newBullet = Instantiate(mBulletPrefab);
            newBullet.transform.position = this.transform.position;
            Bullet bulletScript = newBullet.GetComponent<Bullet>();
            // TODO: Set the direction of the bullet
            //       Use the SetDirection() function from the Bullet class
            Vector2 megaManDirection = mMegaManRef.GetFacingDirection();
            bulletScript.SetDirection((megaManDirection == EMPTY_VECTOR ? DEFAULT_DIRECTION : megaManDirection));
            // TODO: Play the mBusterSound!
            mBusterSound.Play();
            // Set animation params
            mShooting = true;
            mTime = 0.0f;
        }

        if(mShooting)
        {
            mTime += Time.deltaTime;
            if(mTime > kShootDuration)
            {
                mShooting = false;
            }
        }

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        mAnimator.SetBool ("isShooting", mShooting);
    }
}
