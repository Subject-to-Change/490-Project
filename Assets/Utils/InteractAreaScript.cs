using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAreaScript : MonoBehaviour
{
    private Collider2D collisionBox;
    private GameObject player;
    private Collider2D playerCollider;
    private bool previousState = false;

    [SerializeField]
    private bool _isAvailible = true;

    
    public UnityEngine.Events.UnityEvent onActivate;
    public UnityEngine.Events.UnityEvent onPopupShow;
    public UnityEngine.Events.UnityEvent onPopupHide;

    public bool IsAvailible{
        get{
            return _isAvailible;
        }
        set{
            _isAvailible = value;
            if(playerInBounds()) {
                if(_isAvailible)
                    onPlayerEnter();
                else{
                    visibleWhenInAreaNode.SetActive(false);
                }
            }
        }
    }

    [SerializeReference]
    public GameObject visibleWhenInAreaNode = null;


    public UnityEngine.InputSystem.InputActionReference activate;

    // Start is called before the first frame update
    void Start()
    {
        collisionBox = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) {
            Debug.LogError("Activation area could not find player gameObject.  Looking for \"Player\" tagged object in scene.");
        }
        playerCollider = player.GetComponent<Collider2D>();
        activate.ToInputAction().Enable();
        activate.ToInputAction().started += onActivateKeyDown;
    }

    // Physics update loop
    void FixedUpdate()
    {
        //Not ideal implementation!!!  It would be better to use trigger on enter/exit functions but those did not work when tested
        if(previousState != playerInBounds()) {
            previousState = !previousState;
            if(previousState)
                onPlayerEnter();
            else
                onPlayerExit();
        }
    }

    bool playerInBounds() {
        return collisionBox.IsTouching(playerCollider);
    }

    void onPlayerEnter() {
        onPopupShow.Invoke();
        if(visibleWhenInAreaNode==null) return;
        visibleWhenInAreaNode.SetActive(true);
    }

    void onPlayerExit() {
        onPopupHide.Invoke();
        if(visibleWhenInAreaNode==null) return;
        visibleWhenInAreaNode.SetActive(false);
    }

    void onActivateKeyDown(UnityEngine.InputSystem.InputAction.CallbackContext data) {
        if(playerInBounds() && IsAvailible) {
            onActivate.Invoke();
        }
    }
}
