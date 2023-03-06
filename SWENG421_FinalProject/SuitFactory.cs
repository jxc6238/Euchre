using System;

namespace SWENG421_FinalProject
{
    public class SuitFactory : SuitFactoryIF
    {
        public SuitIF createSuit(string name)
        {
            string temp = "SWENG421_FinalProject." + name;
            Type t = Type.GetType(temp);
            Object o = Activator.CreateInstance(t);
            SuitIF suit = (SuitIF)o;
            return suit;
        }
    }
}
