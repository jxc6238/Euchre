using System;

namespace SWENG421_FinalProject
{
    public class FaceFactory : FaceFactoryIF
    {
        public FaceIF createFace(string name)
        {
            string temp = "SWENG421_FinalProject." + name;
            Type t = Type.GetType(temp);
            Object o = Activator.CreateInstance(t);
            FaceIF face = (FaceIF)o;
            return face;
        }
    }
}
