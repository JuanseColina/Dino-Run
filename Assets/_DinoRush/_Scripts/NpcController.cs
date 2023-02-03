using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class NpcController : MonoBehaviour
{
    private NpcManager _npcManager;
    [SerializeField] private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private BoxCollider _boxCollider;

    [SerializeField] private bool isPolice;
    enum NpcState
    {
        Walk,
        Idle,
        Running,
        Thinking,
        Talking,
        WalkAndText,
        Text,
        ScareRun,
        JumpToPool
    }

    public float speed { get; set; }

    private int anim;
    
    [SerializeField] private NpcState _npcState;


    private bool isAngry;

    private Transform playerPos;


    [SerializeField] private float cash = 10;

    [SerializeField] private GameObject[] objectToRobbery;

    #region AnimationsName

    private string mode = "Mode";
    private string deadNro = "Ndead";
    private string left = "Left"; 
    private string right = "Right";
    #endregion

    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        _npcManager = FindObjectOfType<NpcManager>();
        NpcSelector();
        playerPos = GameObject.FindWithTag("Player").transform;

    }

    private bool following;
    public bool Following => following;
    private int maxEmojis = 6;

    private void Update()
    {
        float time = Time.deltaTime;

        if (_navMeshAgent.enabled)
        {
            _navMeshAgent.SetDestination(playerPos.position);
            _navMeshAgent.speed = _npcManager.PlayerController.MSpeed + 1;
        }
        
        //movimientos
        Transform transform2;
        Transform transform3;
        (transform2 = (transform3 = transform)).Translate(new Vector3(0,0, time * speed));
        var transform1 = _animator.transform;
        transform1.position = transform2.position;
        transform1.rotation = transform2.rotation;

        
        
        var player = playerPos.position;
        var dis = Vector3.Distance(transform3.position, player);
        if (dis < 15 && !following)
        {
            var random = Random.Range(0, maxEmojis);
            if (random == 0) StartCoroutine(_npcManager.WhenNpcIsScared(transform2));
            _npcState = NpcState.ScareRun;
            NpcSelector();
            transform.rotation = new Quaternion(0, 0, 0, 0);
            following = true;
        }
    }

    private void NpcSelector()
    {
        switch (_npcState)
        {
            case NpcState.Walk: anim = 0; 
                speed = 1.5f;
                break;
            case NpcState.Idle: anim = 1;
                speed = 0;
                break;
            case NpcState.Running: anim = 2;
                speed = 6;
                break;
            case NpcState.ScareRun: anim = 10;
                speed = 7f;
                break;
            case NpcState.Talking: anim = 3;
                speed = 0;
                break;
            case NpcState.Thinking: anim = 4;
                speed = 0;
                break;
            case NpcState.WalkAndText: anim = 6;
                speed = 1.5f;
                break;
            case NpcState.Text: anim = 5;
                speed = 0;
                break;
            case NpcState.JumpToPool: anim = 11;
                speed = 2;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        _animator.SetInteger(mode, anim);
        //_animator.speed = Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAngry == false && other.CompareTag("Player"))
        {
            isAngry = true;
            var playerController = other.GetComponentInParent<PlayerController>();
            playerPos = other.transform;
            _npcManager.NpcDead++;
            _npcManager.PlaySoundRobbery();

            playerController.Attack();

            if (isPolice)
            {
                //StartCoroutine(IsNpcAngry());
            }

            if (objectToRobbery != null)
            {
                //StolenObject();
            }
            NpcDeadMode(true);
        }
    }

    public void NpcDeadMode(bool toSlider)
    {
        if (isAngry)
        {
            GetComponent<BoxCollider>().enabled = false;
            //_npcManager.WhenNpcStolen(transform, cash , StolenObjectToHolder());
            if (toSlider)
            {
                GameManager.Instance.SliderValueAdd(_npcManager.ValueToSlider);
            }
            int randomN = Random.Range(1, 4);
            _animator.SetInteger(deadNro, randomN);
            _animator.SetInteger(mode, 100);
            speed = 0;
            _navMeshAgent.enabled = false;
        }
    }

    public IEnumerator JumpInToThePool()
    {
        if (following)
        {
            transform.Rotate(0,-90,0);
            _npcState = NpcState.JumpToPool;
            NpcSelector();
            _boxCollider.enabled = false;
            yield return new WaitForSeconds(1f);
            _rigidbody.useGravity = true;
        }
        
    }
    
    public float speedAnim = 1.4f;
    IEnumerator IsNpcAngry()
    {
        yield return new WaitForSeconds(.75f);
        _animator.SetInteger(deadNro, 100 );
        _npcState = NpcState.Running;
        _animator.speed = speedAnim;
        NpcSelector();
        _navMeshAgent.enabled = true;
        _boxCollider.enabled = true;
    }
    
    IEnumerator SpawnEmojiCanva()
    {
        //int random = Random.Range(0, emojiCanva.Length);
        yield return new WaitForSeconds(2f);
        //emojiCanva[random].SetActive(true);
        yield return new WaitForSeconds(1f);
        //emojiCanva[random].SetActive(false);
    }

    private void StolenObject()
    {
        foreach (var obj in objectToRobbery)
        {
            obj.transform.SetParent(null);
            obj.GetComponent<BoxCollider>().isTrigger = false;
            obj.GetComponent<Rigidbody>().isKinematic = false;
            StartCoroutine(MoveObjectsToBag(obj));
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator MoveObjectsToBag(GameObject objectToTween)
    {
        yield return new WaitForSeconds(.55f);
        objectToTween.GetComponent<Rigidbody>().useGravity = false;
        objectToTween.GetComponent<BoxCollider>().isTrigger = true;
        objectToTween.GetComponent<ObjectSettings>().follow1 = true;
        // LeanTween.move(objectToTween, playerPos.position + new Vector3(0,0,3), .5f).setOnComplete((() =>
        // {
        //     _npcManager.AddItemToBagValue();
        //     objectToTween.SetActive((false));
        // }));
    }

    private Transform StolenObjectToHolder()
    {
        if (objectToRobbery.Length > 0 )
        {
            return objectToRobbery[Random.Range(0, objectToRobbery.Length - 1)].transform;
        }
        else
        {
            return null;
        }
    }
}
