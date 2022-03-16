import FRP.Yampa
import Control.Monad.Trans.MSF.Reader

stiffness = 0.125
oneOverMass = 1.125
damping = 0.75
gravity = 0.75
targetDistance = 2.0
targetPos = 2.0 -- in the Z direction

jiggle :: MSF
jiggle = proc () -> do
	let force = targetPos - dynamicPos) * stiffness
	let acc = force * oneOverMass
	let vel += acc * (1 - damping)
	let dynamicPos += vel + force
