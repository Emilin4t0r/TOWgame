using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Missile : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;
    public string targetTag;
    public float AOERadius;
    public float launchForce;
    private Transform target;
    private Rigidbody rb;
    bool isLaunched;
    public GameObject fireEffect;
    public GameObject smokeEffect;
    public GameObject startSmokeEffect;
    public GameObject explosionEffect;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        isLaunched = false;
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        TrackTargetAndMoveOneStep();
        StartCoroutine(LaunchDelayer());
    }

    private IEnumerator LaunchDelayer()
    {
        rb.AddForce(transform.TransformDirection(Vector3.forward) * launchForce, ForceMode.Impulse);
        yield return new WaitForSeconds(GameManager.instance.launchDelay);
        rb.isKinematic = false;
        isLaunched = true;
        fireEffect.SetActive(true);
        smokeEffect.SetActive(true);
        GameObject startSmk = Instantiate(startSmokeEffect, transform.position, Quaternion.identity, null);
        Destroy(startSmk, 5);
        CamShaker.Shake(2);
        transform.GetComponent<SoundPlayer>().PlaySound(0, 1);
    }

    private void Update()
    {
        if (isLaunched)
        {
            TrackTargetAndMoveOneStep();
        }
    }

    void TrackTargetAndMoveOneStep()
    {
        Vector3 targetDirection = target.position - transform.position;
        Vector3 lookDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0f);
        float dist = targetDirection.magnitude;
        transform.Translate(Vector3.forward * Time.deltaTime * speed * dist, Space.Self);
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<UFO>().Kill();            
        }
        BlowUp();
    }

    public void BlowUp()
    {        
        GameObject expl = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(expl, 5);
        smokeEffect.transform.parent = null;        
        DoAOEDamage();
        float dist = Vector3.Distance(Vector3.zero, transform.position);
        if (dist < 500)
            transform.GetComponent<SoundPlayer>().SpawnSound(gameObject, 1, 1);
        else
            transform.GetComponent<SoundPlayer>().SpawnSound(gameObject, 2, 1);
        Destroy(gameObject);
        GameManager.instance.mslTemp = null;
        GameManager.instance.ResetMissile();
        CamShaker.Shake(4);
    }

    public void DoAOEDamage()
    {
        Collider[] hitCols = new Collider[EnemySpawner.instance.activeEnemies.Count];
        int cols = Physics.OverlapSphereNonAlloc(transform.position, AOERadius, hitCols);
        for (int i = 0; i < cols; ++i)
        {
            if (hitCols[i].CompareTag("Enemy"))
            {
                hitCols[i].GetComponent<UFO>().Kill();
            }
        }
    }
}
