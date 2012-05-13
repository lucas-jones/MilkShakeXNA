luanet.load_assembly "System"
luanet.load_assembly("Milkshake");

Console = luanet.import_type "System.Console"
Math = luanet.import_type "System.Math"

Node = luanet.import_type("MilkShakeFramework.Core.Node");
Sprite = luanet.import_type("MilkShakeFramework.Core.Game.Sprite");
MouseInput = luanet.import_type("MilkShakeFramework.IO.Input.Devices.MouseInput");

----------------------------------------------------------------------------------------------

curTime = 0
keySprite = Sprite("tiles/key")

function Fixup()
	gameObject:AddNode(keySprite)
end

function Update(GameTime)
	if MouseInput.isLeftDown() == true then

		curTime = curTime + GameTime.ElapsedGameTime.Milliseconds

		keySprite.Y = Sin(curTime * 0.01) * 15
		keySprite.X = Cos(curTime * 0.01) * 15
	end
end

AddHook("Fixup", Fixup);
AddHook("Update", Update);