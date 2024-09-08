using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PhysicsTests
{
    [Test]
    public void NoDistanceCollisionCheckReturnsFalse()
    {
        GameObject gameObject = new GameObject();
        BoxCollider2D coll = gameObject.AddComponent<BoxCollider2D>();
        coll.size = new Vector2(1, 1);

       //  bool foundCol = CollisionChecks.CheckDirectionForCollision(coll, Vector2.up, 0, new ContactFilter2D());
        // Assert.IsFalse(foundCol);
    }

    [Test]
    public void CollisionCheckHitsRigidBodyIfGreaterDistanceThanBounds()
    {
        // Make first object for casting collider
        GameObject gameObject = new GameObject();
        BoxCollider2D coll = gameObject.AddComponent<BoxCollider2D>();
        coll.size = new Vector2(1, 1);

        // Make second body for collision
        GameObject gameObject2 = new GameObject();
        Rigidbody2D body2 = gameObject2.AddComponent<Rigidbody2D>();
        body2.bodyType = RigidbodyType2D.Dynamic;
        BoxCollider2D coll2 = gameObject.AddComponent<BoxCollider2D>();
        coll.size = new Vector2(1, 1);

        // bool foundCol = CollisionChecks.CheckDirectionForCollision(coll, Vector2.up, 5, new ContactFilter2D());


        // Assert.IsTrue(foundCol);
    }
}
