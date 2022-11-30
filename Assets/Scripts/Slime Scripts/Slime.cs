using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Slime : MonoBehaviour
{
    [SerializeField] public string slimeType;

    //State Machine
    private Dictionary<Type, ISlimeBehavior> behaviorsMap;
    private ISlimeBehavior behaviorCurrent;

    //Slime Components
    public Rigidbody body;
    public Animator anim;

    //Environment objects
    public GameObject bag;
    public GameObject currentCoins;
    public LayerMask coinsMask;
    public Transform coinHolder;

    //Slime properties
    public float speed = 3f;
    public float searchingRange = 100f;

    //Trading values
    public float stopLossBalance;
    public float riskManagementPercent;

    // Start is called before the first frame update
    void Awake()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }

    private void Start()
    {
        riskManagementPercent = GameManager.instance.GetRiskByType(this.slimeType);

        this.InitBehaviors();
        this.SetBehaviorByDefault();
    }

    private void InitBehaviors()
    {
        this.behaviorsMap = new Dictionary<Type, ISlimeBehavior>();

        this.behaviorsMap[typeof(SlimeChasingBehavior)] = new SlimeChasingBehavior();
        this.behaviorsMap[typeof(SlimeReturningBehavior)] = new SlimeReturningBehavior();
        this.behaviorsMap[typeof(SlimeDamageBehavior)] = new SlimeDamageBehavior();
        this.behaviorsMap[typeof(SlimeEscapingBehavior)] = new SlimeEscapingBehavior();
    }

    private void SetBehavior(ISlimeBehavior newBehavior)
    {
        if (this.behaviorCurrent != null)
            this.behaviorCurrent.Exit(this);

        this.behaviorCurrent = newBehavior;
        this.behaviorCurrent.Enter(this);
    }

    private void SetBehaviorByDefault()
    {
        this.SetBehaviorChasing();
    }

    private ISlimeBehavior GetBehavior<T>() where T : ISlimeBehavior
    {
        var type = typeof(T);
        return this.behaviorsMap[type];
    }

    // Update is called once per frame
    private void Update()
    {
        if (this.behaviorCurrent != null)
            this.behaviorCurrent.Update(this);
    }

    private void FixedUpdate()
    {
        if (this.behaviorCurrent != null)
            this.behaviorCurrent.FixedUpdate(this);
    }

    public void SetBehaviorChasing()
    {
        var behavior = this.GetBehavior<SlimeChasingBehavior>();
        this.SetBehavior(behavior);
    }

    public void SetBehaviorReturning()
    {
        var behavior = this.GetBehavior<SlimeReturningBehavior>();
        this.SetBehavior(behavior);
    }

    public void SetBehaviorDamaging()
    {
        var behavior = this.GetBehavior<SlimeDamageBehavior>();
        this.SetBehavior(behavior);
    }

    public void SetbehaviorEscaping()
    {
        var behavior = this.GetBehavior<SlimeEscapingBehavior>();
        this.SetBehavior(behavior);
    }

    public void DestorySlime()
    {
        Destroy(this.gameObject);
    }

    public void OnDestroy()
    {
        if (currentCoins != null)
        {
            currentCoins.GetComponent<Coins>().isTaken = false;
        }
    }

}
