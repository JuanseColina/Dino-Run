using UnityEngine;

public class SawBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject saw, bar;
    [SerializeField] private float speed = 5, time;


    private float _zMax;

    private void Start()
    {
        _zMax = saw.transform.localPosition.z;
        LeanTween.moveLocalZ(saw, _zMax * -1, time).setLoopPingPong(-1);
    }

    private void Update()
    {
        float timeD = Time.deltaTime;
        saw.transform.Rotate(new Vector3(speed * timeD, 0 ,0 ));
    }

    
}
