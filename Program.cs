using System.Collections;
using System;

namespace JiggleBone {
class JiggleBoneStatic {
  static void Main(string[] args) {
    JiggleBone jb = new JiggleBone();
    jb.Awake();
    jb.LateUpdate();
    Console.WriteLine("force.x: {}\nforce.y: {}\nforce.z: {}\nacc.x: {}\nacc.y: {}\nacc.z: {}\nvel.x: {}\nvel.y: {}\nvel.z: {}, targetPos", jb.force.x, jb.force.y, jb.force.z, jb.acc.x, jb.acc.y, jb.acc.z, jb.vel.x, jb.vel.y, jb.vel.z, jb.targetPos);
  }
}
}
