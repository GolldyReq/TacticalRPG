using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    public static IEnumerator Idle2(Transform slime)
    {
        while (true)
        {
            float elapsedTime = 0;
            float duree = 1f;
            Vector3 scaleStart = slime.localScale;
            while (elapsedTime < duree)
            {
                float k = elapsedTime / duree;
                slime.localScale = Vector3.Lerp(scaleStart, new Vector3(1.1f, 1.1f, 1.1f), k);
                elapsedTime += Time.deltaTime;
                yield return null; // Attendre la prochaine frame 
            }
            slime.localScale = new Vector3(1.1f, 1.1f,1.1f);
             elapsedTime = 0;
            duree = 1f;
            scaleStart = slime.localScale;
            while (elapsedTime < duree)
            {
                float k = elapsedTime / duree;
                slime.localScale = Vector3.Lerp(scaleStart, new Vector3(1f, 1f, 1f), k);
                elapsedTime += Time.deltaTime;
                yield return null; // Attendre la prochaine frame 
            }
            slime.localScale = new Vector3(1f,1f,1f);
           
        }
    }

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

    public static IEnumerator Gauche(GameObject character)
    {
        character.GetComponent<Movement>().IsMoving = true;
        Animator animation = character.GetComponentInChildren<Animator>();
        Rigidbody rigidbody = character.GetComponentInChildren<Rigidbody>();
        rigidbody.useGravity = false;
        animation.Play("Jump");
        float elapsedTime = 0;
        float duree = .5f;
        Vector3 posStart = character.gameObject.transform.localPosition;
        while (elapsedTime < duree)
        {
            float k = elapsedTime / duree;
            character.gameObject.transform.localPosition = Vector3.Lerp(posStart, new Vector3(posStart.x - 5.0f, posStart.y, posStart.z), k);
            elapsedTime += Time.deltaTime;
            yield return null; // Attendre la prochaine frame 
        }
        character.gameObject.transform.localPosition = new Vector3(posStart.x - 5.0f, posStart.y, posStart.z);
        yield return new WaitForSeconds(.25f); // Attendre la prochaine frame 
        rigidbody.useGravity = true;
        character.GetComponent<Movement>().IsMoving = false;

    }

    public static IEnumerator Droite(GameObject character)
    {
        character.GetComponent<Movement>().IsMoving = true;
        Animator animation = character.GetComponentInChildren<Animator>();
        Rigidbody rigidbody = character.GetComponentInChildren<Rigidbody>();
        rigidbody.useGravity = false;
        animation.Play("Jump");
        float elapsedTime = 0;
        float duree = .5f;
        Vector3 posStart = character.gameObject.transform.localPosition;
        while (elapsedTime < duree)
        {
            float k = elapsedTime / duree;
            character.gameObject.transform.localPosition = Vector3.Lerp(posStart, new Vector3(posStart.x + 5.0f,posStart.y,posStart.z), k);
            elapsedTime += Time.deltaTime;
            yield return null; // Attendre la prochaine frame 
        }
        character.gameObject.transform.localPosition = new Vector3(posStart.x + 5.0f, posStart.y, posStart.z);
        yield return new WaitForSeconds(.25f); // Attendre la prochaine frame 
        rigidbody.useGravity = true;
        character.GetComponent<Movement>().IsMoving = false;

    }

    public static IEnumerator Avancer(GameObject character)
    {
        character.GetComponent<Movement>().IsMoving = true;
        Animator animation = character.GetComponentInChildren<Animator>();
        Rigidbody rigidbody = character.GetComponentInChildren<Rigidbody>();
        rigidbody.useGravity = false;
        animation.Play("Jump");
        float elapsedTime = 0;
        float duree = .5f;
        Vector3 posStart = character.gameObject.transform.localPosition;
        while (elapsedTime < duree)
        {
            float k = elapsedTime / duree;
            character.gameObject.transform.localPosition = Vector3.Lerp(posStart, new Vector3(posStart.x , posStart.y, posStart.z + 5.0f), k);
            elapsedTime += Time.deltaTime;
            yield return null; // Attendre la prochaine frame 
        }
        character.gameObject.transform.localPosition = new Vector3(posStart.x, posStart.y, posStart.z + 5.0f);
        yield return new WaitForSeconds(.25f); // Attendre la prochaine frame 
        rigidbody.useGravity = true;
        character.GetComponent<Movement>().IsMoving = false;

    }


    public static IEnumerator Reculer(GameObject character)
    {
        character.GetComponent<Movement>().IsMoving = true;
        Animator animation = character.GetComponentInChildren<Animator>();
        Rigidbody rigidbody = character.GetComponentInChildren<Rigidbody>();
        rigidbody.useGravity = false;
        animation.Play("Jump");
        float elapsedTime = 0;
        float duree = .5f;
        Vector3 posStart = character.gameObject.transform.localPosition;
        while (elapsedTime < duree)
        {
            float k = elapsedTime / duree;
            character.gameObject.transform.localPosition = Vector3.Lerp(posStart, new Vector3(posStart.x, posStart.y, posStart.z - 5.0f), k);
            elapsedTime += Time.deltaTime;
            yield return null; // Attendre la prochaine frame 
        }
        character.gameObject.transform.localPosition = new Vector3(posStart.x, posStart.y, posStart.z - 5.0f);
        yield return new WaitForSeconds(.25f); // Attendre la prochaine frame 
        rigidbody.useGravity = true;
        character.GetComponent<Movement>().IsMoving = false;

    }

}
