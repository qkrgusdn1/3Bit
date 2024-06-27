using System.Collections;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public GameObject text;
    public float range;
    public bool enterd;
    public bool watched;
    public float findAngle;
    public float angle;

    public LayerMask runnerLayer;

    Collider[] runnersInRange;
    public virtual void Start()
    {
        text.SetActive(false);
        StartCoroutine(CoUpdate());
    }


    public void Range()
    {
        runnersInRange = Physics.OverlapSphere(transform.position, range, runnerLayer);

        if (runnersInRange.Length <= 0)
        {
            enterd = false;
        }
        else
        {
            enterd = true;
        }


    }

    public virtual IEnumerator CoUpdate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            Range();
            CheckLookAt();

            if (enterd && watched)
            {
                text.SetActive(true);
            }
            else
            {
                text.SetActive(false);
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void CheckLookAt()
    {
        if (runnersInRange == null || runnersInRange.Length <= 0)
        {
            watched = false;
            return;
        }

        for (int i = 0; i < runnersInRange.Length; i++)
        {
            Vector3 dir = transform.position - runnersInRange[i].transform.position;
            dir.y = 0;
            dir = dir.normalized;
            angle = Vector3.Angle(runnersInRange[i].GetComponent<Player>().bodyTr.forward, dir);

            
            watched = angle <= findAngle;
            if(watched)
                break;
        }
    }
}
