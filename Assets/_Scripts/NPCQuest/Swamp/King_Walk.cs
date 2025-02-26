using UnityEngine;

public class King_Walk : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    KingEclipseNPC king;
    public float speed = 0;
    public float attackRange = 3.5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       rb = animator.GetComponent<Rigidbody2D>();
       king = animator.GetComponent<KingEclipseNPC>();
       if(king != null){
        speed = king.survivability * 1.2f;
        Debug.Log("speed: " + speed);
       }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if(king.isWalking){
            king.FacePlayer();
            Vector2 target = new(player.position.x+2f, player.position.y+1.2f);
            if(king.isFlipped){
                target = new(player.position.x-2f, player.position.y+1.2f);
            }
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            if(Vector2.Distance(player.position,rb.position) <= attackRange){
                animator.SetTrigger("Attack");
            }
            else
            {
                //Debug.Log("Distance: " + Vector2.Distance(player.position, rb.position));
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
