using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [Header("Title / Credits Screen")]
    public GameObject title; // tile image object. used also for credits
    bool _isTitleVisible = true; // determs if title is visble
    public Sprite creditsSprite; // credits image used for title
    public GameObject itemsObject; // menu options
    public GameObject creditsObject; // credits text object
    [Multiline]
    public List<string> credits; // Stores credits texts - for original creators and unity port

    [Header("Navigation")]
    public GameObject selectedItem; // currently selected menu option
    public GameObject indicator; // indicator object
    public int selected; // currently selected menu option as integer
    bool _inputLock; // used so if player holds up or down key the options don't change super quickly

    [Header("Sky Data")]
    public GameObject sky; // sky object
    public float skySpeed; // current sky scrolling speed
    public float skyStartingPosition;

    public static MainMenu instance;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        creditsObject.SetActive(false);
        itemsObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        #region Input
        if (Input.GetButtonDown("Fire1"))
        {
            // if credits or title is visbile - turns it off
            if (_isTitleVisible)
            {
                title.SetActive(false);
                itemsObject.SetActive(true);
                creditsObject.SetActive(false);
                _isTitleVisible = false;
                // disabled credits routine so the credits text doesn't change while it's disabled
                if (creditsRoutine != null)
                {
                    StopCoroutine(creditsRoutine);
                    creditsRoutine = null;
                }
            }
            // else it launches the selected menu option function
            else
            {
                selectedItem.GetComponent<MenuItem>().Open();
            }
        }

        // Ignore all other input if the title/credits is visible
        if (_isTitleVisible) return;

        if (!_inputLock)
        {
            if (Input.GetAxisRaw("Vertical") < 0)
                Navigate(Move.Down);
            else if (Input.GetAxisRaw("Vertical") > 0)
                Navigate(Move.Up);
        }

        if (Input.GetAxisRaw("Vertical") == 0) _inputLock = false;
        #endregion

        // Scrolls the sky background
        sky.transform.position = new Vector2(sky.transform.position.x - skySpeed, sky.transform.position.y);
        if (sky.transform.position.x <= skyStartingPosition * -1)
            sky.transform.position = new Vector2(skyStartingPosition, sky.transform.position.y);
    }

    enum Move { Up, Down };
    void Navigate(Move move)
    {
        _inputLock = true;
        switch (move)
        {
            case Move.Down:
                selected++;
                break;
            case Move.Up:
                selected--;
                break;
        }

        // Checks if the value isn't higher than options in menu
        selected = selected > 2 ? selected = 0 :
        selected < 0 ? selected = 2 : selected;

        selectedItem = GameObject.Find("i" + selected);
        indicator.transform.position = new Vector2(indicator.transform.position.x, selectedItem.transform.position.y);
    }

    public void Credits()
    {
        title.SetActive(true);
        title.GetComponent<SpriteRenderer>().sprite = creditsSprite;
        itemsObject.SetActive(false);
        creditsObject.SetActive(true);
        creditsObject.GetComponent<Text>().text = credits[0];
        creditsRoutine = CreditsRoutine();
        StartCoroutine(creditsRoutine);
        _isTitleVisible = true;
    }

    private IEnumerator creditsRoutine;
    public IEnumerator CreditsRoutine()
    {
        Text creditsText = creditsObject.GetComponent<Text>();

        while (true)
        {
            yield return new WaitForSeconds(4);
            creditsText.text = credits[1];
            yield return new WaitForSeconds(4);
            creditsText.text = credits[0];
        }
    }
}
