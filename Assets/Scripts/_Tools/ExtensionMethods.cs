using UnityEngine;
using System.Collections;

/*
 * This class implement all the function allowing to 
 * describe behaviors with steering implementation
 */
public static class ExtensionMethods {

    /*
     * Seek behavior
     */
    public static Vector2 seek(this Transform trans, ArrayList behaviors, Vector2 targetPosition)
    {
        Vector2 steering = ((SeekBehavior)behaviors[0]).computeSeekSteering(targetPosition);
        return ((SeekBehavior)behaviors[0]).computeNewPosition(steering - ((SeekBehavior)behaviors[0]).computeSteeringSeparationForce());
    }

    /*
     * Flee behavior
     */
    public static Vector2 flee(this Transform trans, ArrayList behaviors, Vector2 targetPosition)
    {
        Vector2 steering = ((FleeBehavior)behaviors[1]).computeFleeSteering(targetPosition);
        return ((FleeBehavior)behaviors[1]).computeNewPosition(steering - ((FleeBehavior)behaviors[1]).computeSteeringSeparationForce());
    }

    /*
     * Pursuit behavior
     */
    public static Vector2 pursuit(this Transform trans, ArrayList behaviors, Vector2 targetPosition, Vector2 targetVelocity)
    {
        Vector2 steering = ((PursuitBehavior)behaviors[2]).computePursuitSteering(targetPosition, targetVelocity);
        return ((PursuitBehavior)behaviors[2]).computeNewPosition(steering - ((PursuitBehavior)behaviors[2]).computeSteeringSeparationForce());
    }

    /*
     * Evasion behavior
     */
    public static Vector2 evasion(this Transform trans, ArrayList behaviors, Vector2 targetPosition, Vector2 targetVelocity)
    {
        Vector2 steering = ((EvasionBehavior)behaviors[3]).computeEvasionSteering(targetPosition, targetVelocity);
        return ((EvasionBehavior)behaviors[3]).computeNewPosition(steering - ((EvasionBehavior)behaviors[3]).computeSteeringSeparationForce());
    }

    /*
     * Wait behavior
     */
    public static Vector2 wait(this Transform trans, ArrayList behaviors, Vector2 targetPosition, float sizeRadius, float timeBeforeChangePos)
    {
        Vector2 steering = ((WaitBehavior)behaviors[4]).computeWaitSteering(targetPosition, sizeRadius, timeBeforeChangePos);
        return ((WaitBehavior)behaviors[4]).computeNewPosition(steering - ((WaitBehavior)behaviors[4]).computeSteeringSeparationForce());
    }
}
