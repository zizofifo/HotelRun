using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorObstacle : MonoBehaviour
{
    public enum DoorObstacleState
    {
        Opened,
        Closed
    }

    public DoorObstacleState initialState;

    public float chanceToOpen = .0005f;
    public float minimumTimeAllowedOpened = 2;
    public float maximumTimeAllowedOpened = 5;

    [Header("Internal State")]
    [SerializeField]
    private DoorObstacleState state;
    [SerializeField]
    private float timeRemainingOpened = 0;

    private Animator anim;
    private GameObject childColliderObject;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        childColliderObject = transform.Find("DoorCollider").gameObject;
        SetState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        DoorObstacleState prevState = state;

        if (timeRemainingOpened > 0 && (timeRemainingOpened -= Time.deltaTime) <= 0)
        {
            timeRemainingOpened = 0;
            SetState(DoorObstacleState.Closed);
        }
        else if (timeRemainingOpened > 0)
        {
            return;
        }

        if (Random.value < chanceToOpen)
        {
            SetState(DoorObstacleState.Opened);
            timeRemainingOpened = Random.Range(minimumTimeAllowedOpened, maximumTimeAllowedOpened);
        }
    }

    private void SetState(DoorObstacleState nextState)
    {
        state = nextState;

        switch (state)
        {
            case DoorObstacleState.Opened:
                anim.SetBool("isOpened", true);
                childColliderObject.SetActive(true);
                break;
            case DoorObstacleState.Closed:
                anim.SetBool("isOpened", false);
                childColliderObject.SetActive(false);
                break;
        }
    }
}
