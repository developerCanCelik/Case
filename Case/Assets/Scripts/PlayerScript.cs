using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] Camera;
    float forceMagnitude = 3;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "sphere")
        {
            Player.GetComponent<CapsuleCollider>().isTrigger = true;
            Player.GetComponent<Rigidbody>().isKinematic = false;
            StartCoroutine(Death());
        }
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(2f);
        Camera[0].SetActive(false);
        Camera[1].SetActive(true);
        Destroy(gameObject);
        yield return null;
    }
    float speed = 20f;
    private void FixedUpdate(){
        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;

        if(dir.sqrMagnitude > 1){
      
            dir.Normalize();
        }
        dir *= Time.deltaTime;

        this.transform.Translate(dir * speed);

        this.transform.GetComponent<Rigidbody>().mass = 100;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit) {
    Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if(rigidbody != null){
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();
            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }    
    }


}
