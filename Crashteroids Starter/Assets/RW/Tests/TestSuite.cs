using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Net;

public class TestSuite
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }

    // 1
    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        // 3
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        // 4
        float initialYPos = asteroid.transform.position.y;
        // 5
        yield return new WaitForSeconds(0.1f);
        // 6
        Assert.Less(asteroid.transform.position.y, initialYPos);
        
    }

    [UnityTest]
    public IEnumerator GameOverOccursOnAsteroidCollision()
    {

        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        //1
        asteroid.transform.position = game.GetShip().transform.position;
        //2
        yield return new WaitForSeconds(0.1f);

        //3
        Assert.True(game.isGameOver);

    }

    [UnityTest]
    public IEnumerator NewGameRestartsGame()
    {
        //1
        game.isGameOver = true;
        game.NewGame();
        //2
        Assert.False(game.isGameOver);
        yield return null;
    }

    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        // 1
        GameObject laser = game.GetShip().SpawnLaser();
        // 2
        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);
        // 3
        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator DestroyedAsteroidRaisesScore()
    {
        // 1
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        Assert.AreEqual(game.score, 1);
    }

    [UnityTest]
    public IEnumerator NewGameResetsScrore()
    {
        game.isGameOver = true;
        game.NewGame();

        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(game.score, 0);
    }

    [UnityTest]
    public IEnumerator CheckRightMove()
    {
        Ship ship = game.GetShip();

        float initialxPos = ship.transform.position.x;

        ship.MoveRight();

        float newxPos = ship.transform.position.x;

        yield return new WaitForSeconds(0.1f);
        Assert.Greater(newxPos, initialxPos);
    }

    [UnityTest]
    public IEnumerator CheckLeftMove()
    {
        Ship ship = game.GetShip();

        float initialxPos = ship.transform.position.x;

        ship.MoveLeft();

        float newxPos = ship.transform.position.x;

        yield return new WaitForSeconds(0.1f);
        Assert.Less(newxPos, initialxPos);
    }

    [UnityTest]
    public IEnumerator AsteroidSpawnsPowerUp()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.GetComponent<Asteroid>().SetPowerUpChance(1.0f);

        //destroy asteroid
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;

        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(!GameObject.Find("powerUp"));
    }

    [UnityTest]
    public IEnumerator PlayerObtainsShield()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.GetComponent<Asteroid>().SetPowerUpChance(1.0f);
        Ship ship = game.GetShip();

        //spawn powerup by destroying asteroid
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;

        //collide with player
        ship.transform.position = Vector3.zero;

        yield return new WaitForSeconds(0.5f);
        Assert.IsTrue(ship.shielded);
    }


    [UnityTest]
    public IEnumerator ShieldStopsGameOver()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.GetComponent<Asteroid>().SetPowerUpChance(1.0f);
        Ship ship = game.GetShip();

        //spawn powerup by destroying asteroid
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;

        //collide with player
        ship.transform.position = Vector3.zero;

        //collide shielded ship with asteroid
        GameObject asteroid2 = game.GetSpawner().SpawnAsteroid();
        asteroid2.transform.position = Vector3.zero;

        yield return new WaitForSeconds(0.5f);
        Assert.IsFalse(ship.shielded);
    }

}