using UnityEngine;

public class Cookie : MonoBehaviour
{
    Transform player;
    public Transform owner;
    public float moveSpeed = 3f;

    [SerializeField]private bool isFollowing = false;
    [SerializeField]private bool isReturningToOwner = false;
    Animator animator;

    void Start()
    {
        player = Player.Instance.transform;
        animator = GetComponent<Animator>();
        owner = GameObject.FindGameObjectWithTag("Owner").transform;
    }

    void Update()
    {
        // Nếu đang theo người chơi
        if (isFollowing && !isReturningToOwner)
        {
            MoveTowards(player);
            FacePlayer();
        }

        // Nếu đang trở về chủ nhân (Owner)
        if (isReturningToOwner)
        {
            MoveTowards(owner);
        }
    }

        private void FacePlayer()
    {
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    // Hàm di chuyển đến mục tiêu
    void MoveTowards(Transform target)
    {
        
        float distance = Vector2.Distance(transform.position, target.position);

        // Cập nhật trạng thái animation
        if (distance <= 0.3f)
        {
            animator.SetBool("isRunning", false);

            // Nếu đến chủ, cập nhật nhiệm vụ hoàn thành
            if (target == owner)
            {
                CompleteQuest();
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (owner.CompareTag(other.tag))
        {
            isReturningToOwner = true;
            isFollowing = false;
            Debug.Log("see owner");
            return;
        }

        if (!isReturningToOwner)
        {
            if (other.CompareTag("Player"))
            {
                isFollowing = true;
                isReturningToOwner = false;
                Debug.Log("see player");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowing = false;
            animator.SetBool("isRunning", false); // Dừng animation chạy khi ra khỏi phạm vi
        }
    }

    // Hàm hoàn thành nhiệm vụ
    void CompleteQuest()
    {
        Debug.Log("Nhiệm vụ hoàn tất!");
        GameManager.Instance.killCountBoss = 1;
        // Dừng di chuyển và animation
        isReturningToOwner = false;
        animator.SetBool("isRunning", false);
    }
}
