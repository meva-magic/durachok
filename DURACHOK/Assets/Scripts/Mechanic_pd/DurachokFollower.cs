using UnityEngine;

public class DurachokFollower : MonoBehaviour
{
    [SerializeField] private Transform player;               // ������ �� ������
    [SerializeField] private float followRadius = 1f;        // ������ ����������
    [SerializeField] private float followSpeed = 2f;         // �������� ����������
    [SerializeField] private float levitateHeight = 1f;      // ������ ���������
    [SerializeField] private float activationRadius = 5f;    // ������ ��� �����������
    [SerializeField] private float smoothTime = 0.3f;        // ����� ��� �����������

    private bool isFollowing = false;        // ������� �� �� �������
    private bool isAbsorbed = false;         // ��������� �� ������ ������

    private DurachokAbsorption absorptionScript;
    private Collider durachokCollider;
    private Vector3 velocity = Vector3.zero; // ���������� ��� SmoothDamp

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // �������� ������ �� ��������� DurachokAbsorption
        absorptionScript = GetComponent<DurachokAbsorption>();
        absorptionScript.player = player;

        // �������� ��������� ��� ���������� ��� ��������
        durachokCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // �������� ���������� ��� ������� �� ������, ���� � �������
        if (Input.GetKeyDown(KeyCode.Space) && distanceToPlayer <= activationRadius)
        {
            isFollowing = true;
            durachokCollider.enabled = false; // ��������� ��������� ��� �������� � ������
        }

        // ������������� ����������, ����� ������ �����������
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isFollowing = false;
            durachokCollider.enabled = true; // �������� ��������� ��� ��������� ����������
        }

        // ������ ����������� ���������� ��� ������� Enter, ���� � �������
        if (Input.GetKeyDown(KeyCode.Return) && distanceToPlayer <= activationRadius && !isAbsorbed)
        {
            isAbsorbed = true;
            durachokCollider.enabled = false; // ��������� ��������� ��� ����������
            StartCoroutine(absorptionScript.AbsorbIntoPlayer());
            Invoke(nameof(ResetAbsorbedStatus), absorptionScript.invisibleDuration + 1f);
        }

        // ���������� �� �������, ���� ������������ � �� ��������
        if (isFollowing && !isAbsorbed && !absorptionScript.isInvisible)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        // ������������ ����� �� �������� ������� (���� ������ � ������� ������)
        Vector3 targetPosition = player.position + (player.forward + player.right).normalized * followRadius;
        targetPosition.y += levitateHeight; // ��������� ������� ��� ������� ���������

        // ������ ���������� ������ � ������� �������
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void ResetAbsorbedStatus()
    {
        isAbsorbed = false;
        durachokCollider.enabled = true; // �������� ��������� ����� ���������� ����������
    }
}
