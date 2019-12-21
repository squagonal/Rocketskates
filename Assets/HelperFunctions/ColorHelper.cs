using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorHelper
{
  public static Vector4 toHSV(this Color c){
      var rval = Vector4.zero;
      rval.w=c.a;
      Color.RGBToHSV(c,out rval.x,out rval.y,out rval.z);
      return rval;
  }
  public static Color setA(this Color c, float a){
      c.a=a;
      return c;

  }
  public static Color setR(this Color c, float r){
      c.r=r;
      return c;

  }
  public static Color setG(this Color c, float g){
      c.g=g;
      return c;

  }
  public static Color setB(this Color c, float b){
      c.b=b;
      return c;

  }
  public static Color toRGB(Vector4 v){
      return Color.HSVToRGB(v.x,v.y,v.z).setA(v.w);
  }
}
