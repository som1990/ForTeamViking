/*
 Description: Run this script on a sphere collider set as a child of the AI. This quick prototype calculates 
              distance of all units close to a player/enemy. In order for onTrigger to work, you need to have 
              rigid bodies with isKinematic set to "On" and useGravity set to "Off". This way the rigid bodies will
              affect the triggers but won't get affected by external forces and themselves hence avoiding collision issues.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour {

    public string tag2detect = "Enemy";
    private List<Collider> enemyColliderList;
    public float period = 2.0f;
    private float nextActionTime = 0.0f;

	// Awake is required over Start otherwise you get initializer errors.
	void Awake () {
        enemyColliderList = new List<Collider>();
	}
	
	// FixedUpdate is called every simulation frame
	void FixedUpdate () {
        //Runs every period seconds
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            //Debug.Log("Next Action Time: " + nextActionTime);
            Collider closestCollider = null;
            float min = 10000000000;
            if (enemyColliderList != null)
            {
                foreach (Collider col in enemyColliderList)//Run through the colliderList
                {
                    float dist = Vector3.Distance(transform.position, col.transform.position);
                    Renderer rend = col.gameObject.GetComponentInParent<Renderer>(); // used to color the object
                    //Color the objects green
                    rend.material.SetColor("_Color", Color.green);
                    //Calculating the minimum distance
                    if (dist < min)
                    {
                        min = dist;
                        closestCollider = col;
                    }
                }
                Debug.Log("Minimum Distance = " + min);
                if (closestCollider != null)
                {
                    Debug.Log("Closest Object : " + closestCollider.name);
                    //Color the closest object Blue.
                    closestCollider.gameObject.GetComponentInParent<Renderer>().material.SetColor("_Color", Color.blue);
                }
            }
        }
    }

    //Called each time an object triggers this 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tag2detect)
        {
            Debug.Log(tag2detect + " Detected");
            if(!enemyColliderList.Contains(other))
                enemyColliderList.Add(other);
            foreach (Collider col in enemyColliderList)
                Debug.Log(col.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == tag2detect)
        {
            Debug.Log(tag2detect + "is no longer visible");
            if (enemyColliderList.Contains(other))
            {
                other.gameObject.GetComponentInParent<Renderer>().material.SetColor("_Color", Color.green);
                enemyColliderList.Remove(other);
            }
            foreach (Collider col in enemyColliderList)
                Debug.Log(col.name);

        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.tag == tag2detect)
        {
        //Run every "period" seconds
            if (Time.time > nextActionTime) 
            {
                nextActionTime += period;
                //Debug.Log("Next Action Time: " + nextActionTime);
                Collider closestCollider = null;
                float min = 1000000000;
                if (enemyColliderList != null)
                {
                    foreach (Collider col in enemyColliderList)//Run through the colliderList
                    {
                        float dist = Vector3.Distance(transform.position, col.transform.position);
                        Renderer rend = col.gameObject.GetComponentInParent<Renderer>(); // used to color the object
                        //Color the objects green
                        rend.material.SetColor("_Color", Color.green);
                        //Calculating the minimum distance
                        if (dist < min)
                        {
                            min = dist;
                            closestCollider = col;  
                        }                      
                    }
                    Debug.Log("Minimum Distance = " + min);
                    Debug.Log("Closest Object : " + closestCollider.name);
                    //Color the closest object Blue.
                    closestCollider.gameObject.GetComponentInParent<Renderer>().material.SetColor("_Color", Color.blue);
                }
            }
            
        }
    }
*/
}
