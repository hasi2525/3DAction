using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(EnemyStatus))]

/// <summary>
/// �G�̈ړ��𐧌䂷��X�N���v�g
/// </summary>
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayerMask;

    private NavMeshAgent _agent;

    private RaycastHit[] _raycastHits = new RaycastHit[10];

    private EnemyStatus _status;

    void Start()
    {
        //NavMeshAgent���擾
        _agent = GetComponent<NavMeshAgent>();
        _status = GetComponent<EnemyStatus>();
    }

    /// <summary>
    /// ����̃I�u�W�F�N�g�����o�����Ƃ��ɌĂяo����郁�\�b�h
    /// </summary>
    /// <param name="collider">���o���ꂽ�R���C�_�[</param>
    public void OnDetectObject(Collider collider)
    {
        if(! _status.IsMoveble)
        {
            _agent.isStopped = true;
            return;
        }



        // ���o���ꂽ�I�u�W�F�N�g���v���C���[�ł���ꍇ�A���̈ʒu��ړI�n�ɐݒ�
        if (collider.CompareTag("Player"))
        {
            //�����ƃv���C���[�̈ʒu��
            Vector3 positionDiff = collider.transform.position - transform.
            position;
            //�v���C���[�Ƃ̋������v�Z
            float distance = positionDiff.magnitude;
            //�v���C���[�̕���
            Vector3 direction = positionDiff.normalized;

            float hitCount = Physics.RaycastNonAlloc(transform.position,
            direction, _raycastHits, distance, raycastLayerMask);

            Debug.Log("hitCount:" + hitCount);
            if(hitCount == 0)
            {
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;
            }
            else
            {
                //�����������~
                _agent.isStopped = true;
            }
        }
    }

    /// <summary>
    /// NavMeshAgent�̖ړI�n��ݒ肵�܂�
    /// </summary>
    /// <param name="destination">�ړI�n�̍��W</param>
    private void SetDestinationToPlayer(Vector3 destination)
    {
        _agent.destination = destination;
    }
}