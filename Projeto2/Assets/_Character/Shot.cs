using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public Transform Pistol;
    public ParticleSystem ShotEffect;
    public AudioSource Tiro;
    public GameObject WoodEffect;
    public GameObject StoneEffect;
    Animator anim;
    float ShotDuration;
    public static bool shooting;
    float attackSpeed = 3f;
    float attackColdown = 0f;
    public int PistolDamage;

    public int bulletCount;
    public static bool canShoot;



    void Start ()
    {
        anim = GetComponent<Animator>();
        ShotDuration = 0.0f;
        shooting = false;
        bulletCount = 0;
        canShoot = true;
    }

	void Update ()
    {
       
        attackColdown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (attackColdown <= 0f && canShoot)
            {
                ShotFire();
                shooting = true;
                bulletCount++;
                Debug.Log(bulletCount);
                Tiro.Play();
                attackColdown = 1f / attackSpeed;
                
            }
        }

        if (shooting)
        {
            if (ShotDuration < 0.15f)
                ShotDuration += Time.deltaTime;

            else
            {
                ShotEffect.gameObject.SetActive(false);
                ShotDuration = 0.0f;
                shooting = false;
            }
        }
        if (bulletCount > 15)
        {
            anim.SetBool("Gun", false);
            canShoot = false;

        }
    }

    void ShotFire()
    {
        if (canShoot)
        {

            ShotEffect.gameObject.SetActive(true);
            ShotEffect.Play();
            RaycastHit hit;
            Vector3 forward = transform.TransformDirection(Vector3.forward * 10);
            Debug.DrawRay(Pistol.position, forward, Color.red);
            Ray ray = new Ray(Pistol.position, transform.forward);

            if (anim.GetBool("Gun") == true)
            {
                if (Physics.Raycast(ray, out hit, 200))
                {
                    Debug.Log("Hit: " + hit.collider.tag);
                    if (hit.collider.tag == "Stone")
                    {
                        GameObject go = Instantiate(StoneEffect, hit.point, Quaternion.identity);
                        Destroy(go, 1f);
                    }
                    else if (hit.collider.tag == "Tree")
                    {
                        GameObject go = Instantiate(WoodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(go, 1f);
                    }
                    else if (hit.collider.tag == "house")
                    {
                        GameObject go = Instantiate(WoodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(go, 1f);
                    }
                    else if (hit.collider.tag == "Foundation")
                    {
                        GameObject go = Instantiate(WoodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(go, 1f);
                    }
                    else if (hit.collider.tag == "woodFence")
                    {
                        GameObject go = Instantiate(WoodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(go, 1f);
                    }
                    else if (hit.collider.tag == "Enemy")
                    {
                        hit.collider.GetComponentInParent<WolfAnimController>().GetDamage(PistolDamage);
                    }
                }
            }
        }
    }

}
