﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInformationUI : MonoBehaviour
{
    public GameObject   go_StageInformationUI;

    public Text         stageNumberText;
    public Text         bestScoreText;
    public Text[]       checkPointTexts;
    public Image[]      starImages;

    //현재 슬롯 및 스테이지를 null로 바꿔준다.
    public void StageSelectCancelButton()
    {
        go_StageInformationUI.SetActive( false );
        StageManager.instance.currentStageSlot = null;
        StageManager.instance.currentStage = null;
        StageManager.instance.currentBossStageSlot = null;
        StageManager.instance.currentBossStage = null;
    }

    //최대 점수, Star Image, CheckPountText 초기화
    public void ResetStageInformation()
    {
        bestScoreText.text = StageManager.instance.currentStageSlot.bestScore.ToString();

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = UIManager.instance.blankStarSprite;
            checkPointTexts[i].text = StageManager.instance.currentStageSlot.checkPoints[i].ToString();
            checkPointTexts[i].enabled = true;
        }
    }

    //클릭한 StageSlot의 정보에 맞게 설정
    public void SetStageInformation()
    {
        if(StageManager.instance.currentStageSlot != null)
        {
            if (StageManager.instance.currentStageSlot.starCount > 0)
            {
                for (int i = 0; i < StageManager.instance.currentStageSlot.starCount; i++)
                {
                    starImages[i].sprite = UIManager.instance.starSprite;
                }
            }

            if (StageManager.instance.currentStageSlot.starCount == 3)
            {
                for (int i = 0; i < checkPointTexts.Length; i++)
                {
                    checkPointTexts[i].enabled = false;
                }
            }
            else if (StageManager.instance.currentStageSlot.starCount == 2)
            {
                checkPointTexts[0].enabled = false;
                checkPointTexts[1].enabled = false;
            }
            else if (StageManager.instance.currentStageSlot.starCount == 1)
                checkPointTexts[0].enabled = false;

            stageNumberText.text = "STAGE " + (StageManager.instance.currentStageSlot.stageNumber + 1).ToString();
        }
        

        go_StageInformationUI.SetActive( true );
    }

    //누르면 게임 시작
    public void StageStartButton()
    {
        UIManager.instance.chapterSelectUI.go_CurrentChapterUI.SetActive( false );
        go_StageInformationUI.SetActive( false );

        //일반 스테이지면
        if (StageManager.instance.currentStage != null)
            StageManager.instance.currentStage = Instantiate( StageManager.instance.currentStageSlot.go_StagePrefab, new Vector3( 0, 0, 0 ), Quaternion.identity ).GetComponent<Stage>();

        //보스 스테이지면
        else
        {
            StageManager.instance.currentBossStage = Instantiate( StageManager.instance.currentBossStageSlot.go_BossStageNormal, new Vector3( 0, 0, 0 ), Quaternion.identity ).GetComponent<BossStage>();
        }
    }
}
