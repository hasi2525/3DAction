using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    public void Effect()
    {
        ParticleSystem newParticle = Instantiate(particle);
        newParticle.transform.position = this.transform.position;
        // �p�[�e�B�N���𔭐�������B
        newParticle.Play();
        // �C���X�^���X�������p�[�e�B�N���V�X�e����GameObject��5�b��ɍ폜����
        // ����������newParticle�����ɂ���ƃR���|�[�l���g�����폜����Ȃ�
        Destroy(newParticle.gameObject, 1.0f);
    }
}