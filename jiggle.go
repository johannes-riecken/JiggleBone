package main

import (
	"fmt"
)

type Vector3 struct {
	x, y, z float64
}

type Game struct {
	force Vector3
	acc Vector3
	vel Vector3
	dynamicPos Vector3
	targetPos Vector3
}

// Dynamics settings
const bStiffness = 0.125;
const oneOverMass = 1.125;
const bDamping = 0.75;
const bGravity = 0.75;

func (g *Game) update() {
		// Bone settings
		boneAxis := Vector3{0, 0, 1};
		targetDistance := 2.0;

		// Calculate target position
		g.targetPos = Vector3{boneAxis.x * targetDistance,boneAxis.y * targetDistance,boneAxis.z * targetDistance};

		// Calculate g.force, acceleration, and velocity per X, Y and Z
		g.force.x = (g.targetPos.x - g.dynamicPos.x) * bStiffness;
		g.acc.x = g.force.x * oneOverMass;
		g.vel.x += g.acc.x * (1 - bDamping);

		g.force.y = (g.targetPos.y - g.dynamicPos.y) * bStiffness;
		g.force.y -= bGravity / 8; // Add some gravity
		g.acc.y = g.force.y * oneOverMass;
		g.vel.y += g.acc.y * (1 - bDamping);

		g.force.z = (g.targetPos.z - g.dynamicPos.z) * bStiffness;
		g.acc.z = g.force.z * oneOverMass;
		g.vel.z += g.acc.z * (1 - bDamping);

		// Update dynamic postion
		// TODO: Investigate if mass has to be 1 so that velocity and force can
		// be added
		g.dynamicPos.x += g.vel.x + g.force.x;
		g.dynamicPos.y += g.vel.y + g.force.y;
		g.dynamicPos.z += g.vel.z + g.force.z;
}

func main() {
	g := Game{}
	for i := 0; i < 100; i++ {
		g.update();
		fmt.Println(g.dynamicPos.z)
	}
	// g.update()
	// fmt.Println(g.dynamicPos.z)
}
