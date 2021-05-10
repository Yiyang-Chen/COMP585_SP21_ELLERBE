using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The codes used to detect which habitate palyers are in
/// </summary>
public class InsideARegion : MonoBehaviour
{
    public Vector2[] points = new Vector2[8];
    public int[][] polygons= new int[4][] { new int[] { 4,7,15,10,6,11,5,9 }, new int[] { 4,9,5,11,13,12,8,1,0 }, new int[] {3,2,8,12,14 }, new int[] { 15, 3, 14, 12,13,11,6,10 } };
    const float eps = 0.0001f;
    public int inRegion = -2;

    //Check which region the point is in, return -1 if outside all the regions 
    public int checkRegion(Vector2 P)
    {
        for (int i = 0; i < polygons.Length; i++)
        {
            if (inPolygon(P, getPoints(polygons[i])))
            {
                return i;
            }
        }
        return -1;
    }

    //Functions below are to serve for the algorithm to determine which region a point is inside

    //compare two floats under certain eps
    private int fcmp(float x)
    {
        if (Mathf.Abs(x) < eps) return 0;
        else
            return x < 0 ? -1 : 1;
    }
    //If Q is on P1P2
    private bool onSegment(Vector2 P1, Vector2 P2, Vector2 Q)
    {
        //Q on the line P1P2 && Q in the region P1P2
        return fcmp((P1 - Q).x * (P2 - Q).y - (P1 - Q).y * (P2 - Q).x) == 0 && fcmp((P1 - Q).x * (P2 - Q).x + (P1 - Q).y * (P2 - Q).y) <= 0;
    }
    //If P is in ploygon
    private bool inPolygon(Vector2 P , Vector2[] polygon)
    {
        bool flag = false; //calculate the time
        Vector2 P1, P2; //One edge of polygon
        for (int i = 0, j = polygon.Length-1; i <= polygon.Length-1; j = i++)
        {
            //polygon[] are the corners of polygon
            P1 = polygon[i];
            P2 = polygon[j];
            //(i , j ) Comparing point P with edge: P1 -> P2
            if (onSegment(P1, P2, P))//point on one edge of polygon
            {
                //Point P on the edge: P1  ->  P2
                return true; 
            }
            //min(P1.y,P2.y)<P.y<=max(P1.y,P2.y) && on left hand side of the vector
            if ((fcmp(P1.y - P.y) > 0 != fcmp(P2.y - P.y) > 0) && fcmp(P.x - (P.y - P1.y) * (P1.x - P2.x) / (P1.y - P2.y) - P1.x) < 0)
            {
                //Point y of P intersect with edge and P on the lefthand side of edge: P1 ->  P2
                flag = !flag;
            }
                
        }
        return flag;
    }

    //Get all the points
    private Vector2[] getPoints(int[] polygon)
    {
        
        Vector2[] polyPoints = new Vector2[polygon.Length];
        for (int i=0;i<polygon.Length;i++)
        {
            polyPoints[i] = points[polygon[i]];
        }
        return polyPoints;
    }
}
