using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to choose a book from the respective bookshelf section in the bookshelf window
public class BookSelectionController : MonoBehaviour
{
    [SerializeField]
    private BookshelfController controller;

    [SerializeField]
    private GameObject redBook;
    [SerializeField]
    private GameObject blueBook;
    [SerializeField]
    private GameObject greenBook;
    [SerializeField]
    private GameObject purpleBook;
    [SerializeField]
    private GameObject orangeBook;

    private GameObject redBookHighlight;
    private GameObject blueBookHighlight;
    private GameObject greenBookHighlight;
    private GameObject purpleBookHighlight;
    private GameObject orangeBookHighlight;

   private List<GameObject> bookList = new List<GameObject>();
    private List<GameObject> highlights = new List<GameObject>();

    private int currentBook = 0;
    private int takenBookIndex = -1;
    public int totalbooks; 
    

    private void Start()
    {
        bookList.Add(redBook);
        bookList.Add(blueBook);
        bookList.Add(greenBook);
        bookList.Add(purpleBook);
        bookList.Add(orangeBook);

        highlights.Add(redBookHighlight);
        highlights.Add(blueBookHighlight);
        highlights.Add(greenBookHighlight);
        highlights.Add(purpleBookHighlight);
        highlights.Add(orangeBookHighlight);

        SelectBook();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) // Platzhalter, lieber pfeiltasten?
        {
            if (currentBook == bookList.Count - 1)
            {
                currentBook = 0;
                SelectBook();
            }
            else
            {
                currentBook++;
                SelectBook();
            }
        }
        if (Input.GetKeyDown(KeyCode.J)) // Platzhalter, lieber pfeiltasten?
        {
            if (currentBook == 0)
            {
                currentBook = bookList.Count - 1;
                SelectBook();
            }
            else
            {
                currentBook--;
                SelectBook();
            }
        }
        if (Input.GetKeyDown(KeyCode.K)) // Platzhalter
        {
            TakeSelectedBook();
        }
        /*
        if (controller.GetComponent<BookshelfUI>().IsOpen)
        {
            ResetAll();
            SetAll();
        }*/
    }

    private void SelectBook()
    {
        UnselectAllHighlights();
        highlights[currentBook].SetActive(true);
    }

    private void TakeSelectedBook()
    {
        bookList[currentBook].SetActive(false);
        bookList.RemoveAt(currentBook);
        highlights.RemoveAt(currentBook);
        takenBookIndex = currentBook;
        currentBook = 0;
        controller.GetComponent<BookshelfUI>().OpenAndCloseBookshelfWindow();
    }

    private void UnselectAllHighlights()
    {
        for (int i = 0; i < bookList.Count; i++)
        {
            highlights[i].SetActive(false);
        }
    }

    private void ResetAll()
    {
        for (int i = 0; i < bookList.Count; i++)
        {
            bookList[i].SetActive(true);
            takenBookIndex = -1;
        }
    }

    private void SetAll()
    {
        bool[] visibleBooks = controller.VisibleBooks;
        for (int i = 0; i < visibleBooks.Length; i++)
        {
            bookList[i].SetActive(visibleBooks[i]);
            if (visibleBooks[i] == false)
            {
                bookList.RemoveAt(i);
            }
        }
    }

    public int TakenBookIndex
    {
        get { return takenBookIndex; }
    }
}
