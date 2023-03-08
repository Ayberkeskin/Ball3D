using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image whiteeffectimage;
    private int effectcontrol = 0;
    private bool radialshine;

    public Image FillRateImage;
    public GameObject Player;
    public GameObject FinishLine;
    public Animator LayoutAnimator;

    public Text coin_Text;

    //Butonlar
    public GameObject restartScreen;
    public GameObject settings_open;
    public GameObject settings_close;
    public GameObject sound_On;
    public GameObject sound_Off;
    public GameObject vibration_On;
    public GameObject vibration_Off;
    public GameObject iap;
    public GameObject information;
  
    //Oyun baslayinca kapanacak olanlar
    public GameObject intro_Hand;
    public GameObject taptomove_Text;
    public GameObject noAds;
    public GameObject shop_Button;
    public GameObject layoutBackground;



    //oyun sonu ekreaný
    public GameObject finishScreen;
    public GameObject blackBackGround;
    public GameObject complate;
    public GameObject radial_shine;
    public GameObject coin;
    public GameObject rewarded;
    public GameObject nothanks;

    private void Start()
    {
        SoundSaveControl();
        VibrationSaveControl();
        CoinTextUpdate();
    }

    public void Update()
    {
        Radialshine();
        FillRate();
    }

    public void Radialshine()
    {
        if (radialshine==true)
        {
            radial_shine.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 15f * Time.deltaTime));
        }
    }

    public void FillRate()
    {
        FillRateImage.fillAmount = ((Player.transform.position.z-4) / (FinishLine.transform.position.z));
    }

    //hepsini listeye alýp for ile de kapatýlabilir.
    public void FirstTouch()
    {
        intro_Hand.SetActive(false);
        taptomove_Text.SetActive(false);
        noAds.SetActive(false);
        shop_Button.SetActive(false);

        settings_open.SetActive(false);
        settings_close.SetActive(false);
        sound_On.SetActive(false);
        sound_Off.SetActive(false);
        vibration_On.SetActive(false);
        vibration_Off.SetActive(false);
        iap.SetActive(false);
        information.SetActive(false);
        layoutBackground.SetActive(false);
    }

    public void CoinTextUpdate()
    {
        coin_Text.text = PlayerPrefs.GetInt("moneyy").ToString();
    }

    public void RestartButtonActive()
    {
        restartScreen.SetActive(true);
    }

    public void RestartScreen()
    {
        Variables.firsttouch = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FinishScreen()
    {
        StartCoroutine(FinishLaunch());
    }

    public IEnumerator FinishLaunch()
    {
        Time.timeScale = 0.5f;
        radialshine = true;
        finishScreen.SetActive(true);
        blackBackGround.SetActive(true);
        yield return new WaitForSecondsRealtime(0.8f);
        complate.SetActive(true);
        yield return new WaitForSecondsRealtime(1.3f);
        radial_shine.SetActive(true);
        coin.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        rewarded.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        nothanks.SetActive(true);
    }


    // Save fonks.
    public void SoundSaveControl()
    {
        if (PlayerPrefs.GetInt("Vibration") ==2)
        {
            Sound_On();
        }
        else
        {
            Sound_Off();

        }
    }

    public void VibrationSaveControl()
    {
        if (PlayerPrefs.GetInt("Vibration") == 2)
        {
            Vibration_On();

        }
        else
        {
            Vibration_Off();
        }
    }



    //Buton's fonks.

    public void SettingsOpen()
    {
        settings_open.SetActive(false);
        settings_close.SetActive(true);
        LayoutAnimator.SetTrigger("Slide_in");
    }

    public void SettingsClose()
    {
        settings_open.SetActive(true);
        settings_close.SetActive(false);
        LayoutAnimator.SetTrigger("Slide_out");
    }

    public void Sound_On()
    {
        sound_On.SetActive(false);
        sound_Off.SetActive(true);
        AudioListener.volume = 0;
        PlayerPrefs.SetInt("Sound", 2);
        

    }

    public void Sound_Off()
    {
        sound_On.SetActive(true);
        sound_Off.SetActive(false);
        AudioListener.volume = 1;
        PlayerPrefs.SetInt("Sound", 1);

    }

    public void Vibration_On()
    {
        vibration_On.SetActive(false);
        vibration_Off.SetActive(true);
        PlayerPrefs.SetInt("Vibration", 2);

    }

    public void Vibration_Off()
    {
        vibration_On.SetActive(true);
        vibration_Off.SetActive(false);
        PlayerPrefs.SetInt("Vibration", 1);
    }




    public IEnumerator WhiteEffect()
    {
        whiteeffectimage.gameObject.SetActive(true);
        while (effectcontrol==0)
        {
            yield return new WaitForSeconds(0.001f);
            whiteeffectimage.color += new Color(0, 0, 0, 0.5f);
            if (whiteeffectimage.color==new Color(whiteeffectimage.color.r, whiteeffectimage.color.g, whiteeffectimage.color.b,1))
            {
                effectcontrol = 1;
            }
        }
        while (effectcontrol==1)
        {
            yield return new WaitForSeconds(0.001f);
            whiteeffectimage.color -= new Color(0, 0, 0, 0.5f);
            if (whiteeffectimage.color == new Color(whiteeffectimage.color.r, whiteeffectimage.color.g, whiteeffectimage.color.b, 0))
            {
                effectcontrol = 2;
            }
        }
        
    }
}
