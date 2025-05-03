using TMPro;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // �v���C���[�̈ړ��p�ϐ�
    float x, z;

    // �v���C���[�̈ړ����x
    float speed = 0.1f;

    // �J�����̎Q��
    public GameObject cam;

    // �J�����ƃL�����N�^�[�̉�]���Ǘ�����ϐ�
    Quaternion cameraRot, characterRot;

    // �}�E�X���x
    float Xsensitivity = 3f, Ysensitivity = 3f;

    // �J�[�\�����b�N��Ԃ��Ǘ�����t���O
    bool cursorLock = true;

    // �J�����̉�]�p�x����
    float minX = -90f, maxX = 90f;

    // �A�j���[�^�[�̎Q��
    public Animator animator;

    void Start()
    {
        // �J�����ƃL�����N�^�[�̏�����]���擾
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
    }

    // ���t���[���Ăяo����鏈��
    void Update()
    {
        // �}�E�X�̓��͂��擾
        float xRot = Input.GetAxis("Mouse X") * Xsensitivity;
        float yRot = Input.GetAxis("Mouse Y") * Ysensitivity;

        // �J�����ƃL�����N�^�[�̉�]���X�V
        cameraRot *= Quaternion.Euler(-yRot, 0, 0); // �J�����̏㉺��]
        characterRot *= Quaternion.Euler(0, xRot, 0); // �L�����N�^�[�̍��E��]

        // �J�����̉�]�𐧌�
        cameraRot = ClampRotation(cameraRot);

        // �J�����ƃL�����N�^�[�̉�]��K�p
        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

        // �J�[�\�����b�N��Ԃ��X�V
        UpdateCursorLock();


    }

    // �v���C���[�̈ړ�����
    void FixedUpdate()
    {
        // ���͂Ɋ�Â��Ĉړ��ʂ��v�Z
        x = Input.GetAxisRaw("Horizontal") * speed; // ���E�ړ�
        z = Input.GetAxisRaw("Vertical") * speed;   // �O��ړ�

        // �v���C���[�̈ʒu���X�V
        transform.position += transform.forward * z + transform.right * x;
    }

    // �J�[�\�����b�N��Ԃ��Ǘ����鏈��
    public void UpdateCursorLock()
    {
        // Escape�L�[�ŃJ�[�\�����b�N��؂�ւ�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = !cursorLock;
        }
        // ���N���b�N�ŃJ�[�\�����b�N��L����
        else if (Input.GetMouseButtonDown(0))
        {
            cursorLock = true;
        }

        // �J�[�\�����b�N��Ԃ�K�p
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked; // �J�[�\�������b�N
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // �J�[�\�������
        }
    }

    // �J�����̉�]�𐧌����鏈��
    public Quaternion ClampRotation(Quaternion q)
    {
        // �N�H�[�^�j�I���𐳋K��
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        // X���̉�]�p�x���v�Z
        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        // X���̉�]�p�x�𐧌�
        angleX = Mathf.Clamp(angleX, minX, maxX);

        // ������̊p�x���N�H�[�^�j�I���ɕϊ�
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
