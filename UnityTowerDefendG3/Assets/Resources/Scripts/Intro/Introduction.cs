using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Introduction : MonoBehaviour
{
    public static Introduction Instance;

    public TextMeshProUGUI instructionText;
    public Image itemImage;
    public Button nextButton;
    public Button backButton;
    // public GameObject gameUI;
    public GameObject guideUI;
    public GameObject gameUI;



    public Sprite[] itemImages; // Mảng chứa hình ảnh vật phẩm
    public string[] itemTexts;     // Mảng chứa văn bản hướng dẫn
    public int step = 0;

    public bool isGuideActive = true;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        guideUI.SetActive(true);

        // Bắt đầu game logic (sinh ra quái vật, chạy game...)
        gameUI.SetActive(false);
        // Gán đối tượng RawImage
        //   FindAnyObjectByType<PauseMenu>().StopPlay();
        nextButton.onClick.AddListener(NextStep);
        backButton.onClick.AddListener(BackStep);

    }


    private void StartInstructionCoroutine()
    {
       
        StartCoroutine(ShowInstructions());

        UpdateItemImage(); // Cập nhật hình ảnh vật phẩm khi bắt đầu hướng dẫn

    }

    private void UpdateItemImage()
    {
        if (step >= 0 && step < itemImages.Length )
        {
            itemImage.sprite = itemImages[step];
        }
    }

    private void UpdateItemImageAndText()
    {
          Time.timeScale = 1;
      
            if (step >= 0 && step < itemImages.Length)
            {

              // Time.timeScale = 1;
                Debug.Log(itemImages[step]);
                //  Debug.Log(itemTexts[step]);

                itemImage.sprite = itemImages[step]; // Gán hình ảnh tương ứng với bước hướng dẫn hiện tại
                instructionText.text = itemTexts[step]; // Gán văn bản tương ứng với bước hướng dẫn hiện tại
                Debug.Log(itemImage);

            }
        
    }

    public void NextStep()
    {

        if (step < itemImages.Length - 1)
        {
            step++;
            UpdateItemImageAndText();
            StartInstructionCoroutine();
        }
        else
        {
            guideUI.SetActive(false);

            // Bắt đầu game logic (sinh ra quái vật, chạy game...)
            gameUI.SetActive(true);

        }

    }
    public void Exit()
    {
        guideUI.SetActive(false);
        gameUI.SetActive(true);
    }

    private void BackStep()
    {

        if (step > 0)
        {
            step--;
            UpdateItemImageAndText();
            StartInstructionCoroutine();
        }

    }


    private IEnumerator ShowTextLetterByLetter(string fullText)
    {
        instructionText.text = ""; // Xóa nội dung hiện tại

        for (int i = 0; i < fullText.Length; i++)
        {
            instructionText.text += fullText[i]; // Thêm ký tự vào dòng văn bản
            yield return new WaitForSeconds(0.05f); // Chờ một khoảng thời gian trước khi hiển thị ký tự tiếp theo
        }
    }

    private IEnumerator ShowInstructions()
    {
        string fullText = itemTexts[step]; // Lấy toàn bộ văn bản từ bước hướng dẫn
        yield return ShowTextLetterByLetter(fullText); // Sử dụng Coroutine để hiển thị từng ký tự
        if (isGuideActive) // Kiểm tra trạng thái trước khi cập nhật hình ảnh vật phẩm
        {
            UpdateItemImage(); // Cập nhật hình ảnh vật phẩm sau khi hiển thị văn bản
        }
       // UpdateItemImage(); // Cập nhật hình ảnh vật phẩm sau khi hiển thị văn bản
    }
    public void Pause()
    {
        guideUI.SetActive(true);
        gameUI.SetActive(false);

    }
}
