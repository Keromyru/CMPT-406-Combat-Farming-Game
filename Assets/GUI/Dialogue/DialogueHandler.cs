using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    [Header("Text Options")]
    public Text nameText;
    public Text dialogueText;

    public Animator animator;
    public float typingSpeed;

    private Queue<string> sentences;
    [Header("References")]
    [SerializeField] DayNightCycle daynight;
    [SerializeField] GameObject TutorialButton;
    private PlayerController myPlayer; //Using this to stop the player from having unlimited time.

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        myPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }


    void OnEnable() {
        DayNightCycle.isNowNight += newNight;
    }

    void OnDisable() {
        DayNightCycle.isNowNight -= newNight;
    } 

    public void newNight(){
        if (TutorialButton != null){ //Disable The Tutorial at night... because it fundamentally doesn't work
            TutorialButton.SetActive(false); 
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        daynight.pause();
        animator.SetBool("IsOpen", true);
		//animator.SetTrigger("Enter");
		
        myPlayer.setCanMove(false);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //dialogueText.text = sentence;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
		//animator.SetTrigger("Exit");
        daynight.resume();
        myPlayer.setCanMove(true);
    }

    public void SkipDialogue()
    {
        sentences.Clear();
        EndDialogue();
    }
}
