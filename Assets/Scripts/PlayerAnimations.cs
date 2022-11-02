using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        updateAnimations(horizontal, vertical);

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0 ,gameObject.transform.position.z);
        
    }

    void updateAnimations(float horizontal, float vertical) {
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if(direction != Vector3.zero){
            animator.SetBool("Moving", true);
        }
        else {
            animator.SetBool("Moving", false);
        }
        if(Input.GetKeyDown(KeyCode.E)){
            animator.SetTrigger("Attack");
        }
        else if(Input.GetKeyDown(KeyCode.Space)){
            animator.SetTrigger("Jump");
        }
    }
}
