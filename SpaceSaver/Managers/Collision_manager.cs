namespace SpaceSaver
{
    public static class Collision_manager
    {
        public static bool CheckCollision(PrimitiveComponent spr1, PrimitiveComponent spr2)
        {
            return (spr1.Velo.X > 0 && IsTouchingLeft(spr1, spr2)) || (spr1.Velo.X < 0 && IsTouchingRight(spr1, spr2)) ||
                    (spr1.Velo.Y > 0 && IsTouchingTop(spr1, spr2)) || (spr1.Velo.Y < 0 && IsTouchingBottom(spr1, spr2));
        }

        public static bool Collision_X(PrimitiveComponent spr1, PrimitiveComponent spr2)
        {
            return (spr1.Velo.X > 0 && IsTouchingLeft(spr1, spr2)) || (spr1.Velo.X < 0 && IsTouchingRight(spr1, spr2));
        }

        public static bool Collision_Y(PrimitiveComponent spr1, PrimitiveComponent spr2)
        {
            return (spr1.Velo.Y > 0 && IsTouchingTop(spr1, spr2)) || (spr1.Velo.Y < 0 && IsTouchingBottom(spr1, spr2));
        }

        private static bool IsTouchingLeft(PrimitiveComponent spr1, PrimitiveComponent spr2)
        {
            return (spr1.Properties.Right + spr1.Velo.X > spr2.Properties.Left) &&
              (spr1.Properties.Left < spr2.Properties.Left) &&
              (spr1.Properties.Bottom > spr2.Properties.Top) &&
              (spr1.Properties.Top < spr2.Properties.Bottom);
        }

        private static bool IsTouchingRight(PrimitiveComponent spr1, PrimitiveComponent spr2)
        {
            return (spr1.Properties.Left + spr1.Velo.X < spr2.Properties.Right) &&
             ( spr1.Properties.Right > spr2.Properties.Right) &&
             ( spr1.Properties.Bottom > spr2.Properties.Top) &&
             ( spr1.Properties.Top < spr2.Properties.Bottom);
        }

        private static bool IsTouchingTop(PrimitiveComponent spr1, PrimitiveComponent spr2)
        {
            return (spr1.Properties.Bottom + spr1.Velo.Y > spr2.Properties.Top) &&
              (spr1.Properties.Top < spr2.Properties.Top )&&
              (spr1.Properties.Right > spr2.Properties.Left) &&
              (spr1.Properties.Left < spr2.Properties.Right);
        }

        private static bool IsTouchingBottom(PrimitiveComponent spr1, PrimitiveComponent spr2)
        {
            return (spr1.Properties.Top + spr1.Velo.Y < spr2.Properties.Bottom) &&
             ( spr1.Properties.Bottom > spr2.Properties.Bottom) &&
             ( spr1.Properties.Right > spr2.Properties.Left) &&
             ( spr1.Properties.Left < spr2.Properties.Right);
        }
    }
}