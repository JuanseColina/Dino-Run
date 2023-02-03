using System;
using System.Collections;
using UnityEngine;

public class CashCtrl : MonoBehaviour
{
    [SerializeField] private float followSpeed;

    private Transform _follower;
    private bool _startFollowing;
    public void UpdateCubePosition(Transform followedCube, bool isFollowStart)
    {
        _startFollowing = isFollowStart;
        _follower = followedCube;
    }

    [SerializeField] private float speed = 6;
    private void Update()
    {
        // if (!_startFollowing && _follower)
        // {
        //     var position = _follower.localPosition;
        //     transform.Translate(new Vector3(position.x * Time.deltaTime * speed,position.y * Time.deltaTime * speed,position.z * Time.deltaTime * speed) );
        //     if (Vector3.Distance(transform.localPosition, _follower.localPosition) < 2)
        //     {
        //         _startFollowing = true;
        //     }
        // }
        
        if (_startFollowing && _follower)
        {
            var myPosition = transform.position;
            var followerPosition = _follower.position;
            myPosition = new Vector3(Mathf.Lerp(myPosition.x, followerPosition.x, followSpeed * Time.deltaTime),
                myPosition.y,
                followerPosition.z);
                transform.position = myPosition;
        }
    }
    
}
