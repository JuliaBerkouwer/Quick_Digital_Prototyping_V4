using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotationCenter : MonoBehaviour
{
    public GameObject friendObject;
    public int amountOfFriends;
    public int amountOfTiers;
    public float offset;
    public float moveSpeed;
    public float reverseSpeed;
    private Vector3 eulerAngle;
    private GameObject[] friends;

    private void Start()
    {
        friends = new GameObject[amountOfFriends * amountOfTiers];
        CreateFriend();
    }

    private void Update()
    {
        float leftTrigger = Input.GetAxis("LeftTrigger");
        float rightTrigger = Input.GetAxis("RightTrigger");

        if (leftTrigger >= 0.5f)
        {
            eulerAngle = new Vector3(0, 0, moveSpeed);
        }
        else if (rightTrigger >= 0.5f)
        {
            eulerAngle = new Vector3(0, 0, reverseSpeed);
        }
        else
        {
            eulerAngle = Vector3.zero;
        }

        Quaternion target = Quaternion.Euler(eulerAngle);
        transform.Rotate(eulerAngle);
    }

    public void CreateFriend()
    {
        int friendIndex = GetIndex();
        if (friendIndex == -1) { return; }

        int tier = Mathf.RoundToInt(friendIndex / amountOfFriends);
        Vector3 position = new Vector3(0, tier + offset, 0);

        int t = friendIndex % amountOfFriends;
        int zRotation = ((360 / amountOfFriends) * t);
        int offsetRotation = ((360 / amountOfFriends) / 2) * tier;

        Vector3 rotation = new Vector3(0, 0, zRotation + offsetRotation);

        GameObject currentFriend = Instantiate(friendObject, transform.position, Quaternion.identity);
        currentFriend.transform.SetParent(transform);
        currentFriend.transform.GetChild(0).transform.position = transform.TransformPoint(position);

        currentFriend.transform.eulerAngles = rotation;

        friends[friendIndex] = currentFriend;

        FriendPositions friendPosition = new FriendPositions();
        friendPosition.tierIndex = friendIndex;
        friendPosition.tier = tier;
        friendPosition.index = friendIndex;

        Friend f = currentFriend.AddComponent<Friend>();
        f.fp = friendPosition;
    }

    public void RemoveFriend(FriendPositions friendPosition)
    {
        friends[friendPosition.index] = null;
    }

    private int GetIndex()
    {
        for (int i = 0; i < friends.Length; i++)
        {
            if (friends[i] == default)
            {
                return i;
            }
        }
        return -1;
    }
}

public struct FriendPositions
{
    public int index;
    public int tierIndex;
    public int tier;
}