using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace spaceconquest
{
    class StarField
    {
        static Model star;
        int number;
        Random rand;
        List<Vector3> starlist;
        static int distance = 2000;
        static int size = 5;
        //Color color = Color.White;
        BasicEffect effect;
        
        public StarField(int n)
        {
            number = n;
            starlist = new List<Vector3>(number);
            rand = new Random();
            Vector3 direction;
            
            effect = new BasicEffect(Game1.device);

            for (int i = 0; i < number; i++)
            {
                direction = new Vector3(nrand(1000), nrand(1000), nrand(1000));
                direction.Normalize();
                direction = direction * distance;
                starlist.Add(direction);
            }

            foreach (ModelMesh mesh in star.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = effect;
                }
            }

        }

        private int nrand(int i)
        {
            int sign;
            sign = rand.Next(2);
            if (sign == 1) { return rand.Next(i); }
            else { return -rand.Next(i); }
        }

        public static void LoadStarModel(Model m)
        {
            star = m;
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;
            //effect.VertexColorEnabled = true;

            foreach (Vector3 pos in starlist)
            {
                foreach (ModelMesh mesh in star.Meshes)
                {
                    effect.World = Matrix.CreateScale(size)* Matrix.CreateTranslation(pos) * world;
                    mesh.Draw();
                }
            }

            //foreach (EffectPass effectPass in effect.CurrentTechnique.Passes)
            //{
            //    effectPass.Apply();
            //    Game1.device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList,starlist,0,number);
            //}
            


        }
        
    }
}
