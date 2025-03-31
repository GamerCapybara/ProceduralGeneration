using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevelPhysics;

public class EnemyFollow : MonoBehaviour
{
    public Transform player; // The player to follow
    public float moveSpeed = 3f; // Movement speed of the enemy
    private float stopDistance = 5f; // Distance at which the enemy stops moving toward the player

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player is null) return;

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Stop moving if within the stop distance
        if (distanceToPlayer <= stopDistance) return;

        // Calculate direction towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move the enemy in the direction of the player
        transform.position +=  moveSpeed * Time.deltaTime * direction;
    }
}
