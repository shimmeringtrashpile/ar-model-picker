using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is an object declaration
public class CubeClick : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startingLocation;
    bool hasStarted = false;

    [SerializeField]
    AudioSource audiosource;

    [SerializeField]
    AudioClip clink, clonk;

    float currentScale = 1;

    bool shouldShrink = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingLocation = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == true && startingLocation.z >=this.gameObject.transform.position.z)
        {
            print("triggered");
            rb.isKinematic = true;

            rb.isKinematic = false;
            hasStarted = false;
        }

        if (shouldShrink)
        {
            currentScale = Mathf.Lerp(currentScale, 1.0f, .02f);
            this.gameObject.transform.localScale = new Vector3(currentScale, currentScale, currentScale);

        }
    }

    private void OnMouseDown()
    {
        audiosource.clip = clink;
        audiosource.Play();
        // move forward (backwards are negative) * 1000 (very fast)
        // ForceMode is the type of force being added. Impulse uses mass. VelocityChange does not.
        rb.isKinematic = false;
        rb.AddForce(transform.forward * 10, ForceMode.Impulse);
        StartCoroutine(DelayForVariable());

    }

    //Over is continual. Enter is once only.
    private void OnMouseOver()
    {
        print("hi!");
        currentScale = Mathf.Lerp(currentScale, 2.0f, .02f);
        // TODO: Checkout Mathf in unity docs.
        // print(currentScale);
        this.gameObject.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    }

    private void OnMouseEnter()
    {
        // gameObject.transform.localScale = new Vector3(1.1f, 1.3f, 2.0f);
        shouldShrink = false;
    }

    private void OnMouseExit()
    {
        // gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        shouldShrink = true;

    }

    private void OnMouseUp()
    {
        print("mouse went up.");
        CameraAddOn cameraAddOn = FindObjectOfType<CameraAddOn>();
        cameraAddOn.StartMoving();
     
    }

    // Two or more object with colliders. One of them must have a rigidbody.
    //
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Wall>() != null) 
        {
            print("hit wall!");
            rb.isKinematic = true; // Freeze and zero out.
            rb.isKinematic = false; // Now we can effect it.
            // Go backward (Forward * negative number.).
            rb.AddForce(transform.forward * -10, ForceMode.Impulse);
            audiosource.clip = clonk;
            audiosource.Play();
        }
    }

    IEnumerator DelayForVariable()
    {
        yield return new WaitForSeconds(.03f);
        hasStarted = true;
    }

}
