using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private InputAction action;

    private Animator animator;
    private int currentState = 0;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        action.performed += _ => SwitchState();
    }


    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    private void SwitchState()
    {
        switch (currentState)
        {
            case 1:
                animator.SetInteger("CurrentState", currentState + 1);
                currentState ++;
                break;
            case 2:
                animator.SetInteger("CurrentState", currentState + 1);
                currentState++;
                break;
            case 3:
                animator.SetInteger("CurrentState", currentState + 1);
                currentState++;
                break;
            case 4:
                animator.SetInteger("CurrentState", currentState + 1);
                currentState++;
                break;
            case 5:
                animator.SetInteger("CurrentState", currentState + 1);
                currentState++;
                break;
            default:
                break;
        }
    }

}
