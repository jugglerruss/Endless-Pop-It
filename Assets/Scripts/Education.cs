using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Education : MonoBehaviour
{
    [SerializeField] private float  _speed;
    [SerializeField] private float  _stopPointX;
    [SerializeField] private float  _endPointX;
    [SerializeField] private GameObject _portal;
    private bool _portalDone => PlayerPrefs.GetInt("portalEducation", 0) == 1;
    [SerializeField] private GameObject _toch;
    private bool _tochDone => PlayerPrefs.GetInt("tochEducation", 0) == 1;
    [SerializeField] private GameObject _swipe;
    private bool _swipeDone => PlayerPrefs.GetInt("swipeEducation", 0) == 1;
    [SerializeField] private GameObject _hand;
    [SerializeField] private float _handStopY;
    [SerializeField] private bool _testEducation;
    void Start()
    {
        if (_testEducation)
        {
            StartCoroutine(MoveToch());
        }
        else
        {
            if (!_tochDone)
            {
                StartCoroutine(MoveToch());
            }
            else
            {
                if (!_portalDone)
                    StartCoroutine(MovePortal());
                else
                    if (!_swipeDone) StartCoroutine(MoveSwipe());
            }
        }
    }
    private IEnumerator MoveToch()
    {
        _toch.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(MoveForward(_toch, 30, _speed / 5));
        _toch.gameObject.SetActive(false);
        PlayerPrefs.SetInt("tochEducation", 1);
        StartCoroutine(MovePortal());
    }
    private IEnumerator MovePortal()
    {
        yield return StartCoroutine(MoveBack(_portal, _stopPointX, _speed * 3));
        yield return new WaitForSeconds(5);
        yield return StartCoroutine(MoveBack(_portal, _endPointX, _speed * 3));
        _portal.gameObject.SetActive(false);
        PlayerPrefs.SetInt("portalEducation", 1);
        StartCoroutine(MoveSwipe());
    }
    private IEnumerator MoveSwipe()
    {
        yield return StartCoroutine(MoveBack(_swipe, _stopPointX, _speed * 3));
        yield return new WaitForSeconds(2);
        _hand.gameObject.SetActive(true);
        _swipe.gameObject.SetActive(false);
        yield return StartCoroutine(MoveUp(_hand, _handStopY, _speed * 15));
        _hand.gameObject.SetActive(false);
        PlayerPrefs.SetInt("swipeEducation", 1);
    }
    private IEnumerator MoveBack(GameObject obj, float stopPoint, float speed)
    {
        while (obj.transform.position.x > stopPoint)
        {
            obj.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }
    }
    private IEnumerator MoveForward(GameObject obj, float endPoint, float speed)
    {
        while (obj.transform.position.x < endPoint)
        {
            obj.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }
    }
    private IEnumerator MoveUp(GameObject obj, float endPoint, float speed)
    {
        while (obj.transform.localPosition.y < endPoint)
        {
            obj.transform.localPosition += new Vector3(0, speed * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
    }
}
