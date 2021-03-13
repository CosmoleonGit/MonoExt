using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoExt
{
    public static class Collision
    {
        #region Rectangle Collision
        public static bool RayVsRect(Vector2 rayOrigin,
                                     Vector2 rayDir,
                                     RectangleF target,
                                     out Vector2 contactPoint,
                                     out Vector2 contactNormal,
                                     out float tHitNear)
        {
            contactNormal = Vector2.Zero;
            contactPoint = Vector2.Zero;
            tHitNear = 0f;

            // Cache division
            Vector2 invDir = Vector2.One / rayDir;

            // Calculate intersections with rectangle bounding axes
            Vector2 tNear = (target.Location - rayOrigin) * invDir;
            Vector2 tFar = (target.Location + target.Size - rayOrigin) * invDir;

            if (float.IsNaN(tNear.X) || float.IsNaN(tNear.Y)) return false;
            if (float.IsNaN(tFar.X) || float.IsNaN(tFar.Y)) return false;

            // Sort distances
            if (tNear.X > tFar.X)
            {
                float temp = tNear.X;
                tNear.X = tFar.X;
                tFar.X = temp;
            }

            if (tNear.Y > tFar.Y)
            {
                float temp = tNear.Y;
                tNear.Y = tFar.Y;
                tFar.Y = temp;
            }

            // Early rejection
            if (tNear.X > tFar.Y || tNear.Y > tFar.X) return false;

            // Closest 'time' will be the first contact
            tHitNear = Math.Max(tNear.X, tNear.Y);

            // Furthest 'time' is contact on opposite side of target
            float tHitFar = Math.Min(tFar.X, tFar.Y);

            // Reject if ray direction is pointing away from object
            if (tHitFar < 0) return false;

            // Contact point of collision from parametric line equation
            contactPoint = rayOrigin + tHitNear * rayDir;

            if (tNear.X > tNear.Y)
            {
                if (invDir.X < 0)
                    contactNormal = new Vector2(1, 0);
                else
                    contactNormal = new Vector2(-1, 0);
            } else if (tNear.X < tNear.Y) {
                if (invDir.Y < 0)
                    contactNormal = new Vector2(0, 1);
                else
                    contactNormal = new Vector2(0, -1);
            }

            return true;
        }

        public static bool DynamicRectVsRect(RectangleF rDynamic, 
                                             Vector2 velocity,
                                             RectangleF rStatic,
                                             out Vector2 contactPoint,
                                             out Vector2 contactNormal,
                                             out float contactTime)
        {
            contactPoint = Vector2.Zero;
            contactNormal = Vector2.Zero;
            contactTime = 0f;

            // Check if dynamic rectangle is actually moving - we assume rectangles are NOT in collision to start
            if (velocity == Vector2.Zero)
                return false;
            
            // Expand target rectangle by source dimensions
            rStatic.Inflate(rDynamic.Size.X / 2, rDynamic.Size.Y / 2);

            if (RayVsRect(rDynamic.Location + rDynamic.Size / 2, velocity, rStatic, out contactPoint, out contactNormal, out contactTime))
                return (contactTime >= 0f && contactTime < 1f);
            else
                return false;
        }

        public static bool ResolveDynamicRectVsRect(RectangleF rDynamic,
                                                    ref Vector2 velocity,
                                                    RectangleF rStatic)
        {

            if (DynamicRectVsRect(rDynamic, velocity, rStatic, out Vector2 contactPoint, out Vector2 contactNormal, out float contactTime))
            {
                velocity += contactNormal * new Vector2(Math.Abs(velocity.X), Math.Abs(velocity.Y)) * (1f - contactTime);
                return true;
            }

            return false;
        }
        #endregion

        #region
        public static bool CircleVsRect(Vector2 circlePos, float radius, Rectangle rectangle)
        {
            Vector2 v = new Vector2(MathHelper.Clamp(circlePos.X, rectangle.Left, rectangle.Right),
                                    MathHelper.Clamp(circlePos.Y, rectangle.Top, rectangle.Bottom));

            Vector2 direction = circlePos - v;
            float distanceSquared = direction.LengthSquared();

            return ((distanceSquared > 0) && (distanceSquared < radius * radius));
        }

        #endregion
    }
}
