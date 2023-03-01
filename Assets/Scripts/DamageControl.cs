using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControl : MonoBehaviour
{
    public Collider fw, ln, rw;
    public GameObject fwReal, lnReal, rwReal;
    public GameObject fwFake, lnFake, rwFake;

    public CapsuleCollider rr, rl;
    public GameObject rrReal, rrFake, rlReal, rlFake;
    public CapsuleCollider fr, fl;
    public GameObject frReal, frFake, flReal, flFake;

    public Transform damageParent;

    Vector3 fwOrig, lnOrig, rwOrig;

    Vector3 rrOrig, rlOrig, frOrig, flOrig;

    // Start is called before the first frame update
    void Start()
    {
        fwOrig = fwFake.transform.localPosition;
        rwOrig = rwFake.transform.localPosition;
        lnOrig = lnFake.transform.localPosition;

        rrOrig = rrFake.transform.localPosition;
        rlOrig = rlFake.transform.localPosition;
        frOrig = frFake.transform.localPosition;
        flOrig = flFake.transform.localPosition;

        rwFake.SetActive(false);
        fwFake.SetActive(false);
        lnFake.SetActive(false);

        rrFake.SetActive(false);
        rlFake.SetActive(false);
        frFake.SetActive(false);
        flFake.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("tracklimits") && !collision.gameObject.CompareTag("rumblestrips") && !collision.gameObject.CompareTag("terrain"))
        {
            ContactPoint contact = collision.contacts[0];
            if (contact.thisCollider == fw)
            {
                if (collision.impulse.magnitude > 3000)
                {
                    fw.enabled = false;
                    fwReal.SetActive(false);

                    fwFake.SetActive(true);
                    fwFake.transform.SetParent(null);
                }
            }
            else if (contact.thisCollider == rw)
            {
                if (collision.impulse.magnitude > 1000)
                {
                    rw.enabled = false;
                    rwReal.SetActive(false);

                    rwFake.SetActive(true);
                    rwFake.transform.SetParent(null);
                }
            }
            else if (contact.thisCollider == ln)
            {
                if (collision.gameObject.name != "fwFake")
                {
                    if (collision.impulse.magnitude > 6000)
                    {
                        ln.enabled = false;
                        if (fw.enabled)
                        {
                            fw.enabled = false;
                        }
                        lnReal.SetActive(false);
                        if (fwReal.activeSelf)
                        {
                            fwReal.SetActive(false);
                        }

                        lnFake.SetActive(true);
                        lnFake.transform.SetParent(null);
                        if (!fwFake.activeSelf)
                        {
                            fwFake.SetActive(true);
                            fwFake.transform.SetParent(null);
                        }
                    }
                }
            }
            else if (contact.thisCollider == rr)
            {
                if (collision.impulse.magnitude > 1000)
                {
                    rr.GetComponent<WheelCollider>().enabled = false;
                    rr.enabled = false;
                    rrReal.SetActive(false);

                    rrFake.SetActive(true);
                    rrFake.transform.SetParent(null);
                }
            }
            else if (contact.thisCollider == rl)
            {
                if (collision.impulse.magnitude > 1000)
                {
                    rl.GetComponent<WheelCollider>().enabled = false;
                    rl.enabled = false;
                    rlReal.SetActive(false);

                    rlFake.SetActive(true);
                    rlFake.transform.SetParent(null);
                }
            }
            else if (contact.thisCollider == fr)
            {
                if (collision.impulse.magnitude > 1000)
                {
                    fr.GetComponent<WheelCollider>().enabled = false;
                    fr.enabled = false;
                    frReal.SetActive(false);

                    frFake.SetActive(true);
                    frFake.transform.SetParent(null);
                }
            }
            else if (contact.thisCollider == fl)
            {
                if (collision.impulse.magnitude > 1000)
                {
                    fl.GetComponent<WheelCollider>().enabled = false;
                    fl.enabled = false;
                    flReal.SetActive(false);

                    flFake.SetActive(true);
                    flFake.transform.SetParent(null);
                }
            }
        }
    }

    void ResetDamaged(Collider realColl, GameObject real, GameObject fake, Vector3 origPos)
    {
        if (!realColl.enabled)
        {
            realColl.enabled = true;
        }
        if (!real.activeSelf)
        {
            real.SetActive(true);
        }
        if (fake.transform.parent == null)
        {
            fake.transform.SetParent(damageParent);
        }
        if (fake.activeSelf)
        {
            fake.SetActive(false);
        }
    }
}
