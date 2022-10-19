using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Slime : MonoBehaviour
{
    //State Machine
    private Dictionary<Type, ISlimeBehavior> behaviorsMap;
    private ISlimeBehavior behaviorCurrent;

    //Slime Components
    public Rigidbody body;
    public Animator anim;

    //Environment objects
    public GameObject campfire;
    public GameObject currentApple;
    public LayerMask applesMask;
    public Transform appleHolder;

    //Slime properties
    public float speed = 3f;
    public float searchingRange = 100f;

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

    private void SetBehaviorChasing()
    {
        var behavior = this.GetBehavior<SlimeChasingBehavior>();
        this.SetBehavior(behavior);
    }

    private void SetBehaviorReturning()
    {
        var behavior = this.GetBehavior<SlimeReturningBehavior>();
        this.SetBehavior(behavior);
    }

    private void SetBehaviorDamaging()
    {
        var behavior = this.GetBehavior<SlimeDamageBehavior>();
        this.SetBehavior(behavior);
    }

    private void SetbehaviorEscaping()
    {
        var behavior = this.GetBehavior<SlimeEscapingBehavior>();
        this.SetBehavior(behavior);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == currentApple)
        {
            SetBehaviorReturning();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == campfire)
        {
            Destroy(currentApple);
            SetBehaviorChasing();
        }
    }
}
