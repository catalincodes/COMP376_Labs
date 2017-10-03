using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform mTarget;
    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;

    float mArriveThreshold = 0.05f;

    private Transform EnemyTransform;
    void Start()
    {
        EnemyTransform = GetComponentInParent<Transform>();
    }

    void Update ()
    {
        if(mTarget != null)
        {
            float distanceBetweenTargetAndUnit = Vector2.Distance(mTarget.position, this.transform.position);
            if ((distanceBetweenTargetAndUnit > mArriveThreshold) && (distanceBetweenTargetAndUnit < mFollowRange))
            {
                Debug.Log("Attack!!!");
                EnemyTransform.position = Vector2.MoveTowards(EnemyTransform.position, mTarget.position, mArriveThreshold);
            }
            
        }
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }
}
