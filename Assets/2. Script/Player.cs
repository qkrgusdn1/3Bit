using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviourPunCallbacks
{
    public Transform bodyTr;
    public Transform shootTr;
    public float horSensitivity;
    public float vertSensitivity;
    public float rotationY;
    public float rotationX;
    public Rigidbody rb;

    [Header("움직임")]
    public float moveSpeed;
    public float maxMoveSpeed;

    [Header("점프")]
    protected float jumpForce;
    public float maxJumpForce;
    protected bool isJumping;
    public bool isGrounded;

    public LayerMask groundLayer;

    protected Animator animator;
    protected Vector3 dir;

    public float hp;
    public float maxHp;
    public Image hpBar;

    public Skill currentSkill;
    public float stunTimer;
    public float maxStunTimer;
    Coroutine moveBackCoroutine;
    public bool back;

    public GameObject canvas;

    public float skillTimer;
    public float maxSkillTimer;
    public Image skillTime;

    public bool startPlayer;

    public TMP_Text skillTimerText;

    public virtual void Start()
    {
        if (!photonView.IsMine)
        {
            canvas.SetActive(false);
        }
        else
        {
            if (!startPlayer)
            {
                canvas.SetActive(true);
            }

        }
        animator = GetComponentInChildren<Animator>();
        if (photonView.IsMine)
        {
            CameraMgr.Instance.SetTarget(this);
        }
        skillTimer = maxSkillTimer;
        skillTime.fillAmount = 1;
    }
    public virtual void Awake()
    {
        stunTimer = maxStunTimer;
        currentSkill = Skill.Default;
        moveSpeed = maxMoveSpeed;
        jumpForce = maxJumpForce;
        hp = maxHp;
        hpBar.fillAmount = 1;
    }

    public virtual void Update()
    {
        dir = new Vector3(0, 0, 0);
        if (PhotonNetwork.InRoom)
        {
            if (photonView == null)
                return;

            if (!photonView.IsMine)
                return;
        }

        if (Physics.Raycast(bodyTr.position, Vector3.down, 0.1f, groundLayer))
        {
            isGrounded = true;
            isJumping = false;
        }
        else
        {
            isGrounded = false;
        }




        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping)
        {
            isJumping = true;
            photonView.RPC("RpcJump", RpcTarget.All);
        }
        Move();
        if (hp <= 0)
            photonView.RPC("RpcDestroy", RpcTarget.All);
        Rotation();

        if (back)
        {
            moveBackCoroutine = StartCoroutine(MoveBackCoroutine());
        }
        else
        {
            if (moveBackCoroutine != null)
            {
                StopCoroutine(moveBackCoroutine);
            }
            Move();
        }
    }

    IEnumerator MoveBackCoroutine()
    {
        transform.Translate(-transform.forward * moveSpeed * Time.deltaTime);
        yield return new WaitForSeconds(2f);
        back = false;
    }

    public virtual void ApplySkill(Skill skill)
    {
        if(photonView.IsMine)
            photonView.RPC("RPCApplySkill", RpcTarget.All, skill);
    }

    [PunRPC]
    public void RPCApplySkill(Skill skill)
    {
        currentSkill = skill;
        if (currentSkill == Skill.Default)
        {
            
        }
        else if (currentSkill == Skill.Stun)
        {
            animator.Play("Stun");
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        photonView.RPC("RpcTakeDamage", RpcTarget.All);
    }

    public void Rotation()
    {
        rotationY += Input.GetAxis("Mouse X") * horSensitivity;
        rotationX -= Input.GetAxis("Mouse Y") * vertSensitivity;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        bodyTr.localEulerAngles = new Vector3(0, rotationY, 0);
        //CameraMgr.Instance.transform.localEulerAngles = new Vector3(rotationX, 0, 0);
    }

    public void SetPower(string power)
    {
        photonView.RPC("RPCSetPower", RpcTarget.All, power);
    }
    [PunRPC]
    public void RPCSetPower(string power)
    {
        if (photonView.IsMine == false)
            return;

        if(power == "LightningMan")
        {
            GameObject lightningMan = PhotonNetwork.Instantiate(power, transform.position, transform.rotation);
        }
        else if(power == "Tagger")
        {
            GameObject tagger = PhotonNetwork.Instantiate(power, transform.position, transform.rotation);
        }
        else if(power == "HammerMan")
        {
            GameObject hammerMan = PhotonNetwork.Instantiate(power, transform.position, transform.rotation);
        }
        else if (power == "Speeder")
        {
            GameObject speeder = PhotonNetwork.Instantiate(power, transform.position, transform.rotation);
        }

        PhotonNetwork.Destroy(gameObject);
    }
    void Move()
    {
        bool isMoving = false;
        if (Input.GetKey(KeyCode.W))
        {
            dir += bodyTr.forward * moveSpeed;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir += -bodyTr.right * moveSpeed;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir += bodyTr.right * moveSpeed;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir += -bodyTr.forward * moveSpeed;
            isMoving = true;
        }
        if (!isMoving && !isJumping)
        {
            animator.SetBool("IsRunning", false);
        }
        else if(isMoving && !isJumping)
        {
            animator.SetBool("IsRunning", true);
        }
        rb.velocity = new Vector3(dir.x, rb.velocity.y, dir.z);

    }

    [PunRPC]
    public void RpcDestroy()
    {
        Destroy(gameObject);

    }

    //[PunRPC]
    //public void RpcMove(bool move)
    //{
    //    if (animator == null) 
    //        return;
    //    if (!move && !isJumping)
    //    {
    //        animator.CrossFade("Idle", 0, 0);
    //    }
    //    else if (move && !isJumping)
    //    {
    //        animator.CrossFade("Run", 0, 0);
    //    }
    //}

    

    

    [PunRPC]
    public void RpcTakeDamage()
    {
        hpBar.fillAmount = hp / maxHp;
    }

    public virtual void Shoot()
    {
        
    }
    [PunRPC]
    public virtual void RpcShoot(Vector3 direction, int shooterID)
    {
        
    }
    [PunRPC]
    public void RpcJump()
    {
        animator.Play("Jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
public enum Skill
{
    Default,
    Stun,
}
