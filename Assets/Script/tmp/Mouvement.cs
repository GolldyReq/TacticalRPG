using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
 
    public static IEnumerator Idle(GameObject character)
    {
        Animator animation = character.GetComponentInChildren<Animator>();
        while (true)
        {
            animation.Play("Idle");
            yield return null; // Attendre la prochaine frame

        }
    }

    public static IEnumerator Jump(GameObject character)
    {
        Animator animation = character.GetComponentInChildren<Animator>();
        Rigidbody rigidbody = character.GetComponentInChildren<Rigidbody>();
        rigidbody.useGravity = false;
        animation.Play("Jump");
        yield return new WaitForSeconds(1f); // Attendre la prochaine frame 
        rigidbody.useGravity = true;
    }
   
    public static IEnumerator GoTO(GameObject character, List<Tile> path)
    {
        GameController.Instance.ChangePhase(GameController.PHASEACTION.Mouvement);
        character.GetComponent<Personnage>().IsMoving = true;
        character.GetComponent<Personnage>().currentTile.empty = true;
        Animator animation = character.GetComponentInChildren<Animator>();
        Rigidbody rigidbody = character.GetComponentInChildren<Rigidbody>();
        foreach(Tile t in path)
        {
            Vector3 targetPosition = new Vector3( t.x * 5 , t.y + 1f , t.z * 5 );
            if(rigidbody!=null)
                rigidbody.useGravity = false;
            if(animation!=null)
                animation.Play("Move");
            float elapsedTime = 0;
            float duree = .5f;
            Vector3 posStart = character.gameObject.transform.localPosition;
            while (elapsedTime < duree)
            {
                float k = elapsedTime / duree;
                character.gameObject.transform.localPosition = Vector3.Lerp(posStart, targetPosition, k);
                elapsedTime += Time.deltaTime;
                yield return null; // Attendre la prochaine frame 
            }
            character.gameObject.transform.localPosition = targetPosition;
            if(rigidbody!=null)
                rigidbody.useGravity = true;
            yield return new WaitForSeconds(.15f); // Attendre la prochaine frame 
        }
        character.GetComponent<Personnage>().IsMoving = false;
        character.GetComponent<Personnage>().currentTile = character.GetComponent<Personnage>().getTile();
        GameController.Instance.ChangePhase(GameController.PHASEACTION.ChoixAction);
        GameController.Instance.hasMove = true;
        //GameController.m_Instance.NextPlayer();
    }
}
