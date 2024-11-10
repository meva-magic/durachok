using UnityEngine;

public class DurachokFollower : MonoBehaviour
{
    public Transform player;               // ������ �� ������
    public float followDistance = 2f;      // ��������� ����������
    public float followSpeed = 2f;         // �������� ����������
    public float levitateHeight = 1f;      // ������ ���������
    public float activationRadius = 5f;    // ������ ��� �����������
    public float smoothTime = 0.3f;        // ����� ��� �����������

    private bool isFollowing = false;      // ������� �� �� �������
    private bool isAbsorbed = false;       // ��������� �� ������ ������

    private DurachokAbsorption absorptionScript;

    // ��� �����������
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // �������� ������ �� ��������� DurachokAbsorption
        absorptionScript = GetComponent<DurachokAbsorption>();
        absorptionScript.player = player;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ��������� ���������� ��� ��������� Enter � ���������� � �������
        if (Input.GetKeyDown(KeyCode.Space) && distanceToPlayer <= activationRadius)
        {
            isFollowing = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isFollowing = false;
        }

        // ��������� ����������� ���������� ��� ������� Space � ���������� � �������
        if (Input.GetKeyDown(KeyCode.Return) && distanceToPlayer <= activationRadius && !isAbsorbed)
        {
            isAbsorbed = true;
            StartCoroutine(absorptionScript.AbsorbIntoPlayer());
            Invoke(nameof(ResetAbsorbedStatus), absorptionScript.invisibleDuration + 1f);
        }

        // ���������� �� �������, ���� �� � ������� ����������� � �� ��������
        if (isFollowing && !isAbsorbed && !absorptionScript.isInvisible)
        {
            // ������������ ������� ���� ������ � ������� ������
            Vector3 targetPosition = player.position + player.forward * followDistance + player.right * 1f; // 1f - �������� ������
            targetPosition.y += levitateHeight; // ��������� ������ ���������

            // ���������� Lerp ��� �������� ��������
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    void ResetAbsorbedStatus()
    {
        isAbsorbed = false;
    }
}
