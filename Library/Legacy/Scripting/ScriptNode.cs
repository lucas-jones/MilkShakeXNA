using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using LuaInterface;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;
using System.Reflection;
using MilkShakeFramework.Core.Content;

namespace MilkShakeFramework.Components.Scripting
{
    public class ScriptNode : GameEntity
    {
        public const string EVENT_SETUP = "Setup";
        public const string EVENT_LOAD = "Load";
        public const string EVENT_FIXUP = "Fixup";
        public const string EVENT_UPDATE = "Update";

        private Lua mScript;
        private string mUrl;

        public ScriptNode(GameEntity gameEntity, string url)
        {
            mUrl = url;
            mScript = new Lua();

            AddFunctions(typeof(ScriptFunctions));
            AddFunctions(typeof(MilkHooks));

            mScript["gameObject"] = gameEntity;
            mScript["scene"] = Scene;
            attatchListeners(gameEntity);

            mScript.DoFile("Scripts//" + mUrl);
        }

        private void AddFunctions(Type classType)
        {
            MethodInfo[] scriptFunctions = classType.GetMethods();

            for (int index = 0; index < scriptFunctions.Length; index++)
            {     
                bool isLuaFunction = (Attribute.IsDefined(scriptFunctions[index], typeof(LUAFunction)));
                if (isLuaFunction)
                {
                    AddGlobalFunction(classType, scriptFunctions[index].Name);

                    Console.WriteLine("Adding Function: " + scriptFunctions[index].Name);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        private void attatchListeners(GameEntity gameEntity)
        {
            gameEntity.Listener.Setup   += () => dispatchEvent(EVENT_SETUP);
            gameEntity.Listener.Load    += () => dispatchEvent(EVENT_LOAD);
            gameEntity.Listener.Fixup   += () => dispatchEvent(EVENT_FIXUP);
            gameEntity.Listener.Update  += (GameTime gametime) => MilkHooks.TriggerEvent(EVENT_UPDATE, gametime);
        }

        private void dispatchEvent(string eventName, params object[] arguments)
        {
            MilkHooks.TriggerEvent(eventName, arguments);
        }

        /// <summary>
        /// AddGlobalFunction - Neatens up the stringy proccess of adding Global Lua Functions
        /// </summary>
        /// <param name="_Type">This is where the class is passed</param>
        /// <param name="_LocalName">Local name is a referance to a function within MilShake</param>
        /// <param name="_LUAName">Name of the function which will be usedin LUA</param>
        private void AddGlobalFunction(Type _Type, string _LocalName, string _LUAName = "_NameClone")
        {
            if (_LUAName == "_NameClone")
                _LUAName = _LocalName;

            try
            {
                MethodInfo _MethodInfo = _Type.GetMethod(_LocalName);
                mScript.RegisterFunction(_LUAName, _Type, _MethodInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine("AddGlobalFunction - Failed!" +
                                                          "\n\tType:" + _Type.Name +
                                                          "\n\tLocal:" + _LocalName +
                                                          "\n\tLuaName:" + _LUAName);
            }
        }

        public Lua Script { get { return mScript; } }
        public string Url { get { return mUrl; } }
    }

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class LUAFunction : System.Attribute
    {
        public LUAFunction()
        {
        }
    }
}
