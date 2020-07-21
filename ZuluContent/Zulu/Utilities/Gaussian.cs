using System;
using Util = Server.Utility;

namespace Scripts.Zulu.Utilities
{
  public class Gaussian
  {
    private double z0;
    private double z1;
    private bool generate = false;

    public double Next(double mu, double sigma)
    {
      // Box-Muller algorithm, supposedly.
      generate = !generate;

      if (!generate)
      {
        return z1 * sigma + mu;
      }

      double u0 = Util.RandomDouble();
      double u1 = Util.RandomDouble();

      z0 = Math.Sqrt(-2.0 * Math.Log(u0)) * Math.Cos(2.0 * Math.PI * u1);
      z1 = Math.Sqrt(-2.0 * Math.Log(u0)) * Math.Sin(2.0 * Math.PI * u1);

      return z0;
    }
  }
}
