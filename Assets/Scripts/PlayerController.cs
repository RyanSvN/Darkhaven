using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    //Third Person Controller
    [SerializeField]
    private Animator playerAnim;

    //Equip-unequip parameters
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private GameObject swordOnShoulder;
    public bool isEquipping;
    public bool isEquipped;

    //Block Parameters
    public bool isBlocking;
    

    //Kick Parameters
    private bool isKicking;

    //Attack Parameters
    public bool isAttacking;
    private float timeSinceAttack;
    public int currentAttack = 0;


    [SerializeField]
    private GameObject damageBox;

    //Sword Slash
    public VisualEffect swordSlashEffect1;
    public VisualEffect swordSlashEffect2;
    public VisualEffect swordSlashEffect3;

    //Audio
    public AudioClip slashSound1;
    private AudioSource audioSource1;

    public AudioClip slashSound2;
    private AudioSource audioSource2;
    
    private void Start()
    {
        // Get or add AudioSource component to the same GameObject
        audioSource1 = GetComponent<AudioSource>();
        if (audioSource1 == null)
        {
            audioSource1 = gameObject.AddComponent<AudioSource>();
        }

        // Assign the slashing sound to the AudioSource
        audioSource1.clip = slashSound1;

        audioSource2 = GetComponent<AudioSource>();
        if (audioSource2 == null)
        {
            audioSource2 = gameObject.AddComponent<AudioSource>();
        }

        // Assign the slashing sound to the AudioSource
        audioSource2.clip = slashSound2;
    }


    private void Update()
    {

        timeSinceAttack += Time.deltaTime;

        Attack();

        Equip();
        Block();
        Kick();
    }


    private void Equip()
    {
        if (Input.GetKeyDown(KeyCode.R) && playerAnim.GetBool("Grounded"))
        {
            isEquipping = true;
            playerAnim.SetTrigger("Equip");
        }
    }

    public void ActiveWeapon()
    {
        if (!isEquipped)
        {
            sword.SetActive(true);
            swordOnShoulder.SetActive(false);
            isEquipped = !isEquipped;
        }
        else
        {
            sword.SetActive(false);
            swordOnShoulder.SetActive(true);
            isEquipped = !isEquipped;
        }
    }

    public void Equipped()
    {
        isEquipping = false;
    }

    private void Block()
    {
        if (Input.GetKey(KeyCode.Mouse1) && playerAnim.GetBool("Grounded"))
        {
            playerAnim.SetBool("Block", true);
            isBlocking = true;
        }
        else
        {
            playerAnim.SetBool("Block", false);
            isBlocking = false;
        }
    }

    private void Kick()
    {
        if (Input.GetKey(KeyCode.LeftControl) && playerAnim.GetBool("Grounded"))
        {
            playerAnim.SetBool("Kick", true);
            isKicking = true;
        }
        else
        {
            playerAnim.SetBool("Kick", false);
            isKicking = false;
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown("f") && playerAnim.GetBool("Grounded") && timeSinceAttack > 0.8f)
        {
            if (!isEquipped)
            return;

            currentAttack++;
            isAttacking = true;

            if (currentAttack > 3)
                currentAttack = 1;

            //Reset
            if (timeSinceAttack > 1.0f)
                currentAttack = 1;
            
            //Call Attack Triggers
            playerAnim.SetTrigger("Attack" + currentAttack);

            //Reset Timer
            timeSinceAttack = 0;


            damageBox.SetActive(true);

            PlaySlashSound1();

        }

        else if (Input.GetKeyUp("f"))
        {
            damageBox.SetActive(false);
        }
    }

    //Used at animation event
    public void ResetAttack()
    {
        isAttacking = false;
    }

    public void TriggerSwordSlashEffect1()
    {
        swordSlashEffect1.Play();
    }

    public void TriggerSwordSlashEffect2()
    {
        swordSlashEffect2.Play();
    }

    public void TriggerSwordSlashEffect3()
    {
        swordSlashEffect3.Play();
    }

    private void PlaySlashSound1()
    {
        if (slashSound1 != null && audioSource1 != null)
        {
            audioSource1.PlayOneShot(slashSound1);
        }
    }

    private void PlaySlashSound2()
    {
        if (slashSound2 != null && audioSource2 != null)
        {
            audioSource2.PlayOneShot(slashSound2);
        }
    }
}
