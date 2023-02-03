using UnityEngine;

public class BonusLineCtrl : MonoBehaviour
{
    [SerializeField] private ParticleSystem confetti;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(confetti, transform.position, confetti.transform.rotation);
            GameManager.Instance.StopAllNpcsFollowing();
           
        }
    }
}
