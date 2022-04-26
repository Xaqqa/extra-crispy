using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Playables;

public class player_move : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    float footstepSpeed;
    PlayableDirector jumpAnim;
    PlayableDirector bobbingAnim;
    [SerializeField] List<AudioClip> footstepSounds;
    [SerializeField] GameObject playerCam;


    public static bool inMenu;
    public static bool inPause;

    bool isMoving;
    bool isSprinting;
    bool isGrounded;
    bool isJumping;
    bool isCrouching;

    Rigidbody playerRB;
    
    void Awake()
    {

        jumpAnim = GetComponent<PlayableDirector>();
        bobbingAnim = playerCam.GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            StopCoroutine("FootstepSounds");
            StartCoroutine("FootstepSounds");
        }
    }

    IEnumerator FootstepSounds()
    {
        
        footstepSpeed = .20f;
        

        yield return new WaitForSeconds(footstepSpeed);
        AudioClip temp_footstep = footstepSounds[Random.Range(0, footstepSounds.Count)];
        GetComponent<AudioSource>().clip = temp_footstep;
        GetComponent<AudioSource>().Play();


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            yield return new WaitForSeconds(footstepSpeed);
            StartCoroutine("FootstepSounds");
        }
    }

    void FixedUpdate()
    {
        #region Basic_Movement

        if (!inMenu && !inPause)
        {

            #region Jumping Animation
            /*/
            if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
            {
                
                if (isSprinting)
                {
                    moveSpeed = 0.1f;
                }
                else
                {
                    moveSpeed = 0.05f;
                }

                isGrounded = false;
                isJumping = true;
                jumpAnim.Play();
            }
            /*/
            #endregion

            #region Crouching Animation
            if (Input.GetButtonDown("Crouch") && isGrounded)
            {
                if (isCrouching)
                {
                    isCrouching = false;
                    GetComponent<CapsuleCollider>().height = 2;
                    moveSpeed = 0.1f;
                }
                else
                {
                    isCrouching = true;
                    GetComponent<CapsuleCollider>().height = 1;
                    moveSpeed = 0.05f;
                }
            }
            #endregion

            #region Movement
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                isMoving = true;
                transform.Translate(Vector3.right * moveSpeed * Input.GetAxisRaw("Horizontal"));
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                isMoving = true;
                transform.Translate(Vector3.left * moveSpeed * -Input.GetAxisRaw("Horizontal"));
            }

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                isMoving = true;
                transform.Translate(Vector3.forward * moveSpeed * Input.GetAxisRaw("Vertical"));
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                isMoving = true;
                transform.Translate(Vector3.back * moveSpeed * -Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetAxisRaw("Vertical") == 0 && (Input.GetAxisRaw("Horizontal") == 0) && isMoving)
            {
                isMoving = false;
            }
            #endregion
        }

        #region Head Bobbing Animation
        if (isMoving)
        {
            bobbingAnim.Play();
        }

        #endregion

        #endregion

        #region Pause Menu
        if (Input.GetButtonDown("Pause") && !inMenu)
        {
            if (!inPause)
                /*/
            {
                inPause = false;
                menu_ui.menuSector = "main";
            }
            else
            /*/          
            {
                inPause = true;
                menu_ui.menuSector = "pause";
            }
        }
        #endregion
       
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }
}


